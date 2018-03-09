namespace SiCo.Utilities.Web.Charts.ChartJs.Dataset
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class BarXDataModel : Base.XModelBase, Base.IXModel
    {
        public BarXDataModel() : base()
        {
        }

        public BarXDataModel(string label) : base(label)
        {
        }

        [JsonProperty(PropertyName = "xAxisID", NullValueHandling = NullValueHandling.Ignore)]
        public string AxisXId { get; set; }

        [JsonProperty(PropertyName = "yAxisID", NullValueHandling = NullValueHandling.Ignore)]
        public string AxisYId { get; set; }

        [JsonProperty(PropertyName = "backgroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> BackgroundColor { get; set; }

        [JsonProperty(PropertyName = "hoverBackgroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> BackgroundColorHover { get; set; }

        [JsonProperty(PropertyName = "borderColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> BorderColor { get; set; }

        [JsonProperty(PropertyName = "hoverBorderColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> BorderColorHover { get; set; }

        [JsonProperty(PropertyName = "borderWidth", NullValueHandling = NullValueHandling.Ignore)]
        public IList<decimal> BorderWidth { get; set; }

        [JsonProperty(PropertyName = "hoverBorderWidth", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> BorderWidthHover { get; set; }

        [JsonProperty(PropertyName = "stack", NullValueHandling = NullValueHandling.Ignore)]
        public string Stack { get; set; }

        public override void AddValue(decimal value)
        {
            this.Data.Add(Math.Round(value, 2, MidpointRounding.AwayFromZero));
        }

        public override void AddValue(decimal value, string color)
        {
            this.Data.Add(Math.Round(value, 2, MidpointRounding.AwayFromZero));

            if (this.BackgroundColor == null)
            {
                this.BackgroundColor = new List<string>();
            }
            this.BackgroundColor.Add(color);
        }
    }
}