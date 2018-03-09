namespace SiCo.Utilities.Pgsql.Builder
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using Attributes;

    /// <summary>
    /// Build and run SELECT query
    /// </summary>
    public class SelectModel : QueryModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public SelectModel()
            : base()
        {
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="schema">Schema Name</param>
        /// <param name="table">Table Name</param>
        public SelectModel(string schema, string table)
            : base(schema, table)
        {
        }

        /// <summary>
        /// Build SELECT query
        /// </summary>
        /// <returns>SQL Query</returns>
        public override string Build()
        {
            if (this.Columns?.Count <= 0)
            {
                return string.Empty;
            }

            var col = string.Empty;
            for (int i = 0; i < this.Columns.Count; i++)
            {
                col += $", \"{this.Columns[i]}\"";
            }

            return $"SELECT {col.Trim().Trim(',').Trim()} FROM {this.FullName};";
        }

        #region Statics

        /// <summary>
        /// Read Table and Column attributes and create SelectModel with filled data
        /// </summary>
        /// <typeparam name="TModel">Model Type</typeparam>
        /// <param name="model">model</param>
        /// <returns></returns>
        public static SelectModel Create<TModel>(TModel model)
            where TModel : new()
        {
            var result = new SelectModel();
            var properties = typeof(TModel).GetProperties().Where(prop => prop.IsDefined(typeof(ColumnAttribute), false));
            var table = (TableAttribute)typeof(TModel).GetTypeInfo().GetCustomAttribute(typeof(TableAttribute));
            var tableName = string.Empty;
            if (table != null)
            {
                result.Table = table.Name;
                result.Schema = table.Schema;
            }

            var list = new List<KeyValuePair<int, string>>();
            foreach (var item in properties)
            {
                ColumnAttribute attributes = (ColumnAttribute)item.GetCustomAttribute(typeof(ColumnAttribute), false);
                if (attributes != null)
                {
                    ColumnPositionAttribute pos = (ColumnPositionAttribute)item.GetCustomAttribute(typeof(ColumnPositionAttribute), false);

                    if (pos != null)
                    {
                        list.Add(new KeyValuePair<int, string>(pos.Position, attributes.Name));
                    }
                }
            }

            result.Columns = list.OrderBy(p => p.Key).Select(p => p.Value).ToList();
            return result;
        }

        #endregion Statics
    }
}