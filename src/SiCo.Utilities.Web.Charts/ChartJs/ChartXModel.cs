namespace SiCo.Utilities.Web.Charts.ChartJs
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class ChartXModel<T>
        where T : Base.IXModel, new()
    {
        public ChartXModel()
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

        public void AddValue(string datasetLabel, decimal value, string color = null)
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

        public void AddValue(string label, string datasetLabel, decimal value, string color = null)
        {
            this.AddValue(datasetLabel, value, color);
            this.AddLabel(label);
        }

        public void CloneValue(string datasetLabel)
        {
            if (this.Datasets == null)
            {
                return;
            }

            var match = this.Datasets.SingleOrDefault(d => d.Label == datasetLabel);
            if (match != null)
            {
                match.CloneValue();
            }
        }

        public T GetAverage(string name, string color)
        {
            var average = Helper.CreateModel<T>(name);

            for (int i = 0; i < this.Lables.Count; i++)
            {
                decimal total = 0;
                foreach (var item in this.Datasets)
                {
                    total += item.Data[i];
                }

                average.AddValue(total / this.Datasets.Count, color);
            }

            return average;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}