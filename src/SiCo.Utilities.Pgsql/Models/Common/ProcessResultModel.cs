namespace SiCo.Utilities.Pgsql.Models.Common
{
    using System;

    /// <summary>
    /// Process Model
    /// </summary>
    public class ProcessResultModel
    {
        /// <summary>
        /// Checksum
        /// </summary>
        public string Checksum { get; set; }

        /// <summary>
        /// Duration
        /// </summary>
        public TimeSpan Duartion { get; set; }

        /// <summary>
        /// Error Log
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Output
        /// </summary>
        public string Ouput { get; set; }
    }
}