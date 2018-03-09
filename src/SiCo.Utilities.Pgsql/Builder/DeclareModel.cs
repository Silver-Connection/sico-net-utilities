namespace SiCo.Utilities.Pgsql.Builder
{
    /// <summary>
    /// Declare Model
    /// </summary>
    public class DeclareModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public DeclareModel()
        {
            this.Name = string.Empty;
            this.Type = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="name"></param>
        public DeclareModel(string name)
        {
            this.Name = name;
            this.Type = "INT";
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
    }
}