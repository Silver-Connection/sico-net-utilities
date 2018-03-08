namespace SiCo.Utilities.Helper.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SiCo.Utilities.Generics;

    /// <summary>
    /// Validation attribute to indicate that a property field or parameter is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
    public sealed class ValidatePhoneAttribute : DataTypeAttribute
    {
        private const string _additionalPhoneNumberCharacters = "-.()";
        private const string _extensionAbbreviationExt = "ext";
        private const string _extensionAbbreviationExtDot = "ext.";
        private const string _extensionAbbreviationX = "x";

        /// <summary>
        /// Init
        /// </summary>
        public ValidatePhoneAttribute()
            : base(DataType.PhoneNumber)
        {
            ErrorMessage = new PhoneAttribute().ErrorMessage;
        }

        /// <summary>
        /// Allow empty string
        /// </summary>
        public bool AllowEmptyStrings { get; set; }

        /// <summary>
        /// Check if input is valid
        /// </summary>
        /// <param name="value">Property Value</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var valueAsString = value as string;
            if (StringExtensions.IsEmpty(valueAsString))
            {
                return this.AllowEmptyStrings ? true : false;
            }

            // Trim + , 00
            valueAsString = valueAsString.TrimFirst("+").TrimFirst("00").Trim();
            valueAsString = RemoveExtension(valueAsString);

            bool digitFound = false;
            foreach (char c in valueAsString)
            {
                if (Char.IsDigit(c))
                {
                    digitFound = true;
                    break;
                }
            }

            if (!digitFound)
            {
                return false;
            }

            foreach (char c in valueAsString)
            {
                if (!(Char.IsDigit(c)
                    || Char.IsWhiteSpace(c)
                    || _additionalPhoneNumberCharacters.IndexOf(c) != -1))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool MatchesExtension(string potentialExtension)
        {
            potentialExtension = potentialExtension.TrimStart();
            if (potentialExtension.Length == 0)
            {
                return false;
            }

            foreach (char c in potentialExtension)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        private static string RemoveExtension(string potentialPhoneNumber)
        {
            var lastIndexOfExtension = potentialPhoneNumber
                .LastIndexOf(_extensionAbbreviationExtDot, StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                var extension = potentialPhoneNumber.Substring(
                    lastIndexOfExtension + _extensionAbbreviationExtDot.Length);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            lastIndexOfExtension = potentialPhoneNumber
                .LastIndexOf(_extensionAbbreviationExt, StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                var extension = potentialPhoneNumber.Substring(
                    lastIndexOfExtension + _extensionAbbreviationExt.Length);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            lastIndexOfExtension = potentialPhoneNumber
                .LastIndexOf(_extensionAbbreviationX, StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                var extension = potentialPhoneNumber.Substring(
                    lastIndexOfExtension + _extensionAbbreviationX.Length);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            return potentialPhoneNumber;
        }
    }
}