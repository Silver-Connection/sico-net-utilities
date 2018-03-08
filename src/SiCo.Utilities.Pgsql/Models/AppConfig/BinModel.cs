namespace SiCo.Utilities.Pgsql.Models.AppConfig
{
    using Newtonsoft.Json;
    using SiCo.Utilities.Generics;

    /// <summary>
    /// App Settings
    /// </summary>
    public class BinModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BinModel()
        {
        }

        /// <summary>
        /// Path to pg_dump binary
        /// </summary>
        [JsonProperty(PropertyName = "pg_dump")]
        public string Pg_Dump { get; set; }

        /// <summary>
        /// Path to pg_restore binary
        /// </summary>
        [JsonProperty(PropertyName = "pg_restore")]
        public string Pg_Restore { get; set; }

        /// <summary>
        /// Path to psql binary
        /// </summary>
        [JsonProperty(PropertyName = "psql")]
        public string Psql { get; set; }

        /// <summary>
        /// Clean user input
        /// </summary>
        public void Clean()
        {
            this.Pg_Dump = this.Pg_Dump.TrimNotEmpty();
            this.Pg_Restore = this.Pg_Restore.TrimNotEmpty();
            this.Psql = this.Psql.TrimNotEmpty();
        }
    }
}