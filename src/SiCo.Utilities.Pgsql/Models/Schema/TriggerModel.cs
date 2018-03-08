namespace SiCo.Utilities.Pgsql.Models.Schema
{
    using System.Text.RegularExpressions;
    using Generics;

    /// <summary>
    /// Builder Trigger Modek
    /// </summary>
    public class TriggerModel : BaseModel
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="line">Trigger SQL Query</param>
        public TriggerModel(string line)
            : base()
        {
            var regexp = new Regex(@"\s*(\w*)\s.*\(\)");
            line = line.TrimNotEmpty();

            this.Sql = line;
            this.Name = regexp.Replace(line, "$1").TrimNotEmpty();
        }
    }
}