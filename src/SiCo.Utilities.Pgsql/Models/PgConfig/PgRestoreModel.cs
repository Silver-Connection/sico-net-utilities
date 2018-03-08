using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SiCo.Utilities.Pgsql.Models.PgConfig
{
    /// <summary>
    /// Config for pg_restore
    /// </summary>
    public class PgRestoreModel : BaseModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PgRestoreModel()
            : base()
        {
            this.ExitOnError = true;
            this.List = false;
            this.SingleTransaction = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs"></param>
        public PgRestoreModel(PgDumpModel configs)
        {
            this.CleanDb = configs.CleanDb;
            this.Create = configs.Create;
            this.DataOnly = configs.DataOnly;
            this.Format = configs.Format;
            this.SchemaOnly = configs.SchemaOnly;
            this.Verbose = configs.Verbose;
            this.Worker = configs.Worker;
        }

        /// <summary>
        /// pg_restore
        /// </summary>
        public bool ExitOnError { get; set; }

        /// <summary>
        /// pg_restore
        /// </summary>
        [JsonIgnore]
        public bool List { get; set; }

        /// <summary>
        /// pg_restore
        /// </summary>
        public bool SingleTransaction { get; set; }

        /// <summary>
        /// Validate file before import
        /// </summary>
        [JsonIgnore]
        public bool Validate { get; set; } = true;
    }
}
