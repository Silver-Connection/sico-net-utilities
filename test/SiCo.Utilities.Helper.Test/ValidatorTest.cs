namespace SiCo.Utilities.Helper.Test
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class ValidatorTest
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("DE", true)]
        [InlineData("DEU", true)]
        [InlineData(" FRA ", true)]
        [InlineData("F1", false)]
        [InlineData("AAAAFRA", false)]
        public void Validate_CountryIso(string iso, bool result)
        {
            //Countries.Initialize();
            ICollection<ValidationResult> validationResult = new List<ValidationResult>();

            var model = new Models.ValidateModel()
            {
                CountryIso = iso,
                Required = "Text",
                Number = 3
            };
            var context = new ValidationContext(model);

            // build-in
            var valid = Validator.TryValidateObject(model, context, validationResult, true);
            Assert.Equal(result, valid);
        }
    }
}