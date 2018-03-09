namespace SiCo.Utilities.Web.Charts.ChartJs.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public abstract class DatasetBaseModel<TData>
    {
        public DatasetBaseModel()
        {
        }

        public DatasetBaseModel(string label)
        {
            this.Label = label;
        }

        [JsonProperty(PropertyName = "data", ItemConverterType = typeof(Converter.DecimalJsonConverter))]
        public IList<TData> Data { get; set; } = new List<TData>();

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        public virtual void AddValue(TData value)
        {
        }

        public virtual void AddValue(TData value, string color)
        {
        }

        public virtual void CloneValue()
        {
            if (this.Data?.Count > 0)
            {
                this.Data.Add(this.Data.Last());
            }
        }
    }
}