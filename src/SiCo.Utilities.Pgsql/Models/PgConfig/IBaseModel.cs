namespace SiCo.Utilities.Pgsql.Models.PgConfig
{
    /// <summary>
    /// Common config for pg_dump and pg_restore
    /// </summary>
    public interface IBaseModel
    {
        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        bool CleanDb { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        bool Create { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        bool DataOnly { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        string Format { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        bool SchemaOnly { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        bool Verbose { get; set; }

        /// <summary>
        /// pg_dump / pg_export
        /// </summary>
        int Worker { get; set; }

        /// <summary>
        /// Clean input
        /// </summary>
        void Clean();
    }
}