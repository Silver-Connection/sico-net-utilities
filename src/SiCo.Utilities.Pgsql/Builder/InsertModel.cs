namespace SiCo.Utilities.Pgsql.Builder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Create INSERT query
    /// </summary>
    public class InsertModel : QueryModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public InsertModel()
            : base()
        {
            this.Values = new List<string>();
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="schema">Schema Name</param>
        /// <param name="table">Table Name</param>
        public InsertModel(string schema, string table)
            : base(schema, table)
        {
            this.Values = new List<string>();
        }

        /// <summary>
        /// Values
        /// </summary>
        public IList<string> Values { get; set; }

        /// <summary>
        /// Build INSERT query
        /// </summary>
        /// <returns></returns>
        public override string Build()
        {
            return this.Build(false, null);
        }

        /// <summary>
        /// Build INSERT query
        /// </summary>
        /// <param name="skipId">skip ID</param>
        /// <param name="injects">Overwrite / Add values</param>
        /// <returns></returns>
        public string Build(bool skipId, IEnumerable<KeyValuePair<string, string>> injects = null)
        {
            if (this.Columns.Count != this.Values.Count)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(this.Values),
                    $"Values count({this.Values.Count}) does not match Columns count({this.Columns.Count}) for Table: {this.FullName}");
            }

            if (injects?.Count() > 0)
            {
                foreach (var item in injects)
                {
                    this.ValueAdd(item.Key, item.Value);
                }
            }

            var col = string.Empty;
            var val = string.Empty;
            var hasId = false;

            for (int i = 0; i < this.Columns.Count; i++)
            {
                if (this.Columns[i] == "id")
                {
                    hasId = true;

                    if (skipId)
                    {
                        continue;
                    }
                }

                col += $", \"{this.Columns[i]}\"";
                val += $", {this.Values[i]}";
            }

            var getInsertId = string.Empty;
            if (hasId)
            {
                getInsertId = " RETURNING id";
            }

            return $"INSERT INTO {this.FullName}({col.Trim().Trim(',').Trim()}) VALUES ({val.Trim().Trim(',').Trim()}){getInsertId};";
        }

        #region Values

        /// <summary>
        /// Add a value to given column
        /// </summary>
        /// <param name="column">Column Name</param>
        /// <param name="value">SQL Value</param>
        public void ValueAdd(string column, string value)
        {
            for (int i = 0; i < this.Columns.Count; i++)
            {
                if (this.Columns[i] == column)
                {
                    this.Values[i] = value;
                    return;
                }
            }

            this.Columns.Add(column);
            this.Values.Add(value);
        }

        /// <summary>
        /// Set value for given column
        /// </summary>
        /// <param name="column">Column Name</param>
        /// <param name="value">SQL Value</param>
        public void ValueSet(string column, string value)
        {
            for (int i = 0; i < this.Columns.Count; i++)
            {
                if (this.Columns[i] == column)
                {
                    this.Values[i] = value;
                    break;
                }
            }
        }

        /// <summary>
        /// Remove Value
        /// </summary>
        /// <param name="column">Column Name</param>
        public void ValueRemove(string column)
        {
            var col = this.Columns.ToArray();
            var val = this.Values.ToArray();
            this.Columns.Clear();
            this.Values.Clear();
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i] == column)
                {
                    continue;
                }

                this.Columns.Add(col[i]);
                this.Values.Add(val[i]);
            }
        }

        #endregion Values

        #region Statics

        /// <summary>
        /// Read Table and Column attributes and create InsertModel with filled data
        /// </summary>
        /// <typeparam name="TModel">Model Type</typeparam>
        /// <param name="model">model</param>
        /// <returns></returns>
        public static InsertModel Create<TModel>(TModel model)
            where TModel : new()
        {
            var result = new InsertModel();
            var properties = typeof(TModel).GetProperties().Where(prop => prop.IsDefined(typeof(ColumnAttribute), false));
            var table = (TableAttribute)typeof(TModel).GetTypeInfo().GetCustomAttribute(typeof(TableAttribute));
            if (table != null)
            {
                result.Table = table.Name;
                result.Schema = table.Schema;
            }

            foreach (var item in properties)
            {
                ColumnAttribute[] attributes = (ColumnAttribute[])item.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (null != attributes[0])
                {
                    result.Columns.Add(attributes[0].Name);
                    result.Values.Add(Common.GetSqlValue(item, item.GetValue(model, null)));
                }
            }

            return result;
        }

        #endregion Statics
    }
}