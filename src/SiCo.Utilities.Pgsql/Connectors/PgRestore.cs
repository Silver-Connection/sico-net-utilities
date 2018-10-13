using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace SiCo.Utilities.Pgsql.Connectors
{
    /// <summary>
    /// Run command via pg_restore application
    /// </summary>
    public class PgRestore : Models.Common.PgCommon<Models.PgConfig.PgRestoreModel>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PgRestore()
            : base()
        {
            this.Config = new Models.PgConfig.PgRestoreModel();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Default Settings</param>
        public PgRestore(Models.AppConfig.AppSettingsModel settings)
            : this()
        {
            this.LoadAppSettings(settings);
        }

        /// <summary>
        /// Skip validation during import
        /// </summary>
        [JsonIgnore]
        public bool SkipValidation { get; set; } = false;

        #region Prepare

        /// <summary>
        /// Clean user input
        /// </summary>
        public override void Clean()
        {
            base.Clean();
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

            this.Clean();

            var param = string.Empty;

            if (this.Config.Format == "p")
            {
                var psql = new Psql(this.AppSettings)
                {
                    File = this.File
                };
                return psql.GetPatameters(connection);
            }
            else
            {
                if (this.Config.SingleTransaction)
                {
                    param += " --single-transaction";
                }

                if (this.Config.ExitOnError)
                {
                    param += " --exit-on-error";
                }

                if (this.Config.List)
                {
                    param += " --list";
                }

                param += " \"" + this.File + "\"";
                return base.GetPatameters(connection) + param;
            }
        }

        /// <summary>
        /// Generate parameters to list content
        /// </summary>
        /// <returns>Parameters</returns>
        public string GetPatametersList()
        {
            this.Clean();
            if (this.Config.Format == "p")
            {
                return string.Empty;
            }

            string param = string.Format(" --format {0}", this.Config.Format);
            param += " --list";
            param += " \"" + this.File + "\"";
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
                //this.FilePath = this.AppSettings.Path.Export;
                this.Bin = this.AppSettings.Bin.Pg_Restore;
            }
        }

        #endregion Prepare

        #region Decompress

        /// <summary>
        /// Decompress backup file
        /// </summary>
        /// <returns>Process results model</returns>
        public Models.Common.ProcessResultModel Decompress()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            if (this.Config.Format == "d" && this.File.Contains(".tar"))
            {
                stopwatch.Start();

                // Decompress
                using (var stream = System.IO.File.OpenRead(this.File))
                using (var reader = ReaderFactory.Open(stream))
                {
                    var dest = new FileInfo(this.File);
                    var destPath = dest.FullName.Remove(dest.FullName.Length - dest.Extension.Length);

                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            reader.WriteEntryToDirectory(destPath, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                    this.File = destPath;
                }

                stopwatch.Stop();
            }

            return new Models.Common.ProcessResultModel()
            {
                Duartion = stopwatch.Elapsed
            };
        }

        /// <summary>
        /// Decompress backup file
        /// </summary>
        /// <returns>Process results model</returns>
        public async Task<Models.Common.ProcessResultModel> DecompressAsync()
        {
            return await Task.Run(() => this.Decompress());
        }

        #endregion Decompress
    }
}