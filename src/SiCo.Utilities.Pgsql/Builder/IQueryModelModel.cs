namespace SiCo.Utilities.Pgsql.Builder
{
    using System.Collections.Generic;

    /// <summary>
    /// Base Query Interface
    /// </summary>
    public interface IQueryModelModel
    {
        /// <summary>
        /// Column Names
        /// </summary>
        IList<string> Columns { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Has ID
        /// </summary>
        bool HasId { get; }

        /// <summary>
        /// Schame Name
        /// </summary>
        string Schema { get; set; }

        /// <summary>
        /// Table Name
        /// </summary>
        string Table { get; set; }

        /// <summary>
        /// Build Query
        /// </summary>
        /// <returns>SQL Query</returns>
        string Build();
    }
}