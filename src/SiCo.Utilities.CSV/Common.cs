namespace SiCo.Utilities.CSV
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Generics;
    using Newtonsoft.Json;

    /// <summary>
    /// Common Helpers
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Create a model. Search for a constructor which accepts dbmodel
        /// </summary>
        /// <typeparam name="TWeb">Output type</typeparam>
        /// <param name="dbmodel">Object to pass to the model constructor</param>
        /// <returns>Display string</returns>
        public static TWeb CreateModel<TWeb>(object dbmodel)
            where TWeb : Transformers.IBaseModel, new()
        {
            try
            {
                // Try to get constructor
                var c = typeof(TWeb).GetConstructor(new Type[] { dbmodel.GetType() });

                // Create model
                TWeb m = (TWeb)c.Invoke(new object[] { dbmodel });

                return m;
            }
            catch (Exception e)
            {
                throw new Exception("Could not find constructor for " + typeof(TWeb).FullName, e);
            }
        }

        /// <summary>
        /// Write File
        /// </summary>
        /// <param name="text">File Content</param>
        /// <param name="file">File Name</param>
        /// <param name="extension">File Type Extension</param>
        public static void WriteFile(string text, string file, string extension)
        {
            if (string.IsNullOrEmpty(file)
                || string.IsNullOrWhiteSpace(file)
                || string.IsNullOrEmpty(text)
                || string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            file = file.TrimEnd(extension);
            var path = System.IO.Path.GetDirectoryName(file);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            System.IO.File.WriteAllText(file + extension, text, System.Text.Encoding.UTF8);
        }

        #region Transformers

        #region Transformers: String

        /// <summary>
        /// Format CSV Header
        /// </summary>
        /// <param name="part">Header Line of CSV file</param>
        /// <returns>Log String</returns>
        public static string HeadFormater(string[] part)
        {
            string r = string.Empty;
            if (part == null || part.Length <= 0)
            {
                return r;
            }

            for (int i = 0; i < part.Length; i++)
            {
                r += string.Format("Column [{0}] => {1}", i, part[i]) + Environment.NewLine;
            }

            return r + Environment.NewLine;
        }

        /// <summary>
        /// Format CSV Line
        /// </summary>
        /// <param name="part">CSV Line</param>
        /// <param name="format">string.Format format parameter, string should have same count of placeholders as parts in the CSV line</param>
        /// <returns>Log String</returns>
        public static string StringFormater(string[] part, string format)
        {
            string r = string.Empty;
            if (part == null || part.Length <= 0)
            {
                return r;
            }

            // count max arguments
            var pattern = @"{(.*?)}";
            var matches = Regex.Matches(format, pattern);
            var totalMatchCount = matches.Count;
            var uniqueMatchCount = matches.OfType<Match>().Select(m => m.Value).Distinct();

            foreach (var item in uniqueMatchCount)
            {
                var num = item.Trim('{').Trim('}').ParsInt();
                ////r += "Found: " + num.ToString() + Environment.NewLine;
                if (num > (part.Length - 1))
                {
                    format = format.Replace("{" + num.ToString() + "}", string.Empty);
                }
            }

            try
            {
                r = string.Format(format, part);
            }
            catch (Exception e)
            {
                r = e.Message.ToString() + Environment.NewLine;
            }

            return r;
        }

        #endregion Transformers: String

        #region Transformers: JSON

        /// <summary>
        /// Convert CSV to JSON
        /// </summary>
        /// <param name="list">List of Transformers</param>
        /// <param name="formating">Formating Style</param>
        /// <param name="length">Max number of lines to process, 0 for all lines</param>
        /// <returns>JSON String</returns>
        /// <example>
        /// ```csharp
        /// var json = GetJson(list, Formatting.Indented, 50);
        /// WriteFile(json, "/tmp/sample", "json")
        /// ```
        /// </example>
        public static string GetJson(IEnumerable<Transformers.IBaseModel> list, Formatting formating = Formatting.Indented, int length = 0)
        {
            if (list != null && list.Count() > 0)
            {
                if (length > 0)
                {
                    list = list.Take(length);
                }

                return JsonConvert.SerializeObject(list, formating);
            }

            return string.Empty;
        }

        #endregion Transformers: JSON

        #region Transformers: SQL

        /// <summary>
        /// Convert CSV to SQL Inserts
        /// </summary>
        /// <param name="list">List of Transformers</param>
        /// <param name="length"></param>
        /// <returns>SQL Query</returns>
        public static string GetSql(IEnumerable<Transformers.IBaseModel> list, int length = 0)
        {
            if (list != null && list.Count() > 0)
            {
                if (length > 0)
                {
                    list = list.Take(length);
                }

                string query = string.Empty;
                var a = list.ToArray();
                for (int i = 0; i < a.Length; i++)
                {
                    query += a[i].ToSql();
                }
            }

            return string.Empty;
        }

        #endregion Transformers: SQL

        #region Transformers: CSV

        /// <summary>
        /// Get CSV String
        /// </summary>
        /// <param name="list">List of Transformers</param>
        /// <param name="options">CSV Options</param>
        /// <returns></returns>
        public static string GetCsv(IEnumerable<Transformers.IBaseModel> list, Options options)
        {
            if (list != null && list.Count() > 0)
            {
                string query = string.Empty;
                var a = list.ToArray();
                for (int i = 0; i < a.Length; i++)
                {
                    query += a[i].ToCsv(options);
                }

                return query;
            }

            return string.Empty;
        }

        #endregion Transformers: CSV

        #endregion Transformers
    }
}