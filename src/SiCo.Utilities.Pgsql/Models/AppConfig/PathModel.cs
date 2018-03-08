namespace SiCo.Utilities.Pgsql.Models.AppConfig
{
    using Newtonsoft.Json;
    using SiCo.Utilities.Generics;

    /// <summary>
    /// App Settings
    /// </summary>
    public class PathModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PathModel()
        {
        }

        /// <summary>
        /// Export path
        /// </summary>
        [JsonProperty(PropertyName = "export")]
        public string Export { get; set; }

        /// <summary>
        /// Jobs path
        /// </summary>
        [JsonProperty(PropertyName = "jobs")]
        public string Jobs { get; set; }

        /// <summary>
        /// Clean user input
        /// </summary>
        public void Clean()
        {
            this.Export = this.Export.TrimNotEmpty();
            this.Jobs = this.Jobs.TrimNotEmpty();
        }
    }
}