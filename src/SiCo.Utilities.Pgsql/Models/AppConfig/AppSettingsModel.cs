namespace SiCo.Utilities.Pgsql.Models.AppConfig
{
    using Newtonsoft.Json;

    /// <summary>
    /// App Settings
    /// </summary>
    public class AppSettingsModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingsModel()
        {
        }

        /// <summary>
        /// Tool paths
        /// </summary>
        [JsonProperty(PropertyName = "bin")]
        public BinModel Bin { get; set; } = new BinModel();

        /// <summary>
        /// Work paths
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public PathModel Path { get; set; } = new PathModel();

        /// <summary>
        /// Clean user input
        /// </summary>
        public void Clean()
        {
            this.Bin.Clean();
            this.Path.Clean();
        }
    }
}