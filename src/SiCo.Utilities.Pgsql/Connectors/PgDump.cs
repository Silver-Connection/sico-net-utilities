namespace SiCo.Utilities.Pgsql.Connectors
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SharpCompress.Archives;
    using SharpCompress.Archives.Tar;
    using SiCo.Utilities.Generics;

    /// <summary>
    /// Run command via pg_dump application
    /// </summary>
    public class PgDump : Models.Common.PgCommon<Models.PgConfig.PgDumpModel>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PgDump()
            : base()
        {
            this.Config = new Models.PgConfig.PgDumpModel();
            this.CurrentRun = 0;
            this.CurrentTable = string.Empty;
            this.FileNameFormat = "$date\\$time-$db.$table";
            this.Date = string.Empty;
            this.Time = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Default Settings</param>
        public PgDump(Models.AppConfig.AppSettingsModel settings)
            : this()
        {
            this.LoadAppSettings(settings);
        }

        /// <summary>
        /// pg_dump: File format
        /// </summary>
        [JsonProperty(Order = 2)]
        public string FileNameFormat { get; set; }

        /// <summary>
        /// pg_dump: Tables to exclude in backup
        /// </summary>
        [JsonProperty(Order = 12)]
        //[JsonConverter(typeof(Newtonsoft.Json.Converters.<string>))]
        public IEnumerable<string> TablesExclude { get; set; }

        [JsonIgnore]
        internal int CurrentRun { get; set; }

        [JsonIgnore]
        internal string CurrentTable { get; set; }

        [JsonIgnore]
        private string Date { get; set; }

        [JsonIgnore]
        private string Time { get; set; }

        #region Prepare

        /// <summary>
        /// Clean user input
        /// </summary>
        public override void Clean()
        {
            base.Clean();
            this.FileNameFormat.TrimNotEmpty();
            this.Config.Clean();
        }

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

            var param = base.GetPatameters(connection, false);

            if (this.Tables != null && this.Tables.Any())
            {
                if (this.Config.SingleFile)
                {
                    foreach (var item in this.Tables)
                    {
                        if (!string.IsNullOrEmpty(item) && !string.IsNullOrWhiteSpace(item))
                        {
                            param += " --table " + Pgsql.Common.EscapeName(item);
                        }
                    }
                }
                else
                {
                    param += " --table " + Pgsql.Common.EscapeName(this.CurrentTable);
                }
            }

            param += " --file \"" + this.File + "\"";

            if (this.Config.Format != "t")
            {
                param += " --compress " + this.Config.Compress.ToString();
            }

            if (this.Tables == null || !this.Tables.Any())
            {
                if (this.TablesExclude != null && this.TablesExclude.Any())
                {
                    foreach (var item in this.TablesExclude)
                    {
                        if (!string.IsNullOrEmpty(item) && !string.IsNullOrWhiteSpace(item))
                        {
                            param += " --exclude-table " + Pgsql.Common.EscapeName(item);
                        }
                    }
                }
            }

            return param;
        }

        /// <summary>
        /// Load default settings
        /// </summary>
        /// <param name="settings"></param>
        public override void LoadAppSettings(Models.AppConfig.AppSettingsModel settings)
        {
            if (settings != null)
            {
                this.AppSettings = settings;
            }

            if (this.AppSettings != null)
            {
                this.FilePath = this.AppSettings.Path.Export;
                this.Bin = this.AppSettings.Bin.Pg_Dump;
            }
        }

        /// <summary>
        /// Generate filename from filename format
        /// </summary>
        /// <param name="jobName">Job Name</param>
        /// <param name="connect">Connection settings</param>
        public void SetFileName(string jobName, Models.Connection.IBaseModel connect)
        {
            // Check if date / time is set
            if (string.IsNullOrEmpty(this.Date))
            {
                this.Date = DateTime.UtcNow.ToString("yyyyMMdd");
            }

            if (string.IsNullOrEmpty(this.Time))
            {
                this.Time = DateTime.UtcNow.ToString("HH.mm");
            }

            // date / time / db / job
            string name = this.FileNameFormat
                .TrimFile()
                .Replace("$date", this.Date)
                .Replace("$time", this.Time)
                .Replace("$db", connect.Database)
                .Replace("$job", jobName);

            // Table
            if (this.Tables != null && this.Tables.Any() && !this.Config.SingleFile)
            {
                name = name.Replace("$table", this.CurrentTable);
            }
            else
            {
                name = name.Replace("$table", string.Empty);
            }

            // Clean
            name = name.TrimEnd('.');

            // Set file extension
            switch (this.Config.Format)
            {
                case "c":
                    name += ".backup";
                    break;

                case "d":
                    break;

                case "t":
                    name += ".tar";
                    break;

                case "p":
                default:
                    name += ".sql";
                    break;
            }

            // Add gz if is compressed
            if (this.Config.Format != "t" && this.Config.Format != "d")
            {
                if (this.Config.Compress > 0)
                {
                    name += ".gz";
                }
            }

            this.FileName = name.Replace("*", string.Empty);

            // Path
            this.FileName = name.Replace("/", Path.DirectorySeparatorChar.ToString());
            this.FileName = name.Replace("\\", Path.DirectorySeparatorChar.ToString());
        }

        /// <summary>
        /// Compress exported data
        /// </summary>
        /// <returns>Process results model</returns>
        public Models.Common.ProcessResultModel Compress()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            if (this.Config.Format == "d" && this.Config.TarDir)
            {
                stopwatch.Start();

                // Compress
                using (var archive = TarArchive.Create())
                {
                    var mode = new SharpCompress.Writers.WriterOptions(SharpCompress.Common.CompressionType.None);
                    archive.AddAllFromDirectory(this.File);
                    archive.SaveTo(this.File + ".tar", mode);
                }

                // Delete
                Directory.Delete(this.File, true);
                this.File += ".tar";

                stopwatch.Stop();
            }

            return new Models.Common.ProcessResultModel()
            {
                Duartion = stopwatch.Elapsed
            };
        }

        /// <summary>
        /// Compress backup file
        /// </summary>
        /// <returns>Process results model</returns>
        public async Task<Models.Common.ProcessResultModel> CompressAsync()
        {
            return await Task.Run(() => this.Compress());
        }

        #endregion Prepare
    }
}