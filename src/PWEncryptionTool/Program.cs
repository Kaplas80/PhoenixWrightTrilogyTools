// -------------------------------------------------------
// © Kaplas. Licensed under MIT. See LICENSE for details.
// -------------------------------------------------------
namespace PWEncryptionTool
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using CommandLine;

    /// <summary>
    /// Main program.
    /// </summary>
    internal static class Program
    {
        private const string Password = "u8DurGE2";
        private const string Salt = "6BBGizHE";

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options.Decrypt, Options.Encrypt>(args)
                .WithParsed<Options.Decrypt>(Decrypt)
                .WithParsed<Options.Encrypt>(Encrypt);
        }

        private static void WriteHeader(bool encrypt)
        {
            Console.WriteLine(CommandLine.Text.HeadingInfo.Default);
            Console.WriteLine(CommandLine.Text.CopyrightInfo.Default);
            Console.WriteLine();
            Console.WriteLine($"MODE: {(encrypt ? "ENCRYPT" : "DECRYPT")}");
            Console.WriteLine();
        }

        private static void Decrypt(Options.Decrypt opts)
        {
            Process(opts, false);
        }

        private static void Encrypt(Options.Encrypt opts)
        {
            Process(opts, true);
        }

        private static void Process(Options.Common opts, bool encrypt)
        {
            WriteHeader(encrypt);

            if (!File.Exists(opts.InputFile))
            {
                Console.WriteLine($"ERROR: \"{opts.InputFile}\" not found!!!!");
                return;
            }

            if (File.Exists(opts.OutputFile))
            {
                Console.WriteLine($"WARNING: \"{opts.OutputFile}\" already exists. It will be overwritten.");
                Console.Write("Continue? (y/N) ");
                string answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && answer.ToUpperInvariant() != "Y")
                {
                    Console.WriteLine("CANCELLED BY USER.");
                    return;
                }
            }

            Console.Write($"Processing '{opts.InputFile}'...");
            byte[] data = File.ReadAllBytes(opts.InputFile);

            if (!encrypt && !IsEncrypted(data))
            {
                Console.WriteLine(" NOT ENCRYPTED!!");
            }
            else
            {
                byte[] processedData = ProcessData(data, encrypt);
                Directory.CreateDirectory(Path.GetDirectoryName(opts.OutputFile));
                File.WriteAllBytes(opts.OutputFile, processedData);
                Console.WriteLine(" DONE!");
            }
        }

        private static bool IsEncrypted(byte[] data)
        {
            byte[] unityFsMagic = { 0x55, 0x6E, 0x69, 0x74, 0x79, 0x46, 0x53 }; // UnityFs
            var span = new ReadOnlySpan<byte>(data, 0, 7);
            return !span.SequenceEqual(new ReadOnlySpan<byte>(unityFsMagic));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5379:Do Not Use Weak Key Derivation Function Algorithm", Justification = "Game algorithm.")]
        private static byte[] ProcessData(byte[] inputData, bool encrypt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Salt);
            using var keyDerivation = new Rfc2898DeriveBytes(Password, bytes, 1000);
            using var algorithm = new RijndaelManaged { KeySize = 128, BlockSize = 128 };
            algorithm.Key = keyDerivation.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = keyDerivation.GetBytes(algorithm.BlockSize / 8);

            ICryptoTransform cryptoTransform = encrypt ? algorithm.CreateEncryptor() : algorithm.CreateDecryptor();

            byte[] result = cryptoTransform.TransformFinalBlock(inputData, 0, inputData.Length);
            cryptoTransform.Dispose();

            return result;
        }
    }
}
