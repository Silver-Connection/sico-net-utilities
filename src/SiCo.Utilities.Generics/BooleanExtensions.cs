namespace SiCo.Utilities.Generics
{
    /// <summary>
    /// Boolean helper
    /// </summary>
    public static class BooleanExtensions
    {
        #region Pars

        /// <summary>
        /// Parse from string
        /// </summary>
        /// <param name="inString">input string</param>
        /// <example>
        /// ```csharp
        /// using SiCo.Utilities.Generics;
        /// ...
        /// var st = "True";
        /// var bol = st.ParsBool();
        /// ```
        /// </example>
        public static bool ParsBool(this string inString)
        {
            if (StringExtensions.IsEmpty(inString)
                || !bool.TryParse(inString, out bool o))
            {
                if (inString == "1")
                {
                    return true;
                }

                return false;
            }

            return o;
        }

        /// <summary>
        /// Parse from string, return null if parsing fails
        /// </summary>
        /// <param name="inString">input string</param>
        /// <example>
        /// ```csharp
        /// using SiCo.Utilities.Generics;
        /// ...
        /// var st = "True";
        /// var bol = st.ParsBool();
        /// ```
        /// </example>
        public static bool? ParsBoolNull(this string inString)
        {
            if (StringExtensions.IsEmpty(inString)
                || !bool.TryParse(inString, out bool o))
            {
                return null;
            }

            return o;
        }

        #endregion Pars

        /// <summary>
        /// Return given string if value is true, vice-versa for false values
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="trueString">Is returned if value is true</param>
        /// <param name="falseString">Is returned if value is false</param>
        /// <returns>Result</returns>
        /// <example>
        /// ```csharp
        /// using SiCo.Utilities.Generics;
        /// ...
        /// var val = true;
        /// var st = val.Check("IsChecked", "Unchecked")
        ///
        /// st -> IsChecked
        /// ```
        /// </example>
        public static string Check(this bool inString, string trueString, string falseString)
        {
            return inString ? trueString : falseString;
        }
    }
}