namespace SiCo.Utilities.Pgsql.Models.Common
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Generics;
    using Newtonsoft.Json;

    /// <summary>
    /// Base Model for running SQL tools
    /// </summary>
    public abstract class PostgreBaseModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PostgreBaseModel()
        {
            this.BinName = string.Empty;
            this.BinPath = string.Empty;
            this.FileName = string.Empty;
            this.FilePath = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Default Settings</param>
        public PostgreBaseModel(AppConfig.AppSettingsModel settings)
            : this()
        {
            this.LoadAppSettings(settings);
        }

        /// <summary>
        /// App Settings
        /// </summary>
        [JsonIgnore]
        public AppConfig.AppSettingsModel AppSettings { get; set; } = new AppConfig.AppSettingsModel();

        /// <summary>
        /// Path to application binary
        /// </summary>
        [JsonIgnore]
        public string Bin
        {
            get
            {
                return DirFile.Combine(this.BinPath, this.BinName);
            }

            set
            {
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    this.BinName = System.IO.Path.GetFileName(value);
                    this.BinPath = System.IO.Path.GetDirectoryName(value);
                }
            }
        }

        /// <summary>
        /// Application Binary Name
        /// </summary>
        [JsonIgnore]
        public string BinName { get; set; }

        /// <summary>
        /// Application binary root path
        /// </summary>
        [JsonIgnore]
        public string BinPath { get; set; }

        /// <summary>
        /// File Path
        /// </summary>
        //[JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        [JsonIgnore]
        public string File
        {
            get
            {
                return DirFile.Combine(this.FilePath, this.FileName);
            }

            set
            {
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    this.FileName = System.IO.Path.GetFileName(value);
                    this.FilePath = System.IO.Path.GetDirectoryName(value);
                }
            }
        }

        /// <summary>
        /// File Names
        /// </summary>
        [JsonIgnore]
        public string FileName { get; set; }

        /// <summary>
        /// File root path
        /// </summary>
        [JsonIgnore]
        public string FilePath { get; set; }

        #region Prepare

        /// <summary>
        /// Clean user input
        /// </summary>
        public virtual void Clean()
        {
            this.BinName = this.BinName.TrimFile();
            this.BinPath = this.BinPath.TrimPath();
            this.FileName = this.FileName.TrimFile();
            this.FilePath = this.FilePath.TrimPath();
        }

        /// <summary>
        /// Create connection string
        /// </summary>
        /// <param name="connection">Connection Model</param>
        /// <returns></returns>
        public string ConnectionString(Connection.IBaseModel connection)
        {
            if (connection == null)
            {
                return string.Empty;
            }

            var con = string.Format(
                "--host {0} --port {2} --username \"{1}\"",
               connection.Server,
               connection.User,
               connection.Port);

            if (!string.IsNullOrEmpty(connection.Database) && !string.IsNullOrWhiteSpace(connection.Database))
            {
                con += " --dbname \"" + connection.Database + "\"";
            }

            if (string.IsNullOrEmpty(connection.Password) && string.IsNullOrWhiteSpace(connection.Password))
            {
                con += " --no-password";
            }

            return con;
        }

        /// <summary>
        /// Create parameters
        /// </summary>
        /// <param name="connection">Connection Model</param>
        /// <returns></returns>
        public virtual string GetPatameters(Connection.IBaseModel connection)
        {
            return this.ConnectionString(connection);
        }

        /// <summary>
        /// Load default settings
        /// </summary>
        /// <param name="settings"></param>
        public virtual void LoadAppSettings(AppConfig.AppSettingsModel settings)
        {
            if (settings != null)
            {
                this.AppSettings = settings;
            }

            if (this.AppSettings != null)
            {
                this.FilePath = this.AppSettings.Path.Export;
                this.Bin = this.AppSettings.Bin.Psql;
            }
        }

        #endregion Prepare

        #region Run

        /// <summary>
        /// Run application with given parameters
        /// </summary>
        /// <param name="connection">Connection Model</param>
        /// <param name="output">display output</param>
        /// <returns></returns>
        public ProcessResultModel Run(Connection.IBaseModel connection, bool output = false)
        {
            if (connection == null)
            {
                return null;
            }

            try
            {
                using (Process p = this.CreateProcess(output))
                {
                    var timer = new Stopwatch();

                    // Encoding
                    System.Environment.SetEnvironmentVariable("PGCLIENTENCODING", connection.Encoding);
                    ////p.StartInfo.EnvironmentVariables.Add("PGCLIENTENCODING", connection.Encoding);

                    // Password
                    if (!StringExtensions.IsEmpty(connection.Password))
                    {
                        // Set password as env var
                        System.Environment.SetEnvironmentVariable("PGPASSWORD", connection.Password);
                        ////p.StartInfo.EnvironmentVariables.Add("PGPASSWORD", connection.Password);
                    }

                    p.StartInfo.Arguments = this.GetPatameters(connection);

                    timer.Start();
                    p.Start();

                    string res = string.Empty;
                    string error = string.Empty;
                    if (output)
                    {
                        res = p.StandardOutput.ReadToEnd();
                        error = p.StandardError.ReadToEnd();
                    }
                    else
                    {
                        error = p.StandardError.ReadToEnd();
                    }

                    p.WaitForExit();
                    timer.Stop();

                    return new ProcessResultModel()
                    {
                        Duartion = timer.Elapsed,
                        Error = error,
                        Ouput = res,
                    };
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Run application with given parameters
        /// </summary>
        /// <param name="connection">Connection Model</param>
        /// <param name="output">display output</param>
        /// <returns></returns>
        public virtual async Task<ProcessResultModel> RunAsync(Connection.IBaseModel connection, bool output = false)
        {
            return await Task.Run(() => this.Run(connection, output));
        }

        internal Process CreateProcess(bool output = false)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;

            if (output)
            {
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
            }
            else
            {
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.RedirectStandardError = true;
            }

            p.StartInfo.WorkingDirectory = this.BinPath;
            p.StartInfo.FileName = this.Bin;
            //p.StartInfo.WindowStyle = ProcessWindowStyle.Hidde;

            return p;
        }

        #endregion Run
    }
}