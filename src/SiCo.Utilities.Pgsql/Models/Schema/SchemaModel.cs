namespace SiCo.Utilities.Pgsql.Models.Schema
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Builder Schema Model
    /// </summary>
    public class SchemaModel
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="schema">Schema Name</param>
        /// <param name="connection">Connection String</param>
        public SchemaModel(string schema, string connection)
        {
            this.Connection = connection;
            this.Name = schema;

            var builder = new Npgsql.NpgsqlConnectionStringBuilder(connection);
            this.Database = builder.Database;
        }

        /// <summary>
        /// Connection String
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// Database Name
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tables
        /// </summary>
        public IEnumerable<TableModel> Tables { get; set; }

        /// <summary>
        /// Read Information Schema
        /// </summary>
        public void ReadSchema()
        {
            this.getColumnInfos();
            this.getConstraints();
        }

        /// <summary>
        /// Read SQL Definitions
        /// </summary>
        /// <param name="settings">Default Settings</param>
        public void ReadSqlDefinitions(AppConfig.AppSettingsModel settings)
        {
            var connection = new Connection.BaseModel(this.Connection);
            var list = new List<TableModel>();
            foreach (var item in this.Tables)
            {
                var copy = item;
                copy.ReadSqlDefinitions(settings, connection);
                list.Add(item);
            }

            this.Tables = list;
        }

        private void getColumnInfos()
        {
            var query = $"SELECT * FROM information_schema.columns WHERE table_catalog  = '{this.Database}' AND table_schema = '{this.Name}' AND table_name NOT LIKE '%view%';";
            var info = Query.Table<InformationSchema.ColumnModel>(query, this.Connection);

            var list = new List<TableModel>();
            foreach (var item in info.GroupBy(p => p.Table))
            {
                list.Add(new TableModel(item));
            }

            this.Tables = list;
        }

        private void getConstraints()
        {
            var query = $"SELECT * FROM information_schema.table_constraints WHERE table_catalog  = '{this.Database}' AND table_schema = '{this.Name}' AND table_name NOT LIKE '%view%';";
            var info = Query.Table<InformationSchema.ConstraintModel>(query, this.Connection);

            query = $"SELECT CAST(confrelid::regclass AS text) AS ref_table, CAST(af.attname AS text) AS ref_column, CAST(conrelid::regclass AS text) AS b_table, CAST(a.attname AS text) AS b_column, CAST(ss2.conname AS text) AS fk_name FROM pg_attribute af, pg_attribute a,(SELECT conrelid, confrelid, conkey[i] AS conkey, confkey[i] AS confkey, ss.conname FROM (SELECT conrelid, confrelid, conkey, confkey, generate_series(1,array_upper(conkey,1)) AS i , conname FROM pg_constraint where contype = 'f') ss ) ss2 WHERE af.attnum = confkey AND af.attrelid = confrelid AND a.attnum = conkey AND a.attrelid = conrelid;";
            var keys = Query.Table<InformationSchema.ForeignKeyModel>(query, this.Connection);

            var group = info.GroupBy(p => p.Table);
            var list = new List<TableModel>();
            foreach (var item in this.Tables)
            {
                var match = group.SingleOrDefault(p => p.Key == item.Name);
                if (match != null)
                {
                    item.SetConstraints(match, keys);
                }
                list.Add(item);
            }

            this.Tables = list;
        }
    }
}