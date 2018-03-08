namespace SiCo.Utilities.CSV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Generics;

    /// <summary>
    /// CSV Reader
    /// </summary>
    public class Reader
    {
        #region File

        /// <summary>
        /// Read a given file and pass it's content to the parser
        /// </summary>
        /// <typeparam name="TModel">Return model</typeparam>
        /// <param name="worker">Background worker for reporting</param>
        /// <param name="e">Worker arguments</param>
        /// <param name="file">Path to file</param>
        /// <param name="converter">Converter function for transforming file content</param>
        /// <param name="opts">Parser options</param>
        /// <returns>List of TModel</returns>
        public static Result<TModel> FileRead<TModel>(
            BackgroundWorker worker,
            DoWorkEventArgs e,
            string file,
            Func<string[], TModel> converter,
            Options opts)
        {
            // check parameter
            if (StringExtensions.IsEmpty(file)
             || opts == null
             || !File.Exists(file))
            {
                return null;
            }

            // report to worker
            if (worker != null)
            {
                worker.ReportProgress(1, "Read file...");
            }

            var timer = new Stopwatch();
            string[] lines = new string[] { };

            timer.Start();
            if (opts.Lines > 0)
            {
                using (
#if NETSTANDARD1_6
                    var stream = new StreamReader(File.Open(file, FileMode.Open), opts.Encoding)
#else
                    var stream = new StreamReader(file, opts.Encoding)
#endif
                    )
                {
                    var list = new List<string>(opts.Lines);
                    int i = 0;
                    var line = string.Empty;
                    while ((line = stream.ReadLine()) != null)
                    {
                        if (i >= opts.Lines)
                        {
                            break;
                        }

                        list.Add(line);
                        i++;
                    }

                    stream.Dispose();
                    lines = list.ToArray();
                }
            }
            else
            {
                lines = File.ReadAllLines(file, opts.Encoding).ToArray();
            }

            timer.Stop();

            var res = ProcessString<TModel>(lines, converter, opts);
            res.File = file;
            res.TimeRead = timer.Elapsed;
            res.CreateLog();

            return res;
        }

        /// <summary>
        /// Read a given file and pass it's content to the parser
        /// </summary>
        /// <typeparam name="TModel">Return model</typeparam>
        /// <param name="file">Path to file</param>
        /// <param name="converter">Converter function for transforming file content</param>
        /// <param name="opts">Parser options</param>
        /// <returns>List of TModel</returns>
        public static Result<TModel> FileRead<TModel>(string file, Func<string[], TModel> converter, Options opts)
        {
            return FileRead<TModel>(null, null, file, converter, opts);
        }

        /// <summary>
        /// Read a given file and pass it's content to the parser. ASYNC
        /// </summary>
        /// <typeparam name="TModel">Return model</typeparam>
        /// <param name="file">Path to file</param>
        /// <param name="converter">Converter function for transforming file content</param>
        /// <param name="opts">Parser options</param>
        /// <returns>List of TModel</returns>
        public static async Task<Result<TModel>> FileReadAsync<TModel>(string file, Func<string[], TModel> converter, Options opts)
        {
            return await Task.Run(() => FileRead<TModel>(file, converter, opts));
        }

        #endregion File

        #region Text

        /// <summary>
        /// Parse array of string and pass each line to converter
        /// </summary>
        /// <typeparam name="TModel">Return model</typeparam>
        /// <param name="worker">Background worker for reporting</param>
        /// <param name="e">Worker arguments</param>
        /// <param name="lines">Lines to process</param>
        /// <param name="converter">Converter function for transforming file content</param>
        /// <param name="opts">Parser options</param>
        /// <returns>List of TModel</returns>
        public static Result<TModel> ProcessString<TModel>(
            BackgroundWorker worker,
            DoWorkEventArgs e,
            string[] lines,
            Func<string[], TModel> converter,
            Options opts)
        {
            if (lines == null)
            {
                return null;
            }

            // report to worker
            if (worker != null)
            {
                worker.ReportProgress(35, "Parse text...");
            }

            var header = ProcessLine(lines[0], opts)
                .Select((x, i) => new KeyValuePair<int, string>(i, x))
                .ToArray();

            if (opts.Head)
            {
                lines = lines.Skip(1).ToArray();
            }

            if (opts.Lines > 0)
            {
                lines = lines.Take(opts.Lines).ToArray();
            }

            var timer = new Stopwatch();
            var transformed = new TModel[lines.Length];
            var raw = new string[lines.Length][];
            var res = new Result<TModel>(string.Empty, opts);

            timer.Start();
            Parallel.For(
                0,
                lines.Length,
                x =>
                {
                    try
                    {
                        var tmp = ProcessLine(lines[x], opts);
                        transformed[x] = converter(tmp);
                        raw[x] = tmp;
                    }
                    catch (Exception ex)
                    {
                        ////throw new Exception(x.ToString(), ex);
                        res.Error += string.Format(Formats.Error, x.ToString(), ex.ToString());
                    }
                });
            timer.Stop();

            // Get transformer model
            var ttrans = converter.GetInvocationList().FirstOrDefault();
            if (ttrans != null
                && typeof(Transformers.IBaseModel).IsAssignableFrom(ttrans.Target.GetType()))
            {
                ////var t = (new TModel() as Transformers.IBaseModel).GetType();
                res.Processor = ttrans.Target as Transformers.IBaseModel;
            }

            res.Header = header;
            res.Raw = raw;
            res.Transformed = transformed;
            res.TimeTransform = timer.Elapsed;
            res.CreateLog();
            return res;
        }

        /// <summary>
        /// Parse array of string and pass each line to converter
        /// </summary>
        /// <typeparam name="TModel">Return model</typeparam>
        /// <param name="lines">Lines to process</param>
        /// <param name="converter">Converter function for transforming file content</param>
        /// <param name="opts">Parser options</param>
        /// <returns>List of TModel</returns>
        public static Result<TModel> ProcessString<TModel>(string[] lines, Func<string[], TModel> converter, Options opts)
        {
            return ProcessString<TModel>(null, null, lines, converter, opts);
        }

        /// <summary>
        /// Parse array of string and pass each line to converter ASYNC
        /// </summary>
        /// <typeparam name="TModel">Return model</typeparam>
        /// <param name="lines">Lines to process</param>
        /// <param name="converter">Converter function for transforming file content</param>
        /// <param name="opts">Parser options</param>
        /// <returns>List of TModel</returns>
        public static async Task<Result<TModel>> ProcessStringAsync<TModel>(string[] lines, Func<string[], TModel> converter, Options opts)
        {
            return await Task.Run(() => ProcessString<TModel>(lines, converter, opts));
        }

        #endregion Text

        #region Line

        /// <summary>
        /// Parse string and cut it to an array
        /// </summary>
        /// <param name="line">Line to process</param>
        /// <param name="opts">Parser options</param>
        /// <returns>List of strings</returns>
        public static string[] ProcessLine(string line, Options opts)
        {
            if (Generics.StringExtensions.IsEmpty(line))
            {
                return null;
            }

            var delimiters = opts.Delimiters;
            string cleanLine = line;

            if (!string.IsNullOrEmpty(opts.Quoted))
            {
                int offset = 0;
                Parallel.For(
                    0,
                    opts.Delimiters.Length,
                    x =>
                    {
                        offset = opts.Delimiters[x].Length + opts.Quoted.Length;
                        var qs = line.IndexOfAll(opts.Delimiters[x] + opts.Quoted).ToArray();

                        for (int i = 0; i < qs.Length; i++)
                        {
                            string original = line.Substring(qs[i] + offset);
                            if (!string.IsNullOrEmpty(original) && !string.IsNullOrWhiteSpace(original))
                            {
                                int f = original.IndexOf(opts.Quoted);
                                if (f > -1)
                                {
                                    original = original.Substring(0, f);
                                }

                                string clean = original.Replace(opts.Delimiters[x], "T1d2X3");

                                cleanLine = cleanLine.Replace(original, clean);
                            }
                        }

                        cleanLine = cleanLine.Replace(opts.Delimiters[x], "^").Replace("T1d2X3", opts.Delimiters[x]);
                    });

                cleanLine = cleanLine.Replace(opts.Quoted, string.Empty);
                delimiters = new string[] { "^" };
            }

            var r = cleanLine.Split(delimiters, StringSplitOptions.None);

            return r;
        }

        #endregion Line
    }
}