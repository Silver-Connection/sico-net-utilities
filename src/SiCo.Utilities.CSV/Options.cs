namespace SiCo.Utilities.CSV
{
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// CSV File Options
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Init
        /// </summary>
        public Options()
        {
            this.Comment = string.Empty;
            this.Delimiters = new string[] { ";" };
            this.Encoding = Encoding.UTF8;
            this.Head = true;
            this.Lines = 0;
            this.Quoted = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="delimiters"></param>
        public Options(string[] delimiters)
            : this()
        {
            this.Delimiters = delimiters;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="delimiter"></param>
        /// <param name="quoted"></param>
        public Options(string[] delimiter, string quoted)
            : this(delimiter)
        {
            this.Quoted = quoted;
        }

        /// <summary>
        /// Comment string
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// List of Delimiters
        /// </summary>
        public string[] Delimiters { get; set; }

        /// <summary>
        /// Encoding Type
        /// </summary>
        [JsonIgnore]
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Encoding Name
        /// </summary>
        public string EncodingName
        {
            get
            {
                if (this.Encoding == null)
                {
                    return string.Empty;
                }

                return this.Encoding.EncodingName;
            }

            set
            {
                var enc = System.Text.Encoding.GetEncoding(value);
                if (enc != null)
                {
                    this.Encoding = enc;
                }
            }
        }

        /// <summary>
        /// Has Head
        /// </summary>
        public bool Head { get; set; }

        /// <summary>
        /// How many lines should be processed
        /// </summary>
        public int Lines { get; set; }

        /// <summary>
        /// Quote String if any
        /// </summary>
        public string Quoted { get; set; }

        /// <summary>
        /// Convert Options to JSON
        /// </summary>
        /// <returns>JSON String</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}