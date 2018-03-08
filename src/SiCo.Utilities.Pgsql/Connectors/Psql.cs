namespace SiCo.Utilities.Pgsql.Connectors
{
    using Generics;

    /// <summary>
    /// Run command via psql application
    /// </summary>
    public class Psql : Models.Common.PostgreBaseModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Psql()
            : base()
        {
            this.Query = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Default Settings</param>
        public Psql(Models.AppConfig.AppSettingsModel settings)
            : this()
        {
            this.LoadAppSettings(settings);
        }

        /// <summary>
        /// Query to run
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Clean user input
        /// </summary>
        public override void Clean()
        {
            base.Clean();
            this.Query = this.Query.TrimNotEmpty();
        }

        #region Get

        /// <summary>
        /// Generate parameters
        /// </summary>
        /// <param name="connection">Connection settings</param>
        /// <returns>Connection parameters</returns>
        public override string GetPatameters(Models.Connection.IBaseModel connection)
        {
            if (connection == null)
            {
                return string.Empty;
            }

            string param = string.Empty;
            if (!string.IsNullOrEmpty(this.Query))
            {
                param += "--command=\"" + this.Query + "\"";
            }
            else
            {
                if (!string.IsNullOrEmpty(this.File))
                {
                    param += " --file=\"" + this.File + "\"";
                    connection.Database = string.Empty;
                }
            }

            return this.ConnectionString(connection) + " " + param;
        }

        #endregion Get
    }
}