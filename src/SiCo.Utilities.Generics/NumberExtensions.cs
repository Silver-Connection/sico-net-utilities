namespace SiCo.Utilities.Generics
{
    ///<Summary>
    /// Common number helpers
    ///</Summary>
    public static class NumberExtensions
    {
        #region NullableConvert

        /// <summary>
        /// Set 0 if is null
        /// </summary>
        /// <param name="num">this/input number</param>
        /// <returns>value</returns>
        public static int NumNullToNum(this int? num)
        {
            return num.HasValue && num.Value != 0 ? num.Value : (int)0;
        }

        /// <summary>
        /// Set 0 if is null
        /// </summary>
        /// <param name="num">this/input number</param>
        /// <returns>value</returns>
        public static long NumNullToNum(this long? num)
        {
            return num.HasValue && num.Value != 0 ? num.Value : (long)0;
        }

        /// <summary>
        /// Set 0 if is null
        /// </summary>
        /// <param name="num">this/input number</param>
        /// <returns>value</returns>
        public static decimal NumNullToNum(this decimal? num)
        {
            return num.HasValue && num.Value != 0 ? num.Value : (decimal)0;
        }

        /// <summary>
        /// Set 0 if is null
        /// </summary>
        /// <param name="num">this/input number</param>
        /// <returns>value</returns>
        public static double NumNullToNum(this double? num)
        {
            return num.HasValue && num.Value != 0 ? num.Value : (double)0;
        }

        /// <summary>
        /// Set null if given number is 0
        /// </summary>
        /// <param name="num">this/input number</param>
        /// <returns>value</returns>
        public static int? NumToNumNull(this int num)
        {
            return num == 0 ? null : (int?)num;
        }

        /// <summary>
        /// Set null if given number is 0
        /// </summary>
        /// <param name="num">this/input number</param>
        /// <returns>value</returns>
        public static long? NumToNumNull(this long num)
        {
            return num == 0 ? null : (long?)num;
        }

        /// <summary>
        /// Set null if given number is 0
        /// </summary>
        /// <param name="num">this/input number</param>
        /// <returns>value</returns>
        public static decimal? NumToNumNull(this decimal num)
        {
            return num == 0 ? null : (decimal?)num;
        }

        /// <summary>
        /// Set null if given number is 0
        /// </summary>
        /// <param name="num">this/input number</param>
        /// <returns>value</returns>
        public static double? NumToNumNull(this double num)
        {
            return num == 0 ? null : (double?)num;
        }

        #endregion NullableConvert

        #region Pars

        /// <summary>
        /// Convert string to double
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <returns>0 if false</returns>
        public static double ParsDouble(this string inString)
        {
            if (StringExtensions.IsEmpty(inString)
                || !double.TryParse(inString, out double o))
            {
                return 0;
            }

            return o;
        }

        /// <summary>
        /// Convert string to double?
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <returns>0 if false</returns>
        public static double? ParsDoubleNull(this string inString)
        {
            if (StringExtensions.IsEmpty(inString)
                || !double.TryParse(inString, out double o))
            {
                return null;
            }

            return o;
        }

        /// <summary>
        /// Convert boolean to int
        /// </summary>
        /// <param name="inBool">this/input number</param>
        /// <returns>0 if false</returns>
        public static int ParseInt(this bool inBool)
        {
            return inBool ? 1 : 0;
        }

        /// <summary>
        /// Convert string to int
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <returns>0 if false</returns>
        public static int ParsInt(this string inString)
        {
            if (StringExtensions.IsEmpty(inString)
                || !int.TryParse(inString, out int o))
            {
                return 0;
            }

            return o;
        }

        /// <summary>
        /// Convert string to int?
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <returns>0 if false</returns>
        public static int? ParsIntNull(this string inString)
        {
            if (StringExtensions.IsEmpty(inString)
                || !int.TryParse(inString, out int o))
            {
                return null;
            }

            return o;
        }

        #endregion Pars
    }
}