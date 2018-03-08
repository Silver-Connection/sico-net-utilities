namespace SiCo.Utilities.Pgsql.Models.JobConfig
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SiCo.Utilities.Generics;

    /// <summary>
    /// Job Model
    /// </summary>
    public class JobModel
    {

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public JobModel()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Default settings</param>
        public JobModel(AppConfig.AppSettingsModel settings)
        {
            this.AppSettings = settings;
            this.Export = new Connectors.PgDump(settings);
            this.Import = new Connectors.PgRestore(settings);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Default settings</param>
        /// <param name="connect">Connection Settings</param>
        public JobModel(AppConfig.AppSettingsModel settings, Connection.BaseModel connect)
            : this(settings)
        {
            this.Connect = connect;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Job Name</param>
        /// <param name="settings">Default settings</param>
        /// <param name="connect">Connection Settings</param>
        public JobModel(string name, AppConfig.AppSettingsModel settings, Connection.BaseModel connect)
            : this(settings, connect)
        {
            this.Name = name;
        }

        #endregion Constructor

        #region Props

        /// <summary>
        /// Default Settings
        /// </summary>
        [JsonIgnore]
        public AppConfig.AppSettingsModel AppSettings { get; set; } = new AppConfig.AppSettingsModel();

        /// <summary>
        /// Connection informations
        /// </summary>
        [JsonProperty(Order = 20)]
        public Connection.BaseModel Connect { get; set; } = null;

        /// <summary>
        /// pg_dump connector
        /// </summary>
        [JsonProperty(Order = 30)]
        public Connectors.PgDump Export { get; set; }

        /// <summary>
        /// Files included
        /// </summary>
        [JsonProperty(Order = 50, NullValueHandling = NullValueHandling.Ignore)]
        public List<FilesModel> Files { get; set; } = new List<FilesModel>();

        /// <summary>
        /// pg_restore connector
        /// </summary>
        //[JsonIgnore]
        [JsonProperty(Order = 40, NullValueHandling = NullValueHandling.Ignore)]
        public Connectors.PgRestore Import { get; set; }

        /// <summary>
        /// Job Name
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Task Count
        /// </summary>
        [JsonIgnore]
        public int Tasks
        {
            get
            {
                if (this.Export.Tables == null || this.Export.Config.SingleFile)
                {
                    return 1;
                }
                else
                {
                    return this.Export.Tables.Count();
                }
            }
        }

        /// <summary>
        /// Job Version
        /// </summary>
        [JsonProperty(Order = 2)]
        public string Version { get; set; } = "3";

        /// <summary>
        /// Output connector's log
        /// </summary>
        [JsonIgnore]
        public bool Output { get; set; } = false;

        #endregion Props

        #region Load

        /// <summary>
        /// Load settings
        /// </summary>
        /// <param name="settings">Default settings</param>
        /// <param name="connect">Connection Settings</param>
        public void Load(AppConfig.AppSettingsModel settings, Connection.BaseModel connect = null)
        {
            this.AppSettings = settings;
            if (this.Export == null)
            {
                this.Export = new Connectors.PgDump(settings);
            }
            else
            {
                this.Export.LoadAppSettings(settings);
            }

            if (this.Import == null)
            {
                this.Import = new Connectors.PgRestore(settings);
            }
            else
            {
                this.Import.LoadAppSettings(settings);
            }


            if (connect != null)
            {
                this.Connect = connect;
            }
        }

        #endregion Load

        #region Log / CLI

        /// <summary>
        /// Create a overview
        /// </summary>
        /// <returns></returns>
        public string OverviewExport()
        {
            var log = FormatCli.KeyValue("Name", this.Name);
            log += FormatCli.KeyValue("Tasks", this.Tasks.ToString());
            log += FormatCli.KeyValue("File", this.Export.File);

            log += FormatCli.KeyValue("PostgreSQL Server", $"{this.Connect.Server}:{this.Connect.Port.ToString()}");
            log += FormatCli.KeyValue("PostgreSQL Database", this.Connect.Database);
            log += FormatCli.KeyValue("PostgreSQL User", this.Connect.User);
            log += FormatCli.KeyValue("PostgreSQL Password", !string.IsNullOrEmpty(this.Connect.Password));

            log += FormatCli.KeyValue("pg_dump Data only", this.Export.Config.DataOnly);
            log += FormatCli.KeyValue("pg_dump Schema only", this.Export.Config.SchemaOnly);
            log += FormatCli.KeyValue("pg_dump Format", this.Export.Config.Format);
            if (this.Export.Config.Format == "d")
            {
                log += FormatCli.KeyValue("pg_dump Worker", this.Export.Config.Worker);
                log += FormatCli.KeyValue("pg_dump Tar export", this.Export.Config.TarDir);
            }

            if (this.Export.Tables?.Count() > 0)
            {
                log += FormatCli.KeyValue("pg_dump Single file", this.Export.Config.SingleFile);
                log += FormatCli.KeyValue("pg_dump Tables", string.Join(", ", this.Export.Tables));
            }
            if (this.Export.TablesExclude?.Count() > 0)
            {
                log += FormatCli.KeyValue("pg_dump Tables exclude", string.Join(", ", this.Export.TablesExclude));
            }

            return log;
        }

        #endregion Log / CLI

        #region Export

        /// <summary>
        /// Dry run export tasks and only get parameters
        /// </summary>
        /// <param name="start">Job start function</param>
        /// <param name="task">Task run function</param>
        /// <param name="end">Job end function</param>
        /// <param name="streamLog">Stream output</param>
        /// <returns>Task</returns>
        public async Task ExportParamsAsync(Action<string> start, Action<string, int> task, Action<string> end, bool streamLog = false)
        {
            if (this.Tasks == 0)
            {
                end("No task to process");
                return;
            }

            Func<string> prepare = () =>
            {
                // Prepare
                this.ExportPrepare();

                // Write Log
                string result = FormatCli.H1("Job");
                result += this.OverviewExport();

                return result;
            };

            await this.TaskRun(prepare, this.Tasks, this._ExportParamsAsync, start, task, end, streamLog);
        }

        /// <summary>
        /// Prepare Export and get tasks
        /// </summary>
        public void ExportPrepare()
        {
            // Table
            if (this.Export.Tables != null)
            {
                if (this.Tasks == 1 || (this.Tasks > 1 && !this.Export.Config.SingleFile))
                {
                    this.Export.CurrentRun = 0;
                    this.Export.CurrentTable = this.Export.Tables.FirstOrDefault();
                }
            }

            this.Export.SetFileName(this.Name, this.Connect);
        }

        /// <summary>
        /// Run export tasks
        /// </summary>
        /// <param name="start">Job start function</param>
        /// <param name="task">Task run function</param>
        /// <param name="end">Job end function</param>
        /// <param name="streamLog">Stream output</param>
        /// <returns>Task</returns>
        public async Task ExportRunAsync(Action<string> start, Action<string, int> task, Action<string> end, bool streamLog = false)
        {
            if (this.Tasks == 0)
            {
                end("No task to process");
                return;
            }

            Func<string> prepare = () =>
            {
                // Prepare
                this.ExportPrepare();

                // Write Log
                string result = FormatCli.H1("Job");
                result += this.OverviewExport();

                return result;
            };

            await this.TaskRun(prepare, this.Tasks, this._ExportRunTaskAsync, start, task, end, streamLog);
        }

        /// <summary>
        /// Prepare Task
        /// </summary>
        public void ExportTaskPrepare()
        {
            // Table
            if (this.Export.Tables != null)
            {
                if (this.Tasks > 1
                    && !this.Export.Config.SingleFile
                    && this.Export.CurrentRun <= (this.Tasks - 1))
                {
                    this.Export.CurrentRun++;
                    this.Export.CurrentTable = this.Export.Tables.ElementAtOrDefault(this.Export.CurrentRun);
                }
            }

            this.Export.SetFileName(this.Name, this.Connect);
        }

        internal async Task<string> _ExportParamsAsync(int i)
        {
            return await Task.Run(() => this._ExportParams(i));
        }

        private string _ExportParams(int i)
        {
            // Write Log
            string t = FormatCli.H2($"Task: { (i + 1).ToString() }");
            t += FormatCli.KeyValue("File", this.Export.FileName);
            t += FormatCli.H3("Parameter");
            t += $"{ this.Export.BinName } { this.Export.GetPatameters(this.Connect) }" + Environment.NewLine;

            this.ExportTaskPrepare();

            return t;
        }

        private async Task<string> _ExportRunTaskAsync(int i)
        {
            var path = System.IO.Path.GetDirectoryName(this.Export.File);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            // ExportRunAsync
            var task = await this.Export.RunAsync(this.Connect, this.Output);

            // Compress export
            var compress = await this.Export.CompressAsync();
            task.Duartion.Add(compress.Duartion);

            // Add to files
            var exported = new FilesModel(this.Export.File, task);
            this.Files.Add(exported);

            // Write log JSON
            if (this.Tasks == 1 || this.Tasks == i + 1)
            {
                var logName = this.Export.File;
                if (this.Tasks != 1)
                {
                    logName = this.Export.File.Replace(this.Export.Tables.Last(), "export");
                }

                this.Import.Config = new PgConfig.PgRestoreModel(this.Export.Config);
                this.JobSave(logName + ".json");
            }

            // Write Log
            string t = FormatCli.H2($"Task: { (i + 1).ToString() }");
            t += FormatCli.KeyValue("File", this.Export.FileName);
            t += FormatCli.KeyValue("Duration", task.Duartion.TotalMinutes + " min");
            t += FormatCli.KeyValue("Checksum", exported.Checksum);
            if (this.Export.Config.Verbose)
            {
                t += FormatCli.H3("Parameter");
                t += $"{ this.Export.BinName } { this.Export.GetPatameters(this.Connect) }" + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(task.Ouput))
            {
                t += FormatCli.H3("Output:");
                t += task.Ouput;
            }

            if (!string.IsNullOrEmpty(task.Error))
            {
                t += FormatCli.H3("Error:");
                t += task.Error;
            }

            this.ExportTaskPrepare();

            return t;
        }

        #endregion Export

        #region Import

        /// <summary>
        /// Dry run export tasks and only get parameters
        /// </summary>
        /// <param name="start">Job start function</param>
        /// <param name="task">Task run function</param>
        /// <param name="end">Job end function</param>
        /// <param name="streamLog">Stream output</param>
        /// <returns>Task</returns>
        public async Task ImportParamsAsync(Action<string> start, Action<string, int> task, Action<string> end, bool streamLog = false)
        {
            if (this.Files?.Count == 0)
            {
                end("No task to process");
                return;
            }

            Func<string> prepare = () =>
            {
                // Write Log
                string result = FormatCli.H1("Job");
                result += this.OverviewExport();

                return result;
            };

            await this.TaskRun(prepare, this.Files.Count, this._ImportParamsAsync, start, task, end, streamLog);
        }

        /// <summary>
        /// Run Import tasks
        /// </summary>
        /// <param name="start">Job start function</param>
        /// <param name="task">Task run function</param>
        /// <param name="end">Job end function</param>
        /// <param name="streamLog">Stream output</param>
        /// <returns>Task</returns>
        public async Task ImportRunAsync(Action<string> start, Action<string, int> task, Action<string> end, bool streamLog = false)
        {
            if (this.Files == null || this.Files.Count == 0)
            {
                end("No task to process");
                return;
            }

            Func<string> prepare = () =>
            {
                // Write Log
                string result = FormatCli.H1("Job");
                result += this.OverviewExport();

                return result;
            };

            await this.TaskRun(prepare, this.Files.Count, this._ImportRunTaskAsync, start, task, end, streamLog);
        }

        internal async Task<string> _ImportParamsAsync(int i)
        {
            return await Task.Run(() => this._ImportParams(i));
        }

        private string _ImportParams(int i)
        {
            var file = this.Files.ElementAtOrDefault(i);

            // Write Log
            string t = FormatCli.H2($"Task: { (i + 1).ToString() }");
            t += FormatCli.KeyValue("File", file.File);
            t += FormatCli.KeyValue("Checksum", file.Checksum);

            //Checks
            var path = DirFile.Combine(this.AppSettings.Path.Export, file.File);
            if (!System.IO.File.Exists(path))
            {
                t += Environment.NewLine;
                t += $"Error: Could not find file {path}";
                return t;
            }
            this.Import.File = path;

            t += FormatCli.H3("Parameter");
            var connector = this.Import.Config.Format == "p" ? new Connectors.Psql(AppSettings).BinName : this.Import.BinName;
            t += $"{ connector } { this.Import.GetPatameters(this.Connect) }" + Environment.NewLine;

            return t;
        }

        private async Task<string> _ImportRunTaskAsync(int i)
        {
            var file = this.Files.ElementAtOrDefault(i);

            // Write Log
            string t = FormatCli.H2($"Task: { (i + 1).ToString() }");
            t += FormatCli.KeyValue("File", file.File);
            t += FormatCli.KeyValue("Checksum", file.Checksum);

            //Checks
            var path = DirFile.Combine(this.AppSettings.Path.Export, file.File);
            if (!System.IO.File.Exists(path))
            {
                t += Environment.NewLine;
                t += $"Error: Could not find file {path}";
                return t;
            }
            this.Import.File = path;

            // Validate
            if (this.Import.SkipValidation)
            {
                t += FormatCli.KeyValue("Status", "Skipped!");
            }
            else
            {
                var checksum = await Task.Run(() => FilesModel.Hash(path));
                if (checksum != file.Checksum)
                {
                    t += Environment.NewLine;
                    t += $"Error: Checksum is not identical, backup maybe corrupted!";
                    return t;
                }

                t += FormatCli.KeyValue("Status", "Valid!");
            }

            // Decompress 
            var decompress = await this.Import.DecompressAsync();

            // Import
            var task = await this.Import.RunAsync(this.Connect, this.Output);
            task.Duartion.Add(decompress.Duartion);

            t += FormatCli.KeyValue("Duration", task.Duartion.TotalMinutes + " min");
            if (this.Import.Config.Verbose)
            {
                t += FormatCli.H3("Parameter");
                var connector = this.Import.Config.Format == "p" ? new Connectors.Psql(AppSettings).BinName : this.Import.BinName;
                t += $"{ connector } { this.Import.GetPatameters(this.Connect) }" + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(task.Ouput))
            {
                t += FormatCli.H3("Output:");
                t += task.Ouput;
            }

            if (!string.IsNullOrEmpty(task.Error))
            {
                t += FormatCli.H3("Error:");
                t += task.Error;
            }

            return t;
        }

        #endregion Import

        #region Validate

        /// <summary>
        /// Run export tasks
        /// </summary>
        /// <param name="start">Job start function</param>
        /// <param name="task">Task run function</param>
        /// <param name="end">Job end function</param>
        /// <param name="streamLog">Stream output</param>
        /// <returns>Task</returns>
        public async Task ValidateRunAsync(Action<string> start, Action<string, int> task, Action<string> end, bool streamLog = false)
        {
            if (this.Files?.Count == 0)
            {
                end("No task to process");
                return;
            }

            Func<string> prepare = () =>
            {
                // Write Log
                string result = FormatCli.H2("Job");
                result += this.OverviewExport();

                return result;
            };

            await this.TaskRun(prepare, this.Files.Count, this._ValidateRunAsync, start, task, end, streamLog);
        }

        private async Task<string> _ValidateRunAsync(int i)
        {
            var file = this.Files.ElementAtOrDefault(i);

            // Write Log
            string t = FormatCli.H2($"Task: { (i + 1).ToString() }");
            t += FormatCli.KeyValue("File", file.File);
            t += FormatCli.KeyValue("Checksum", file.Checksum);

            //Checks
            var path = DirFile.Combine(this.AppSettings.Path.Export, file.File);
            if (!System.IO.File.Exists(path))
            {
                t += Environment.NewLine;
                t += $"Error: Could not find file {path}";
                return t;
            }

            var checksum = await Task.Run(() => FilesModel.Hash(path));
            if (checksum != file.Checksum)
            {
                t += Environment.NewLine;
                t += $"Error: Checksum is not identical, backup maybe corrupted!";
                return t;
            }

            t += FormatCli.KeyValue("Status", "Valid!");

            return t;
        }

        #endregion Validate

        /// <summary>
        /// Run Task
        /// </summary>
        /// <param name="tstart">Function called when job start</param>
        /// <param name="tasks"></param>
        /// <param name="trun"></param>
        /// <param name="start"></param>
        /// <param name="task"></param>
        /// <param name="end"></param>
        /// <param name="streamLog"></param>
        /// <returns></returns>
        public async Task TaskRun(
              Func<string> tstart,
              int tasks,
              Func<int, Task<string>> trun,
              Action<string> start,
              Action<string, int> task,
              Action<string> end,
              bool streamLog = false)
        {
            string result = tstart();

            // ExportRunAsync start
            start(result);

            // Start watch
            var timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < tasks; i++)
            {
                // Run Task
                string t = Environment.NewLine;
                t += await trun(i);

                // ExportRunAsync task
                if (streamLog)
                {
                    task(t, i);
                }
                else
                {
                    result += t;
                    task(result, i);
                }
            }

            // Stop watch
            timer.Stop();

            // Write Log
            string e = Environment.NewLine;
            e += FormatCli.KeyValue("Total Duration", timer.ElapsedMilliseconds + " ms");

            // ExportRunAsync task
            if (streamLog)
            {
                end(e);
            }
            else
            {
                result += e;
                end(result);
            }
        }

        #region Read / Save

        /// <summary>
        /// List all job JSON files in Env.Path.jobs
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>List of jobs</returns>
        public static IEnumerable<string> JobsList(string path)
        {
            var res = DirFile.ListFiles(path, "*.json");
            if (res == null)
            {
                return new string[] { };
            }

            return res;
        }

        /// <summary>
        /// Load JSON file
        /// </summary>
        /// <param name="file">Filename</param>
        /// <returns>Model</returns>
        public static JobModel JobLoad(string file)
        {
            if (!System.IO.File.Exists(file))
            {
                return null;
            }

            // Read file
            var jobJson = System.IO.File.ReadAllText(file);
            return JsonConvert.DeserializeObject<JobModel>(jobJson, new JsonSerializerSettings()
            {
                MaxDepth = 100,
            });
        }

        /// <summary>
        /// Save job to file
        /// </summary>
        /// <param name="file">File path</param>
        /// <param name="excludeFiles">Exclude file in JSON</param>
        public void JobSave(string file, bool excludeFiles = false)
        {
            this.Version = "3";

            var files = this.Files.ToArray();
            var import = this.Import;
            if (excludeFiles)
            {
                this.Files = null;
                this.Import = null;
            }

            var json = JsonConvert.SerializeObject(this, Formatting.Indented);

            if (excludeFiles)
            {
                this.Files = files.ToList();
                this.Import = import;
            }

            System.IO.File.WriteAllText(file, json);
        }

        #endregion Read / Save
    }
}