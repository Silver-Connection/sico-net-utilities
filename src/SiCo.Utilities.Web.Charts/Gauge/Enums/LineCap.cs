namespace SiCo.Utilities.Web.Charts.Gauge.Enums
{
    using Newtonsoft.Json;

    public enum LineCap
    {
        [JsonProperty(PropertyName = "butt")]
        Butt,

        [JsonProperty(PropertyName = "round")]
        Round,

        [JsonProperty(PropertyName = "square")]
        Square,
    }
}