namespace SiCo.Utilities.Helper.Test
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SiCo.Utilities.Helper.Attributes;
    using Xunit;

    public class PhoneClassToBeTested
    {
        public string PhonePropertyToBeTested
        {
            get { return "abcdefghij"; }
        }
    }

    public class ValidatePhoneAttributeTest
    {
        private static readonly ValidationContext s_testValidationContext = new ValidationContext(new object());

        [Fact]
        public static void GetValidationResult_returns_DefaultErrorMessage_if_ErrorMessage_is_not_set()
        {
            var attribute = new ValidatePhoneAttribute();
            var toBeTested = new PhoneClassToBeTested();
            var validationContext = new ValidationContext(toBeTested)
            {
                MemberName = "PhonePropertyToBeTested"
            };
            attribute.GetValidationResult(toBeTested, validationContext);
        }

        [Fact]
        public static void GetValidationResult_returns_ErrorMessage_from_resource_if_ErrorMessageResourceName_and_ErrorMessageResourceType_both_set()
        {
            var attribute = new ValidatePhoneAttribute
            {
                ErrorMessage = string.Empty,
                ErrorMessageResourceName = "number_phone",
                ErrorMessageResourceType = typeof(I18n.Error)
            };
            var toBeTested = new PhoneClassToBeTested();
            var validationContext = new ValidationContext(toBeTested)
            {
                MemberName = "PhonePropertyToBeTested"
            };
            var validationResult = attribute.GetValidationResult(toBeTested, validationContext);
            Assert.Equal(
                "Given value is not a valid phone number.",
                validationResult.ErrorMessage);
        }

        [Fact]
        public static void GetValidationResult_returns_ErrorMessage_if_ErrorMessage_overrides_default()
        {
            var attribute = new ValidatePhoneAttribute
            {
                ErrorMessage = "SomeErrorMessage"
            };
            var toBeTested = new PhoneClassToBeTested();
            var validationContext = new ValidationContext(toBeTested)
            {
                MemberName = "PhonePropertyToBeTested"
            };
            var validationResult = attribute.GetValidationResult(toBeTested, validationContext);
            Assert.Equal("SomeErrorMessage", validationResult.ErrorMessage);
        }

        [Fact]
        public static void Validate_check_AllowEmptyString()
        {
            var attribute = new ValidatePhoneAttribute
            {
                AllowEmptyStrings = true
            };
            attribute.Validate(string.Empty, s_testValidationContext);

            attribute.AllowEmptyStrings = false;
            Assert.Throws<ValidationException>(() => attribute.Validate(string.Empty, s_testValidationContext));
        }

        [Fact]
        public static void Validate_successful_for_null_value()
        {
            var attribute = new ValidatePhoneAttribute();
            attribute.Validate(null, s_testValidationContext); // Null is valid
        }

        [Fact]
        public static void Validate_successful_for_valid_phone_numbers()
        {
            var attribute = new ValidatePhoneAttribute();
            attribute.Validate("00425-555-1212", s_testValidationContext);
            attribute.Validate("425-555-1212", s_testValidationContext);
            attribute.Validate("+1 425-555-1212", s_testValidationContext);
            attribute.Validate("(425)555-1212", s_testValidationContext);
            attribute.Validate("(425) 555-1212", s_testValidationContext);
            attribute.Validate("+44 (3456)987654", s_testValidationContext);
            attribute.Validate("+777.456.789.123", s_testValidationContext);
            attribute.Validate("425-555-1212 x123", s_testValidationContext);
            attribute.Validate("425-555-1212 x 123", s_testValidationContext);
            attribute.Validate("425-555-1212 ext123", s_testValidationContext);
            attribute.Validate("425-555-1212 ext 123", s_testValidationContext);
            attribute.Validate("425-555-1212 ext.123", s_testValidationContext);
            attribute.Validate("425-555-1212 ext. 123", s_testValidationContext);
        }

        [Fact]
        public static void Validate_throws_for_invalid_phone_numbers()
        {
            var attribute = new ValidatePhoneAttribute();
            Assert.Throws<ValidationException>(() => attribute.Validate(new object(), s_testValidationContext));
            Assert.Throws<ValidationException>(() => attribute.Validate(string.Empty, s_testValidationContext));
            Assert.Throws<ValidationException>(() => attribute.Validate("abcdefghij", s_testValidationContext));
            Assert.Throws<ValidationException>(() => attribute.Validate("425-555-1212 ext 123 ext 456", s_testValidationContext));
            Assert.Throws<ValidationException>(() => attribute.Validate("425-555-1212 x", s_testValidationContext));
            Assert.Throws<ValidationException>(() => attribute.Validate("425-555-1212 ext", s_testValidationContext));
            Assert.Throws<ValidationException>(() => attribute.Validate("425-555-1212 ext.", s_testValidationContext));
            Assert.Throws<ValidationException>(() => attribute.Validate("425-555-1212 x abc", s_testValidationContext));
            Assert.Throws<ValidationException>(() => attribute.Validate("425-555-1212 ext def", s_testValidationContext));
            Assert.Throws<ValidationException>(() => attribute.Validate("425-555-1212 ext. xyz", s_testValidationContext));
        }

        [Fact]
        public static void Validate_throws_InvalidOperationException_if_ErrorMessage_and_ErrorMessageResourceName_are_set()
        {
            var attribute = new ValidatePhoneAttribute
            {
                ErrorMessage = "SomeErrorMessage",
                ErrorMessageResourceName = "SomeErrorMessageResourceName"
            };
            Assert.Throws<InvalidOperationException>(() => attribute.Validate("abcdefghij", s_testValidationContext));
        }

        [Fact]
        public static void Validate_throws_InvalidOperationException_if_ErrorMessage_is_null()
        {
            var attribute = new ValidatePhoneAttribute
            {
                ErrorMessage = null // note: this overrides the default value
            };
            Assert.Throws<InvalidOperationException>(() => attribute.Validate("abcdefghij", s_testValidationContext));
        }

        [Fact]
        public static void Validate_throws_InvalidOperationException_if_ErrorMessageResourceName_set_but_ErrorMessageResourceType_not_set()
        {
            var attribute = new ValidatePhoneAttribute
            {
                ErrorMessageResourceName = "SomeErrorMessageResourceName",
                ErrorMessageResourceType = null
            };
            Assert.Throws<InvalidOperationException>(() => attribute.Validate("abcdefghij", s_testValidationContext));
        }

        [Fact]
        public static void Validate_throws_InvalidOperationException_if_ErrorMessageResourceType_set_but_ErrorMessageResourceName_not_set()
        {
            var attribute = new ValidatePhoneAttribute
            {
                ErrorMessageResourceName = null,
                ErrorMessageResourceType = typeof(ErrorMessageResources)
            };
            Assert.Throws<InvalidOperationException>(() => attribute.Validate("abcdefghij", s_testValidationContext));
        }

        [Fact]
        public static void ValidatePhoneAttributeTests_creation_DataType_and_CustomDataType()
        {
            var attribute = new ValidatePhoneAttribute();
            Assert.Equal(DataType.PhoneNumber, attribute.DataType);
            Assert.Null(attribute.CustomDataType);
        }
    }
}