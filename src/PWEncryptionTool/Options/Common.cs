// -------------------------------------------------------
// © Kaplas. Licensed under MIT. See LICENSE for details.
// -------------------------------------------------------
namespace PWEncryptionTool.Options
{
    using CommandLine;

    /// <summary>
    /// Phoenix Wright: Ace Attorney Trilogy decrypt options.
    /// </summary>
    internal class Common
    {
        /// <summary>
        /// Gets or sets the input file.
        /// </summary>
        [Value(0, MetaName = "input_file", Required = true, HelpText = "Input file.")]
        public string InputFile { get; set; }

        /// <summary>
        /// Gets or sets the output file.
        /// </summary>
        [Value(1, MetaName = "output_file", Required = true, HelpText = "Output file.")]
        public string OutputFile { get; set; }
    }
}