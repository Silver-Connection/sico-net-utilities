namespace SiCo.Utilities.Web.Charts.Gauge.Enums
{
    using Newtonsoft.Json;

    public enum CenterY
    {
        [JsonProperty(PropertyName = "center")]
        Center,

        [JsonProperty(PropertyName = "top")]
        Top,

        [JsonProperty(PropertyName = "bottom")]
        Bottom,
    }
}