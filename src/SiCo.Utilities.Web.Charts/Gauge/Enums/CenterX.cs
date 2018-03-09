namespace SiCo.Utilities.Web.Charts.Gauge.Enums
{
    using Newtonsoft.Json;

    public enum CenterX
    {
        [JsonProperty(PropertyName = "center")]
        Center,

        [JsonProperty(PropertyName = "left")]
        Left,

        [JsonProperty(PropertyName = "right")]
        Right,
    }
}