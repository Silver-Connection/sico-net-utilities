namespace SiCo.Utilities.Web.Charts.ChartJs
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class ChartXYModel<T>
        where T : Base.IXYModel, new()
    {
        public ChartXYModel()
        {
        }

        [JsonProperty(PropertyName = "datasets")]
        public IList<T> Datasets { get; set; } = new List<T>();

        [JsonProperty(PropertyName = "labels")]
        public IList<string> Lables { get; set; } = new List<string>();

        public void AddDataset(T value)
        {
            if (value == null)
            {
                return;
            }

            // Add dataset
            if (this.Datasets == null)
            {
                this.Datasets = new List<T>() { value };
            }
            else
            {
                var match = this.Datasets.SingleOrDefault(d => d.Label == value.Label);
                if (match != null)
                {
                    match = value;
                }
                else
                {
                    this.Datasets.Add(value);
                }
            }
        }

        public void AddLabel(string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                return;
            }

            // Add dataset
            if (this.Lables == null)
            {
                this.Lables = new List<string>() { label };
            }
            else
            {
                if (!this.Lables.Any(d => d == label))
                {
                    this.Lables.Add(label);
                }
            }
        }

        public void AddValue(string datasetLabel, string valueX, decimal valueY, string color = null)
        {
            if (this.Datasets == null)
            {
                this.Datasets = new List<T>();
            }

            var match = this.Datasets.SingleOrDefault(d => d.Label == datasetLabel);
            if (match == null)
            {
                match = Helper.CreateModel<T>(datasetLabel);
                this.Datasets.Add(match);
            }

            match.AddValue(valueX, valueY, color);
        }

        public void AddValue(string datasetLabel, Base.DataPointXYModel<string, decimal> value, string color = null)
        {
            if (this.Datasets == null)
            {
                this.Datasets = new List<T>();
            }

            var match = this.Datasets.SingleOrDefault(d => d.Label == datasetLabel);
            if (match == null)
            {
                match = Helper.CreateModel<T>(datasetLabel);
                this.Datasets.Add(match);
            }

            match.AddValue(value, color);
        }

        public void AddValue(string label, string datasetLabel, string valueX, decimal valueY, string color = null)
        {
            this.AddValue(datasetLabel, valueX, valueY, color);
            this.AddLabel(label);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}