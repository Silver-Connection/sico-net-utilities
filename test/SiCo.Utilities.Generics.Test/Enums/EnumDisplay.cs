namespace SiCo.Utilities.Generics.Test.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum EnumDisplay
    {
        [Display(Name = "Unset")]
        Unset = 0,

        [Display(Name = "Stable Version")]
        Stable = 1,

        [Display(Name = "Release Candidate")]
        RC = 2,

        [Display(Name = "Beta Version")]
        Beta = 3,

        [Display(Name = "Alpha Version")]
        Alpha = 4,
    }
}