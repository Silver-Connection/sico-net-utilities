namespace SiCo.Utilities.Web.Charts.ChartJs.Dataset
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class RadarDataModel : Base.XModelBase, Base.IXModel
    {
        public RadarDataModel() : base()
        {
        }

        public RadarDataModel(string label) : base(label)
        {
        }

        [JsonProperty(PropertyName = "backgroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> BackgroundColor { get; set; }

        [JsonProperty(PropertyName = "borderCapStyle", NullValueHandling = NullValueHandling.Ignore)]
        public string BorderCapStyle { get; set; }

        [JsonProperty(PropertyName = "borderColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> BorderColor { get; set; }

        [JsonProperty(PropertyName = "borderDashOffset", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BorderDashOffset { get; set; }

        [JsonProperty(PropertyName = "borderDash", NullValueHandling = NullValueHandling.Ignore)]
        public IList<decimal> BorderDashSpacing { get; set; }

        [JsonProperty(PropertyName = "borderJoinStyle", NullValueHandling = NullValueHandling.Ignore)]
        public string BorderJoinStyle { get; set; }

        [JsonProperty(PropertyName = "borderWidth", NullValueHandling = NullValueHandling.Ignore)]
        public IList<decimal> BorderWidth { get; set; }

        [JsonProperty(PropertyName = "cubicInterpolationMode", NullValueHandling = NullValueHandling.Ignore)]
        public string CubicInterpolationMode { get; set; }

        [JsonProperty(PropertyName = "fill", NullValueHandling = NullValueHandling.Ignore)]
        public string Fill { get; set; }

        [JsonProperty(PropertyName = "lineTension", NullValueHandling = NullValueHandling.Ignore)]
        public decimal LineTension { get; set; }

        [JsonProperty(PropertyName = "pointBackgroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> PointBackgroundColor { get; set; }

        [JsonProperty(PropertyName = "pointBorderColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> PointBorderColor { get; set; }

        [JsonProperty(PropertyName = "pointBorderWidth", NullValueHandling = NullValueHandling.Ignore)]
        public IList<decimal> PointBorderWidth { get; set; }

        [JsonProperty(PropertyName = "pointHitRadius", NullValueHandling = NullValueHandling.Ignore)]
        public IList<decimal> PointHitRadius { get; set; }

        [JsonProperty(PropertyName = "pointHoverBackgroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> PointHoverBackgroundColor { get; set; }

        [JsonProperty(PropertyName = "pointHoverBorderColor", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> PointHoverBorderColor { get; set; }

        [JsonProperty(PropertyName = "pointHoverBorderWidth", NullValueHandling = NullValueHandling.Ignore)]
        public IList<decimal> PointHoverBorderWidth { get; set; }

        [JsonProperty(PropertyName = "pointHoverRadius", NullValueHandling = NullValueHandling.Ignore)]
        public IList<decimal> PointHoverRadius { get; set; }

        [JsonProperty(PropertyName = "pointRadius", NullValueHandling = NullValueHandling.Ignore)]
        public IList<decimal> PointRadius { get; set; }

        [JsonProperty(PropertyName = "pointStyle", NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> PointStyle { get; set; }

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