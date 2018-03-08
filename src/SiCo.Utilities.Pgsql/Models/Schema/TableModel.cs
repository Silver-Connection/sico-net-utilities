namespace SiCo.Utilities.Pgsql.Models.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Generics;

    /// <summary>
    /// Builder Table Model
    /// </summary>
    public class TableModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public TableModel()
        {
            this.Name = string.Empty;
            this.Schema = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="list">List of Column Information Model</param>
        public TableModel(IEnumerable<InformationSchema.ColumnModel> list)
        {
            var model = list.First();
            this.Name = model.Table;
            this.Schema = model.Schema;

            var columns = new List<ColumModel>(list.Count());
            foreach (var item in list)
            {
                columns.Add(new ColumModel(item));
            }
            this.Columns = columns;
        }

        /// <summary>
        /// List of Constrain Checks
        /// </summary>
        public IEnumerable<ConstraintModel> Checks { get; set; }

        /// <summary>
        /// List of Columns
        /// </summary>
        public IEnumerable<ColumModel> Columns { get; set; }

        /// <summary>
        /// List of Constrains
        /// </summary>
        public IEnumerable<ConstraintModel> Constraints { get; set; }

        /// <summary>
        /// List of Foreign Keys
        /// </summary>
        public IEnumerable<ForeignKeyModel> ForeignKeys { get; set; }

        /// <summary>
        /// List of Indexes
        /// </summary>
        public IEnumerable<IndexModel> Indexes { get; set; }

        /// <summary>
        /// Table Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Table Primary Key
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Table References
        /// </summary>
        public IEnumerable<ReferencedModel> References { get; set; }

        /// <summary>
        /// Schame Name
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// SQL Query
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// List of Triggers
        /// </summary>
        public IEnumerable<TriggerModel> Triggers { get; set; }

        /// <summary>
        /// Read Table Information from server
        /// </summary>
        /// <param name="settings">Default Settings</param>
        /// <param name="connection">Connection String</param>
        public void ReadSqlDefinitions(AppConfig.AppSettingsModel settings, Connection.BaseModel connection)
        {
            var psql = new Connectors.Psql(settings)
            {
                Query = $"\\d+ {this.Schema}.{this.Name};"
            };

            var result = psql.Run(connection, true);
            this.Sql = StringExtensions.IsEmpty(result.Ouput) ? result.Error : result.Ouput;

            // parse output
            var lines = this.Sql.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            // index
            var isIndex = false;
            var index = new List<IndexModel>();
            var indexRegexp = new Regex("Indexes:");

            // referenced
            var isReferenced = false;
            var referenced = new List<ReferencedModel>();
            var referencedRegexp = new Regex("Referenced by:");

            // triggers
            var isTrigger = false;
            var trigger = new List<TriggerModel>();
            var triggerRegexp = new Regex("Triggers:");

            // options
            //var optionRegexp = new Regex("Options:");

            var stopRegexp = new Regex(":");

            foreach (var item in lines)
            {
                // indexes
                if (indexRegexp.IsMatch(item))
                {
                    isIndex = true;
                    continue;
                }

                if (isIndex)
                {
                    if (!stopRegexp.IsMatch(item) && !StringExtensions.IsEmpty(item))
                    {
                        index.Add(new IndexModel(item));
                        continue;
                    }
                    else
                    {
                        isIndex = false;
                    }
                }

                // referenced by
                if (referencedRegexp.IsMatch(item))
                {
                    isReferenced = true;
                    continue;
                }

                if (isReferenced)
                {
                    if (!stopRegexp.IsMatch(item) && !StringExtensions.IsEmpty(item))
                    {
                        referenced.Add(new ReferencedModel(item));
                        continue;
                    }
                    else
                    {
                        isReferenced = false;
                    }
                }

                // trigger
                if (triggerRegexp.IsMatch(item))
                {
                    isTrigger = true;
                    continue;
                }

                if (isTrigger)
                {
                    if (!stopRegexp.IsMatch(item) && !StringExtensions.IsEmpty(item))
                    {
                        trigger.Add(new TriggerModel(item));
                        continue;
                    }
                    else
                    {
                        isTrigger = false;
                    }
                }

                //// options
                //if (optionRegexp.IsMatch(item))
                //{
                //    this.Options = item
                //        .Replace("Options: ", string.Empty)
                //        .Trim()
                //        .Split(',')
                //        .Select(p => p.Trim());
                //    continue;
                //}
            }

            this.Indexes = index;
            this.References = referenced;
            this.Triggers = trigger;
        }

        /// <summary>
        /// Set Table Constrains
        /// </summary>
        /// <param name="list">List of Constrains</param>
        /// <param name="fkeys">List of Foreign Keys</param>
        public void SetConstraints(
                    IEnumerable<InformationSchema.ConstraintModel> list,
            IEnumerable<InformationSchema.ForeignKeyModel> fkeys)
        {
            var columns = new List<ConstraintModel>();
            var keys = new List<ForeignKeyModel>();
            var checks = new List<ConstraintModel>();
            foreach (var item in list)
            {
                if (item.Type == "FOREIGN KEY")
                {
                    var match = fkeys.SingleOrDefault(p => p.Name == item.Name);
                    if (match != null)
                    {
                        keys.Add(new ForeignKeyModel(match));
                    }
                    else
                    {
                        Console.WriteLine($"Could not find informations for foreign key: {item.Name}");
                    }
                    continue;
                }

                if (item.Type == "CHECK")
                {
                    checks.Add(new ConstraintModel(item));
                    continue;
                }

                if (item.Type == "PRIMARY KEY")
                {
                    this.PrimaryKey = item.Name;
                    continue;
                }
            }
            this.Constraints = columns;
            this.ForeignKeys = keys;
            this.Checks = checks;
        }
    }
}