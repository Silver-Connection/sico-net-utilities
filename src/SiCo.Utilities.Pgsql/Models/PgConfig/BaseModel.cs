namespace SiCo.Utilities.Pgsql.Models.PgConfig
{
    /// <summary>
    /// Common config model for pg_dump / pg_restore
    /// </summary>
    public class BaseModel : IBaseModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseModel()
        {
            this.CleanDb = false;
            this.Create = false;
            this.DataOnly = false;
            this.Format = "p";
            this.Worker = 1;
            this.SchemaOnly = false;
            this.Verbose = false;
        }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        public bool CleanDb { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        public bool Create { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        public bool DataOnly { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        public bool SchemaOnly { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        public int Worker { get; set; }

        /// <summary>
        /// Check config
        /// </summary>
        public virtual void Clean()
        {
            this.Format = Pgsql.Common.FormatChecker(this.Format);

            if (this.Format != "d")
            {
                this.Worker = 1;
            }
        }
    }
}