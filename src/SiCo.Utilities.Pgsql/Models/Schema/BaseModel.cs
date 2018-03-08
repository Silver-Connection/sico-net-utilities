namespace SiCo.Utilities.Pgsql.Models.Schema
{
    /// <summary>
    /// Builder Base Model
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public BaseModel()
        {
            this.Name = string.Empty;
            this.Sql = string.Empty;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// SQL Query
        /// </summary>
        public string Sql { get; set; }
    }
}
