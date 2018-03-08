namespace SiCo.Utilities.Generics
{
    using System;
    using System.Net;

    ///<Summary>
    /// Update various types
    ///</Summary>
    public static class UpdateExtensions
    {

        #region String

        /// <summary>
        /// Update string with update value if there is a difference.
        /// </summary>
        /// <param name="value">this/input string</param>
        /// <param name="update">update value</param>
        /// <param name="save">indicator if a change was done</param>
        /// <returns>New value</returns>
        public static string Update(this string value, string update, ref bool save)
        {
            if (value != update.TrimNotEmpty())
            {
                save = true;
                if (!StringExtensions.IsEmpty(update))
                {
                    return update.Trim();
                }
                else
                {
                    return string.Empty;
                }
            }

            return value;
        }

        #endregion String

        #region DateTimeOffset

        /// <summary>
        /// Update date time with update value if there is a difference.
        /// </summary>
        /// <param name="value">this/input date time</param>
        /// <param name="update">update value</param>
        /// <param name="format">Format used</param>
        /// <param name="save">indicator if a change was done</param>
        /// <returns>New value</returns>
        public static DateTimeOffset Update(this DateTimeOffset value, string update, string format, ref bool save)
        {
            if (!StringExtensions.IsEmpty(update))
            {
                var date = DateTime.UtcNow;
                if (!DateTime.TryParseExact(update, format, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out date))
                {
                    return value;
                }

                if (!value.Equals(date))
                {
                    save = true;
                    value = date;
                }
            }

            return value;
        }

        /// <summary>
        /// Update date time with update value if there is a difference.
        /// </summary>
        /// <param name="value">this/input date time</param>
        /// <param name="update">update value</param>
        /// <param name="format">Format used</param>
        /// <param name="save">indicator if a change was done</param>
        /// <returns>New value</returns>
        public static DateTimeOffset? Update(this DateTimeOffset? value, string update, string format, ref bool save)
        {
            if (!StringExtensions.IsEmpty(update))
            {
                var date = DateTime.UtcNow;
                if (!DateTime.TryParseExact(update, format, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out date))
                {
                    return value;
                }

                if (!value.Equals(date))
                {
                    save = true;
                    value = date;
                }
            }
            else
            {
                if (value.HasValue)
                {
                    save = true;
                    value = null;
                }
            }

            return value;
        }

        #endregion DateTime

        #region DateTime

        /// <summary>
        /// Update date time with update value if there is a difference.
        /// </summary>
        /// <param name="value">this/input date time</param>
        /// <param name="update">update value</param>
        /// <param name="format">Format used</param>
        /// <param name="save">indicator if a change was done</param>
        /// <returns>New value</returns>
        public static DateTime Update(this DateTime value, string update, string format, ref bool save)
        {
            if (!StringExtensions.IsEmpty(update))
            {
                var date = DateTime.UtcNow;
                if (!DateTime.TryParseExact(update, format, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out date))
                {
                    return value;
                }

                if (!value.Equals(date))
                {
                    save = true;
                    value = date;
                }
            }

            return value;
        }

        /// <summary>
        /// Update date time with update value if there is a difference.
        /// </summary>
        /// <param name="value">this/input date time</param>
        /// <param name="update">update value</param>
        /// <param name="format">Format used</param>
        /// <param name="save">indicator if a change was done</param>
        /// <returns>New value</returns>
        public static DateTime? Update(this DateTime? value, string update, string format, ref bool save)
        {
            if (!StringExtensions.IsEmpty(update))
            {
                var date = DateTime.UtcNow;
                if (!DateTime.TryParseExact(update, format, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out date))
                {
                    return value;
                }

                if (!value.Equals(date))
                {
                    save = true;
                    value = date;
                }
            }
            else
            {
                if (value.HasValue)
                {
                    save = true;
                    value = null;
                }
            }

            return value;
        }

        #endregion DateTime

        #region Enum

        /// <summary>
        /// Update enum with update value if there is a difference.
        /// </summary>
        /// <param name="value">this/input enum</param>
        /// <param name="update">update value</param>
        /// <param name="save">indicator if a change was done</param>
        /// <returns>New value</returns>
        public static Enum Update(this Enum value, Enum update, ref bool save)
        {
            if (!value.Equals(update))
            {
                save = true;
                value = update;
            }

            return value;
        }

        #endregion Enum

        #region IPAddress

        /// <summary>
        /// Update date time with update value if there is a difference.
        /// </summary>
        /// <param name="value">this/input date time</param>
        /// <param name="update">update value</param>
        /// <param name="save">indicator if a change was done</param>
        /// <returns>New value</returns>
        public static IPAddress Update(this IPAddress value, string update, ref bool save)
        {
            if (!StringExtensions.IsEmpty(update))
            {
                IPAddress date;
                if (!IPAddress.TryParse(update, out date))
                {
                    return value;
                }

                if (value == null || !value.Equals(date))
                {
                    save = true;
                    value = date;
                }
            }

            if (value != null)
            {
                save = true;
            }

            return null;
        }

        #endregion IPAddress

        #region Object

        /// <summary>
        /// Update boolean with update value if there is a difference.
        /// </summary>
        /// <param name="value">this/input boolean</param>
        /// <param name="update">update value</param>
        /// <param name="save">indicator if a change was done</param>
        /// <returns>New value</returns>
        public static T Update<T>(this T value, T update, ref bool save)
        {
            if (!value.Equals(update))
            {
                save = true;
                value = update;
            }

            return value;
        }

        #endregion Object

    }
}