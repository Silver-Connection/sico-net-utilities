namespace SiCo.Utilities.Helper.Test.Models
{
    using Newtonsoft.Json;
    using SiCo.Utilities.Helper.Attributes;

    public class ModelSimple
    {
        public ModelSimple()
        {
            this.Age = 0;
            this.Comments = string.Empty;
            this.IsValid = false;
            this.Name = string.Empty;
            this.Password = string.Empty;
            this.Pin = string.Empty;
        }

        public int Age { get; set; }

        public string Comments { get; set; }

        public bool IsValid { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [LogIgnore]
        public string Pin { get; set; }
    }
}