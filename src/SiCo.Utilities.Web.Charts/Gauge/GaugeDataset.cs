namespace SiCo.Utilities.Web.Charts.Gauge
{
    using Newtonsoft.Json;

    public class GaugeDataset
    {
        public GaugeDataset()
        {
        }

        public GaugeDataset(GaugeDataset model)
        {
            this.Color = model.Color;
            this.Label = model.Label;
            this.LabelColor = model.LabelColor;
            this.LabelFont = model.LabelFont;
            this.LabelShow = model.LabelShow;
            this.LabelSize = model.LabelSize;
            this.LabelStyle = model.LabelStyle;
            this.Size = model.Size;
            this.Value = model.Value;
        }

        [JsonProperty(PropertyName = "color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "labelColor", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelColor { get; set; }

        [JsonProperty(PropertyName = "labelCss", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelCss { get; set; }

        [JsonProperty(PropertyName = "labelFont", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelFont { get; set; }

        [JsonProperty(PropertyName = "labelShow", NullValueHandling = NullValueHandling.Ignore)]
        public bool LabelShow { get; set; } = true;

        [JsonProperty(PropertyName = "labelSize", NullValueHandling = NullValueHandling.Ignore)]
        public decimal LabelSize { get; set; }

        [JsonProperty(PropertyName = "labelStyle", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelStyle { get; set; }

        [JsonProperty(PropertyName = "size", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Size { get; set; }

        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Value { get; set; }
    }
}