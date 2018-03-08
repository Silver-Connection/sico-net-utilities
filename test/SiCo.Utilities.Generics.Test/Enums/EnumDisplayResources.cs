namespace SiCo.Utilities.Generics.Test.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum EnumDisplayResources
    {
        [Display(Name = "development", ResourceType = typeof(Resources.Resource))]
        Development = 1,

        [Display(Name = "stage", ResourceType = typeof(Resources.Resource))]
        Stage = 2,

        [Display(Name = "production", ResourceType = typeof(Resources.Resource))]
        Production = 3,
    }
}