namespace SiCo.Utilities.Generics.Test.Models
{
    using SiCo.Utilities.Generics.Attributes;

    [DisplayClass(Name = "name", ResourceType = typeof(Resources.Resource))]
    public class DisplayAttributeResourcesModel
    {
        public DisplayAttributeResourcesModel()
        {
            this.Name = string.Empty;
        }

        public string Name { get; set; }
    }
}