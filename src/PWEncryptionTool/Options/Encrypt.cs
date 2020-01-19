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
    [Verb("encrypt", HelpText = "Encrypts a Phoenix Wright: Ace Attorney Trilogy file.")]
    internal class Encrypt : Common
    {
    }
}