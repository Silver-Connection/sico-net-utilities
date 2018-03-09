namespace SiCo.Utilities.Json.Converters
{
    using System.Globalization;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Converts DateTime to date and time
    /// </summary>
    public class WebDateConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Init
        /// </summary>
        public WebDateConverter()
        {
            this.DateTimeFormat = "yyyy-MM-dd";
            this.Culture = CultureInfo.CurrentCulture;
        }
    }
}