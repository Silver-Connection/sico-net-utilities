////namespace SiCo.Utilities.Generics
////{
////    using System;
////    using System.Net;

////    ///<Summary>
////    /// Update various types
////    ///</Summary>
////    public static class UpdateExtensions
////    {
////        #region String

////        /// <summary>
////        /// Update string with update value if there is a difference.
////        /// </summary>
////        /// <param name="value">this/input string</param>
////        /// <param name="update">update value</param>
////        /// <param name="save">indicator if a change was done</param>
////        /// <returns>New value</returns>
////        public static void Update(this string value, string update, ref bool save)
////        {
////            if (value != update.TrimNotEmpty())
////            {
////                save = true;
////                if (!StringExtensions.IsEmpty(update))
////                {
////                    value = update.Trim();
////                }
////                else
////                {
////                    value = string.Empty;
////                }
////            }
////        }

////        #endregion String

////        #region DateTimeOffset

////        /// <summary>
////        /// Update date time with update value if there is a difference.
////        /// </summary>
////        /// <param name="value">this/input date time</param>
////        /// <param name="update">update value</param>
////        /// <param name="format">Format used</param>
////        /// <param name="save">indicator if a change was done</param>
////        /// <returns>New value</returns>
////        public static void Update(this DateTimeOffset value, string update, string format, ref bool save)
////        {
////            if (!StringExtensions.IsEmpty(update))
////            {
////                var date = DateTime.UtcNow;
////                if (!DateTime.TryParseExact(update, format, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out date))
////                {
////                    return;
////                }

////                if (!value.Equals(date))
////                {
////                    save = true;
////                    value = date;
////                }
////            }
////        }

////        /// <summary>
////        /// Update date time with update value if there is a difference.
////        /// </summary>
////        /// <param name="value">this/input date time</param>
////        /// <param name="update">update value</param>
////        /// <param name="format">Format used</param>
////        /// <param name="save">indicator if a change was done</param>
////        /// <returns>New value</returns>
////        public static void Update(this DateTimeOffset? value, string update, string format, ref bool save)
////        {
////            if (!StringExtensions.IsEmpty(update))
////            {
////                var date = DateTime.UtcNow;
////                if (!DateTime.TryParseExact(update, format, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out date))
////                {
////                    return;
////                }

////                if (!value.Equals(date))
////                {
////                    save = true;
////                    value = date;
////                }
////            }
////            else
////            {
////                if (value.HasValue)
////                {
////                    save = true;
////                    value = null;
////                }
////            }
////        }

////        #endregion DateTimeOffset

////        #region DateTime

////        /// <summary>
////        /// Update date time with update value if there is a difference.
////        /// </summary>
////        /// <param name="value">this/input date time</param>
////        /// <param name="update">update value</param>
////        /// <param name="format">Format used</param>
////        /// <param name="save">indicator if a change was done</param>
////        /// <returns>New value</returns>
////        public static void Update(this DateTime value, string update, string format, ref bool save)
////        {
////            if (!StringExtensions.IsEmpty(update))
////            {
////                var date = DateTime.UtcNow;
////                if (!DateTime.TryParseExact(update, format, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out date))
////                {
////                    return;
////                }

////                if (!value.Equals(date))
////                {
////                    save = true;
////                    value = date;
////                }
////            }
////        }

////        /// <summary>
////        /// Update date time with update value if there is a difference.
////        /// </summary>
////        /// <param name="value">this/input date time</param>
////        /// <param name="update">update value</param>
////        /// <param name="format">Format used</param>
////        /// <param name="save">indicator if a change was done</param>
////        /// <returns>New value</returns>
////        public static void Update(this DateTime? value, string update, string format, ref bool save)
////        {
////            if (!StringExtensions.IsEmpty(update))
////            {
////                var date = DateTime.UtcNow;
////                if (!DateTime.TryParseExact(update, format, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out date))
////                {
////                    return;
////                }

////                if (!value.Equals(date))
////                {
////                    save = true;
////                    value = date;
////                }
////            }
////            else
////            {
////                if (value.HasValue)
////                {
////                    save = true;
////                    value = null;
////                }
////            }
////        }

////        #endregion DateTime

////        #region Enum

////        /// <summary>
////        /// Update enum with update value if there is a difference.
////        /// </summary>
////        /// <param name="value">this/input enum</param>
////        /// <param name="update">update value</param>
////        /// <param name="save">indicator if a change was done</param>
////        /// <returns>New value</returns>
////        public static void Update(this Enum value, Enum update, ref bool save)
////        {
////            if (!value.Equals(update))
////            {
////                save = true;
////                value = update;
////            }
////        }

////        #endregion Enum

////        #region IPAddress

////        /// <summary>
////        /// Update date time with update value if there is a difference.
////        /// </summary>
////        /// <param name="value">this/input date time</param>
////        /// <param name="update">update value</param>
////        /// <param name="save">indicator if a change was done</param>
////        /// <returns>New value</returns>
////        public static void Update(this IPAddress value, string update, ref bool save)
////        {
////            if (!StringExtensions.IsEmpty(update))
////            {
////                IPAddress date;
////                if (!IPAddress.TryParse(update, out date))
////                {
////                    return;
////                }

////                if (value == null || !value.Equals(date))
////                {
////                    save = true;
////                    value = date;
////                }
////            }

////            if (value != null)
////            {
////                save = true;
////            }
////        }

////        #endregion IPAddress

////        #region Object

////        /// <summary>
////        /// Update boolean with update value if there is a difference.
////        /// </summary>
////        /// <param name="value">this/input boolean</param>
////        /// <param name="update">update value</param>
////        /// <param name="save">indicator if a change was done</param>
////        /// <returns>New value</returns>
////        public static void Update<T>(this T value, T update, ref bool save)
////        {
////            if (!value.Equals(update))
////            {
////                save = true;
////                value = update;
////            }
////        }

////        #endregion Object
////    }
////}