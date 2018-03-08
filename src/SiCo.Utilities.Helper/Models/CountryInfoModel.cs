namespace SiCo.Utilities.Helper.Models
{
    /// <summary>
    /// Country Information Model
    /// </summary>
    public class CountryInfoModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public CountryInfoModel()
        {
            this.ISO2 = string.Empty;
            this.ISO3 = string.Empty;
            this.Name = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="model"></param>
        public CountryInfoModel(CountryModel model)
        {
            this.ISO2 = model.ISO2;
            this.ISO3 = model.ISO3;
            this.Name = model.NameFallback;
        }

        /// <summary>
        /// ISO A2 Country Code
        /// </summary>
        public string ISO2 { get; set; }

        /// <summary>
        /// ISO A3 Country Code
        /// </summary>
        public string ISO3 { get; set; }

        /// <summary>
        /// Country Name
        /// </summary>
        public string Name { get; set; }
    }
}