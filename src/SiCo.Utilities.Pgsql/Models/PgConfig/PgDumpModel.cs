namespace SiCo.Utilities.Pgsql.Models.PgConfig
{
    using Newtonsoft.Json;

    /// <summary>
    /// pg_dump configuration
    /// </summary>
    public class PgDumpModel : BaseModel
    {
        /// <summary>
        ///
        /// </summary>
        public PgDumpModel()
            : base()
        {
            this.Compress = 0;
            this.SingleFile = false;
            this.TarDir = true;

            this.Date = string.Empty;
            this.Time = string.Empty;
        }

        /// <summary>
        /// pg_dump
        /// </summary>
        public int Compress { get; set; }

        /// <summary>
        /// pg_dump
        /// </summary>
        public bool SingleFile { get; set; }

        /// <summary>
        /// pg_dump: Compress directory after copy
        /// </summary>
        public bool TarDir { get; set; }

        [JsonIgnore]
        private string Date { get; set; }

        [JsonIgnore]
        private string Time { get; set; }
    }
}