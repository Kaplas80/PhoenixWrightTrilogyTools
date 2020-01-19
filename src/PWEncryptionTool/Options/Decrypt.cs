// -------------------------------------------------------
// © Kaplas. Licensed under MIT. See LICENSE for details.
// -------------------------------------------------------
namespace PWEncryptionTool.Options
{
    using CommandLine;

    /// <summary>
    /// Phoenix Wright: Ace Attorney Trilogy decrypt options.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Class is passed as type parameter.")]
    [Verb("decrypt", HelpText = "Decrypts a Phoenix Wright: Ace Attorney Trilogy file.")]
    internal class Decrypt : Common
    {
    }
}