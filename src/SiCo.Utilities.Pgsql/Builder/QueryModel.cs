namespace SiCo.Utilities.Pgsql.Builder
{
    using System.Collections.Generic;
    using SiCo.Utilities.Generics;

    /// <summary>
    /// Query Model
    /// </summary>
    public abstract class QueryModel : IQueryModelModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public QueryModel()
        {
            this.Columns = new List<string>();
            this.Schema = string.Empty;
            this.Table = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="schema">Schema Name</param>
        /// <param name="table">Table Name</param>
        public QueryModel(string schema, string table)
            : this()
        {
            this.Schema = schema;
            this.Table = table;
        }

        /// <summary>
        /// Columns
        /// </summary>
        public IList<string> Columns { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName
        {
            get
            {
                return ($"\"{this.Schema}\".\"{this.Table}\"").Trim("\"\"").Trim('.');
            }
        }

        /// <summary>
        /// Has ID
        /// </summary>
        public bool HasId
        {
            get
            {
                if (this.Columns?.Count > 0)
                {
                    foreach (var item in this.Columns)
                    {
                        if (item == "id")
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Table Name
        /// </summary>
        public string Table { get; set; }

        #region Methods

        /// <summary>
        /// Build Query
        /// </summary>
        /// <returns>SQL Query</returns>
        public abstract string Build();

        #endregion Methods
    }
}