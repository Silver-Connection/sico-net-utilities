namespace SiCo.Utilities.Pgsql.Models.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Common for pg_dump and pg_restore
    /// </summary>
    public class PgCommon<TConfig> : PostgreBaseModel
        where TConfig : PgConfig.IBaseModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PgCommon()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Default Paths</param>
        public PgCommon(AppConfig.AppSettingsModel settings)
            : this()
        {
            this.LoadAppSettings(settings);
        }

        /// <summary>
        /// Common Config
        /// </summary>
        public TConfig Config { get; set; }

        /// <summary>
        /// pg_dump: Tables to include in backup
        /// </summary>
        [JsonProperty(Order = 10)]
        public IEnumerable<string> Tables { get; set; }

        /// <summary>
        /// Create parameters
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public override string GetPatameters(Connection.IBaseModel connection)
        {
            return this.GetPatameters(connection, true);
        }

        /// <summary>
        /// Create parameters
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public string GetPatameters(Connection.IBaseModel connection, bool table)
        {
            if (connection == null)
            {
                return string.Empty;
            }

            // Prepare stuff
            this.Clean();

            var param = string.Format(
                " --format {0} --jobs {1}",
                        this.Config.Format,
                        this.Config.Worker);

            if (this.Config.Verbose)
            {
                param += " --verbose";
            }

            if (this.Config.DataOnly)
            {
                param += " --data-only";
            }
            else
            {
                if (this.Config.SchemaOnly)
                {
                    param += " --schema-only";
                }
            }

            if (this.Tables != null && this.Tables.Any() && table)
            {
                foreach (var item in this.Tables)
                {
                    if (!string.IsNullOrEmpty(item) && !string.IsNullOrWhiteSpace(item))
                    {
                        param += " --table " + Pgsql.Common.EscapeName(item);
                    }
                }
            }

            if (this.Config.CleanDb)
            {
                param += " --clean";
            }

            if (this.Config.Create)
            {
                param += " --create";
            }

            return base.GetPatameters(connection) + param;
        }
    }
}