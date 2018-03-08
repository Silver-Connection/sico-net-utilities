namespace SiCo.Utilities.CSV
{
    using System;

    /// <summary>
    /// Sample Formats
    /// </summary>
    public static class Formats
    {
        /// <summary>
        /// Error Log
        /// </summary>
        public static readonly string Error = "## Error" + Environment.NewLine + "Line\t\t: {0}" + Environment.NewLine + "Message\t\t: {1}";

        /// <summary>
        /// Headline 1
        /// </summary>
        // Heads Lines
        public static readonly string H1 = "##################### {0,-10} #####################" + Environment.NewLine;

        /// <summary>
        /// Headline 2
        /// </summary>
        public static readonly string H2 = "_____________________ {0,-10} _____________________" + Environment.NewLine;

        /// <summary>
        /// Key Value string
        /// </summary>
        public static readonly string KeyVal = "{0,-20} : {1}" + Environment.NewLine;

        /// <summary>
        /// Table with 6 columns
        /// </summary>
        public static readonly string Table6 = "{0,-5} : {1,-5} : {2,-5} : {3,-15} : {4,-15} : {5,-15}" + Environment.NewLine;
    }
}