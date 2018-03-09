namespace SiCo.Utilities.Json.Converters
{
    using System.Globalization;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Converts DateTime to date and time
    /// </summary>
    public class WebDateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Init
        /// </summary>
        public WebDateTimeConverter()
        {
            this.DateTimeFormat = "yyyy-MM-ddThh:mm";
            this.Culture = CultureInfo.CurrentCulture;
        }
    }
}