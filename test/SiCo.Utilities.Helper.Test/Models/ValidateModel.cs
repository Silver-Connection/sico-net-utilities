namespace SiCo.Utilities.Helper.Test.Models
{
    using System.ComponentModel.DataAnnotations;

    [Generics.Attributes.DisplayClass(Name = "ValidateModel")]
    public class ValidateModel
    {
        public ValidateModel()
        {
            this.CountryIso = string.Empty;
            this.Required = string.Empty;
        }

        [Display(Name = "Country")]
        [Attributes.ValidateCountryIso(AllowEmptyStrings = false)]
        public string CountryIso { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Required { get; set; }

        [Range(1, 9)]
        public int Number { get; set; }
    }
}