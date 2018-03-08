namespace SiCo.Utilities.Generics.Test.Models
{
    public class ModelSimple
    {
        public ModelSimple()
        {
            this.Age = 0;
            this.Comments = string.Empty;
            this.IsValid = false;
            this.Name = string.Empty;
        }

        public int Age { get; set; }

        public string Comments { get; set; }

        public bool IsValid { get; set; }

        public string Name { get; set; }
    }
}