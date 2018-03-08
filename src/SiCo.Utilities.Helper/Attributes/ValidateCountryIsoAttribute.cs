namespace SiCo.Utilities.Helper.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Generics;

    /// <summary>
    ///     Validation attribute to indicate that a property field or parameter is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class ValidateCountryIsoAttribute : ValidationAttribute
    {
        /// <summary>
        /// Init
        /// </summary>
        public ValidateCountryIsoAttribute()
        {
            this.AllowEmptyStrings = false;
            this.ErrorMessageResourceType = typeof(I18n.Error);
            this.ErrorMessageResourceName = "country_not_found";
        }

        /// <summary>
        ///     Gets or sets a flag indicating whether the attribute should allow empty strings.
        /// </summary>
        public bool AllowEmptyStrings { get; set; }

        /// <summary>
        ///     Override of <see cref="ValidationAttribute.IsValid(object)" />
        /// </summary>
        /// <param name="value">The value to test</param>
        /// <returns>
        ///     <c>false</c> if the <paramref name="value" /> is null or an empty string. If
        ///     <see cref="RequiredAttribute.AllowEmptyStrings" />
        ///     then <c>false</c> is returned only if <paramref name="value" /> is null.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var stringValue = value as string;
            if (StringExtensions.IsEmpty(stringValue))
            {
                return this.AllowEmptyStrings ? true : false;
            }

            return Countries.IsValid(value.ToString());
        }
    }
}