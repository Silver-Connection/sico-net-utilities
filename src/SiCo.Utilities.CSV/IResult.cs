namespace SiCo.Utilities.CSV
{
    using System;

    /// <summary>
    /// Interface for Results
    /// </summary>
    /// <typeparam name="TModel">Model Type</typeparam>
    public interface IResult<TModel>
    {
        /// <summary>
        /// Source File
        /// </summary>
        string File { get; set; }

        /// <summary>
        /// Header
        /// </summary>
        System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int, string>> Header { get; set; }

        /// <summary>
        /// Log
        /// </summary>
        string Log { get; set; }

        /// <summary>
        /// CSV Options
        /// </summary>
        Options Options { get; set; }

        /// <summary>
        /// RAW Content
        /// </summary>
        System.Collections.Generic.IEnumerable<string[]> Raw { get; set; }

        /// <summary>
        /// Duration for reading
        /// </summary>
        TimeSpan TimeRead { get; set; }

        /// <summary>
        /// Duration for transforming
        /// </summary>
        TimeSpan TimeTransform { get; set; }

        /// <summary>
        /// List of transformed Model. Each Row is one Model
        /// </summary>
        System.Collections.Generic.IEnumerable<TModel> Transformed { get; set; }

        #region Methods

        /// <summary>
        /// Fill Log Property
        /// </summary>
        void CreateLog();

        #endregion Methods
    }
}