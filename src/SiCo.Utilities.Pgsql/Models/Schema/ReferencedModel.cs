namespace SiCo.Utilities.Pgsql.Models.Schema
{
    using System.Text.RegularExpressions;
    using Generics;

    /// <summary>
    /// Builder Reference Model
    /// </summary>
    public class ReferencedModel : BaseModel
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="line">Reference SQL Query</param>
        public ReferencedModel(string line)
            : base()
        {
            var regexp = new Regex(@"TABLE ""(["",\w]*)""[\s,\w]*""(\w*)""[\s,\w]*\((\w*)\).*");
            line = line.TrimNotEmpty();

            this.Sql = line;
            this.Table = regexp.Replace(line, "$1").TrimNotEmpty();
            this.Name = regexp.Replace(line, "$2").TrimNotEmpty();
            this.Column = regexp.Replace(line, "$3").TrimNotEmpty();
        }

        /// <summary>
        /// Column Name
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// Table Name
        /// </summary>
        public string Table { get; set; }
    }
}