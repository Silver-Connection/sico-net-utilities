namespace SiCo.Utilities.CSV
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Result Model
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class Result<TModel> : SiCo.Utilities.CSV.IResult<TModel>
    {
        /// <summary>
        /// Init
        /// </summary>
        public Result()
        {
            this.File = string.Empty;
            this.Log = string.Empty;
            this.Options = new Options();
            this.Error = string.Empty;

            this.Header = new KeyValuePair<int, string>[] { };
            this.Raw = new List<string[]>();

            this.TimeRead = new TimeSpan();
            this.TimeTransform = new TimeSpan();

            this.Transformed = new List<TModel>();
            this.CacheText = string.Empty;
            this.CacheJson = string.Empty;
            this.CacheSql = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="file">Path to CSV File</param>
        /// <param name="options">CSV Options</param>
        public Result(string file, Options options)
            : this()
        {
            this.File = file;
            this.Options = options;
        }

        #region General

        /// <summary>
        /// Error Log
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Path to CSV File
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Has Errors
        /// </summary>
        public bool HasError
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Error))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Log
        /// </summary>
        public string Log { get; set; }

        /// <summary>
        /// CSV Options
        /// </summary>
        public Options Options { get; set; }

        /// <summary>
        /// Duration for file read
        /// </summary>
        public TimeSpan TimeRead { get; set; }

        /// <summary>
        /// Duration for CSV transformation
        /// </summary>
        public TimeSpan TimeTransform { get; set; }

        #endregion General

        #region Raw

        /// <summary>
        /// RAW CSV Header
        /// </summary>
        public IEnumerable<KeyValuePair<int, string>> Header { get; set; }

        /// <summary>
        /// RAW CSV Text
        /// </summary>
        public IEnumerable<string[]> Raw { get; set; }

        #endregion Raw

        #region Transformed

        /// <summary>
        /// Cached CSV
        /// </summary>
        public string CacheCsv { get; set; }

        /// <summary>
        /// Cached JSON String
        /// </summary>
        public string CacheJson { get; set; }

        /// <summary>
        /// Cached Report
        /// </summary>
        public string CacheReport { get; set; }

        /// <summary>
        /// Cached SQL Query
        /// </summary>
        public string CacheSql { get; set; }

        /// <summary>
        /// Cached Text
        /// </summary>
        public string CacheText { get; set; }

        /// <summary>
        /// Processor used
        /// </summary>
        public Transformers.IBaseModel Processor { get; set; }

        /// <summary>
        /// List of Transformed CSV data. Each Model is one line
        /// </summary>
        public IEnumerable<TModel> Transformed { get; set; }

        #endregion Transformed

        /// <summary>
        /// Is cached
        /// </summary>
        public bool IsCached
        {
            get
            {
                if (this.Transformed != null && this.Transformed.Count() > 0)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Is String
        /// </summary>
        public bool IsString
        {
            get
            {
                if (this.Processor == null)
                {
                    return true;
                }

                return false;
            }
        }

        #region Helper

        /// <summary>
        /// Generate Text
        /// </summary>
        /// <returns></returns>
        public string GenerateText()
        {
            if (this.HasError)
            {
                return this.Error;
            }

            if (this.Transformed != null && this.Transformed.Count() > 0)
            {
                if (this.Processor != null)
                {
                    if (!string.IsNullOrEmpty(this.CacheReport))
                    {
                        return string.Format(Formats.H2, "Report") + this.CacheReport;
                    }

                    return string.Format(Formats.H2, "Transformed") + this.CacheJson;
                }
                else
                {
                    return string.Format(Formats.H2, "Transformed") + this.CacheText;
                }
            }

            return string.Empty;
        }

        #endregion Helper

        #region Transformers

        /// <summary>
        /// Run all transformations
        /// </summary>
        public void Complete()
        {
            this.CreateLog();
            this.CreateReport();

            if (this.Processor != null)
            {
                this.CreateCsv();
                this.CreateJson();
                this.CreateSql();
            }
            else
            {
                this.CreateText();
            }
        }

        /// <summary>
        /// Create CSV
        /// </summary>
        public void CreateCsv()
        {
            if (this.Transformed != null && this.Transformed.Count() > 0 && this.Processor != null)
            {
                var list = new List<Transformers.IBaseModel>(this.Transformed.Count());
                foreach (var item in this.Transformed)
                {
                    list.Add(item as Transformers.IBaseModel);
                }

                this.CacheCsv = Common.GetCsv(list, this.Options);
            }
        }

        /// <summary>
        /// Create JSON String
        /// </summary>
        public void CreateJson()
        {
            if (this.Transformed != null && this.Transformed.Count() > 0 && this.Processor != null)
            {
                var list = new List<Transformers.IBaseModel>(this.Transformed.Count());
                foreach (var item in this.Transformed)
                {
                    list.Add(item as Transformers.IBaseModel);
                }

                this.CacheJson = Common.GetJson(list);
            }
        }

        /// <summary>
        /// Create Log
        /// </summary>
        public void CreateLog()
        {
            string r = string.Format(Formats.H1, "CSV Reader") + Environment.NewLine;
            r += string.Format(Formats.H2, "Details");
            if (!string.IsNullOrEmpty(this.File))
            {
                r += string.Format(Formats.KeyVal, "File", this.File);
            }

            if (this.Transformed != null && this.Transformed.Count() > 0 && this.Processor != null)
            {
                r += string.Format(Formats.KeyVal, "Transformer", this.Processor.TrDisplayName);
            }

            r += string.Format(Formats.KeyVal, "Lines [#]", this.Raw.Count());

            if (!string.IsNullOrEmpty(this.File))
            {
                r += string.Format(Formats.KeyVal, "Read [ms]", this.TimeRead.TotalMilliseconds);
            }

            r += string.Format(Formats.KeyVal, "Transform [ms]", this.TimeTransform.TotalMilliseconds) + Environment.NewLine;
            this.Log = r;
        }

        /// <summary>
        /// Create Report
        /// </summary>
        public void CreateReport()
        {
            if (this.HasError)
            {
                return;
            }

            if (this.Transformed != null && this.Transformed.Count() > 0 && this.Processor != null)
            {
                var t1 = this.Transformed as IEnumerable<Transformers.IBaseModel>;
                this.CacheReport = this.Processor.GetReport(this.Transformed as IEnumerable<object>);
            }
        }

        /// <summary>
        /// Cerate SQL Inserts
        /// </summary>
        public void CreateSql()
        {
            if (this.Transformed != null && this.Transformed.Count() > 0 && this.Processor != null)
            {
                var list = new List<Transformers.IBaseModel>(this.Transformed.Count());
                foreach (var item in this.Transformed)
                {
                    list.Add(item as Transformers.IBaseModel);
                }

                this.CacheSql = Common.GetSql(list);
            }
        }

        /// <summary>
        /// Create Text
        /// </summary>
        public void CreateText()
        {
            if (this.Transformed != null && this.Transformed.Count() > 0 && this.Processor == null)
            {
                this.CacheText = string.Join(string.Empty, this.Transformed);
            }
        }

        #endregion Transformers

        #region Write-File

        /// <summary>
        /// Write CSV File
        /// </summary>
        /// <param name="file"></param>
        public void WriteCsv(string file)
        {
            Common.WriteFile(this.CacheCsv, file, ".csv");
        }

        /// <summary>
        /// Write JSON File
        /// </summary>
        /// <param name="file"></param>
        public void WriteJson(string file)
        {
            Common.WriteFile(this.CacheJson, file, ".json");
        }

        /// <summary>
        /// Write SQL File
        /// </summary>
        /// <param name="file"></param>
        public void WriteSql(string file)
        {
            Common.WriteFile(this.CacheSql, file, ".sql");
        }

        /// <summary>
        /// Write Text File
        /// </summary>
        /// <param name="file"></param>
        public void WriteText(string file)
        {
            Common.WriteFile(this.CacheText, file, ".txt");
        }

        #endregion Write-File
    }
}