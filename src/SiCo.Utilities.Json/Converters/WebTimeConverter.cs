namespace SiCo.Utilities.Json.Converters
{
    using System.Globalization;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Converts DateTime to date and time
    /// </summary>
    public class WebTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Init
        /// </summary>
        public WebTimeConverter()
        {
            this.DateTimeFormat = "hh:mm";
            this.Culture = CultureInfo.CurrentCulture;
        }
    }
}
