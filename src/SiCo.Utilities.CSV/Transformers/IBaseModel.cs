namespace SiCo.Utilities.CSV.Transformers
{
    using System.Collections.Generic;

    /// <summary>
    /// Base Interface for a Processor
    /// </summary>
    public interface IBaseModel
    {
        /// <summary>
        /// Processor Display Name
        /// </summary>
        string TrDisplayName { get; }

        /// <summary>
        /// Processor Name
        /// </summary>
        string TrName { get; }

        /// <summary>
        /// Main Convert Method
        /// </summary>
        /// <param name="model">Each string is a Column of one Row</param>
        /// <returns>Converted Model</returns>
        object GetConverter(string[] model);

        /// <summary>
        /// Get Report Data
        /// </summary>
        /// <param name="list">Converted List</param>
        /// <returns></returns>
        string GetReport(IEnumerable<object> list);

        /// <summary>
        /// Method for Transforming to CSV
        /// </summary>
        /// <param name="opts">CSV Options</param>
        /// <returns></returns>
        string ToCsv(Options opts);

        /// <summary>
        /// Method for Transforming to JSON
        /// </summary>
        string ToJson();

        /// <summary>
        /// Method for Transforming to SQL
        /// </summary>
        string ToSql();
    }
}