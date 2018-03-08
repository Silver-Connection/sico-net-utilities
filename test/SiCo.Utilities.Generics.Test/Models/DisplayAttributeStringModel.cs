namespace SiCo.Utilities.Generics.Test.Models
{
    using SiCo.Utilities.Generics.Attributes;

    [DisplayClass(Name = "Test Class Name")]
    public class DisplayAttributeStringModel
    {
        public DisplayAttributeStringModel()
        {
            this.Name = string.Empty;
        }

        public string Name { get; set; }
    }
}