namespace SiCo.Utilities.Helper.Test
{
    using System.ComponentModel.DataAnnotations;
    using SiCo.Utilities.Helper.Attributes;
    using Xunit;

    public class ValidateCountryIsoAttributeTest
    {
        private static readonly ValidationContext s_testValidationContext = new ValidationContext(new object());

        [Fact]
        public static void Can_get_and_set_AllowEmptyStrings()
        {
            var attribute = new ValidateCountryIsoAttribute();
            Assert.False(attribute.AllowEmptyStrings);
            attribute.AllowEmptyStrings = true;
            Assert.True(attribute.AllowEmptyStrings);
            attribute.AllowEmptyStrings = false;
            Assert.False(attribute.AllowEmptyStrings);
        }

        [Theory]
        [InlineData("DE", true)]
        [InlineData("DEU", true)]
        [InlineData(" FRA ", true)]
        [InlineData("F1", false)]
        [InlineData("AAAAFRA", false)]
        public static void IsValid(string iso, bool result)
        {
            //Countries.Initialize();
            var attribute = new ValidateCountryIsoAttribute
            {
                AllowEmptyStrings = true
            };
            Assert.Equal(result, attribute.IsValid(iso));
        }

        [Fact]
        public static void Validation_ErrorMessage()
        {
            //Countries.Initialize();
            var attribute = new ValidateCountryIsoAttribute();
            Assert.False(attribute.IsValid("INVALID"));
            var t = attribute.FormatErrorMessage("ISO");
            Assert.NotEmpty(t);
        }

        [Fact]
        public static void Validation_throws_ValidationException_for_empty_string_if_AllowEmptyStrings_is_false()
        {
            //Countries.Initialize();
            var attribute = new ValidateCountryIsoAttribute
            {
                AllowEmptyStrings = false
            };
            Assert.Throws<ValidationException>(() => attribute.Validate(string.Empty, s_testValidationContext));
        }

        [Theory]
        [InlineData("F")]
        [InlineData("1DE")]
        [InlineData("DE U")]
        public static void Validation_throws_ValidationException_for_inavlid_country_iso(string iso)
        {
            //Countries.Initialize();
            var attribute = new ValidateCountryIsoAttribute
            {
                AllowEmptyStrings = true
            };
            Assert.Throws<ValidationException>(() => attribute.Validate(iso, s_testValidationContext));
        }

        [Fact]
        public static void Validation_throws_ValidationException_for_null_value()
        {
            var attribute = new ValidateCountryIsoAttribute();
            Assert.Throws<ValidationException>(() => attribute.Validate(null, s_testValidationContext));
        }

        [Theory]
        [InlineData("DE")]
        [InlineData("DEU")]
        [InlineData(" FRA ")]
        public static void Validation_valid_country_iso(string iso)
        {
            //Countries.Initialize();
            var attribute = new ValidateCountryIsoAttribute
            {
                AllowEmptyStrings = true
            };
            attribute.Validate(iso, s_testValidationContext);
        }

        [Fact]
        public static void Validation_valid_for_empty_string_if_AllowEmptyStrings_is_true()
        {
            //Countries.Initialize();
            var attribute = new ValidateCountryIsoAttribute
            {
                AllowEmptyStrings = true
            };
            attribute.Validate(string.Empty, s_testValidationContext);
        }
    }
}