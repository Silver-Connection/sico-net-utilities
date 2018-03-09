namespace SiCo.Utilities.Web.Charts.ChartJs.Base
{
    using Newtonsoft.Json;

    public class DataPointXYModel<TX, TY>
    {
        public DataPointXYModel()
        {
        }

        [JsonProperty(PropertyName = "x")]
        public TX X { get; set; }

        [JsonProperty(PropertyName = "y")]
        public TY Y { get; set; }
    }
}