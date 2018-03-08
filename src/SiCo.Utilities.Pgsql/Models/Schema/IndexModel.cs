namespace SiCo.Utilities.Pgsql.Models.Schema
{
    using System.Text.RegularExpressions;
    using Generics;

    /// <summary>
    /// Builder Index Model
    /// </summary>
    public class IndexModel : BaseModel
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="line">Index SQL Query</param>
        public IndexModel(string line)
            : base()
        {
            var regexp = new Regex("\"(\\w*)\"(.*)");
            line = line.TrimNotEmpty();

            this.Sql = line;
            this.Name = regexp.Replace(line, "$1").TrimNotEmpty();
        }
    }
}
