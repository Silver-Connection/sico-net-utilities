namespace SiCo.Utilities.Web.Charts.Gauge
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class GaugeModel
    {
        public GaugeModel()
        {
        }

        [JsonProperty(PropertyName = "autoDraw", NullValueHandling = NullValueHandling.Ignore)]
        public bool AutoDraw { get; set; } = true;

        [JsonProperty(PropertyName = "backgroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public string BackgroundColor { get; set; }

        [JsonProperty(PropertyName = "backgroundShow", NullValueHandling = NullValueHandling.Ignore)]
        public bool BackgroundShow { get; set; }

        [JsonProperty(PropertyName = "canvasHeight", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CanvasHeight { get; set; }

        [JsonProperty(PropertyName = "canvasWidth", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CanvasWidth { get; set; }

        [JsonProperty(PropertyName = "centerX", NullValueHandling = NullValueHandling.Ignore)]
        public Enums.CenterX CenterX { get; set; }

        [JsonProperty(PropertyName = "centerY", NullValueHandling = NullValueHandling.Ignore)]
        public Enums.CenterY CenterY { get; set; }

        [JsonProperty(PropertyName = "offset", NullValueHandling = NullValueHandling.Ignore)]
        public List<GaugeDataset> Data { get; set; } = new List<GaugeDataset>();

        [JsonProperty(PropertyName = "deg", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Deg { get; set; }

        [JsonProperty(PropertyName = "lineCap", NullValueHandling = NullValueHandling.Ignore)]
        public Enums.LineCap LineCap { get; set; }

        [JsonProperty(PropertyName = "offset", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Offset { get; set; }

        public string ToDataJson()
        {
            return JsonConvert.SerializeObject(this.Data);
        }
    }
}