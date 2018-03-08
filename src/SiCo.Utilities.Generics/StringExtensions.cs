namespace SiCo.Utilities.Generics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    ///<Summary>
    /// Common used string helpers
    ///</Summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Replace new lines with given one
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="newLine">Newline to set</param>
        /// <returns>Converted string</returns>
        public static string ConvertNewLine(this string inString, string newLine)
        {
            if (!string.IsNullOrEmpty(inString))
            {
                if (inString.Contains("\\r\\n"))
                {
                    inString = inString.Replace("\\r\\n", Environment.NewLine);
                }

                if (inString.Contains("\\n"))
                {
                    inString = inString.Replace("\\n", Environment.NewLine);
                }

                if (inString.Contains("&#x0a;&#x0d;"))
                {
                    inString = inString.Replace("&#x0a;&#x0d;", Environment.NewLine);
                }

                if (inString.Contains("&#x0a;"))
                {
                    inString = inString.Replace("&#x0a;", Environment.NewLine);
                }

                if (inString.Contains("&#x0d;"))
                {
                    inString = inString.Replace("&#x0d;", Environment.NewLine);
                }

                if (inString.Contains("&#10;&#13;"))
                {
                    inString = inString.Replace("&#10;&#13;", Environment.NewLine);
                }

                if (inString.Contains("&#10;"))
                {
                    inString = inString.Replace("&#10;", Environment.NewLine);
                }

                if (inString.Contains("&#13;"))
                {
                    inString = inString.Replace("&#13;", Environment.NewLine);
                }

                ////if (inString.Contains("<br />"))
                ////    s = inString.Replace("<br />", Environment.NewLine);

                if (inString.Contains("<LineBreak />"))
                {
                    inString = inString.Replace("<LineBreak />", Environment.NewLine);
                }
            }

            return inString;
        }

        /// <summary>
        /// Flip / Revert a given string
        /// </summary>
        /// <param name="inString">Input String</param>
        /// <returns>Result string</returns>
        public static string Flip(this string inString)
        {
            if (!IsEmpty(inString))
            {
                char[] charArray = inString.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }

            return string.Empty;
        }

        /// <summary>
        /// Shorten string to given length if input is longer
        /// </summary>
        /// <param name="inString">Input String</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="indicator">Indicator which is added if input is shorten. Default: "..." </param>
        /// <returns>Result string</returns>
        public static string Shorten(this string inString, int maxLength, string indicator = "...")
        {
            if (string.IsNullOrEmpty(inString))
            {
                return string.Empty;
            }

            if (inString.Length > maxLength)
            {
                return inString.Substring(0, maxLength) + indicator;
            }

            return inString;
        }

        /// <summary>
        /// Get all positions for a string
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="find">String to search for</param>
        /// <returns>Array of positions</returns>
        public static IEnumerable<int> IndexOfAll(this string inString, string find)
        {
            if (IsEmpty(inString))
            {
                yield break;
            }

            for (int index = 0; ; index += find.Length)
            {
                index = inString.IndexOf(find, index);
                if (index == -1)
                {
                    break;
                }

                yield return index;
            }

        }

        /// <summary>
        /// Get all positions for a string
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="find">String to search for</param>
        /// <returns>Array of positions</returns>
        public static IEnumerable<int> IndexOfAll(this string inString, char find)
        {
            return inString.IndexOfAll(find.ToString());
        }

        /// <summary>
        /// Check if string is null, empty or whitespace
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <returns>true if empty</returns>
        public static bool IsEmpty(string inString)
        {
            if (string.IsNullOrEmpty(inString) || string.IsNullOrWhiteSpace(inString))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Uppercase for first char in a string
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <returns>Result string</returns>
        public static string ToUpperOnlyFirst(this string inString)
        {
            if (!IsEmpty(inString))
            {
                inString = inString.Trim();
                inString = char.ToUpper(inString[0]).ToString() + inString.ToLower().Substring(1);
                return inString;
            }

            return string.Empty;
        }

        #region SetTrim

        /// <summary>
        /// Update string with update value if there is a difference.
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="replace">new string value</param>
        /// <param name="save">indicator if a change was done</param>
        /// <returns>Result string</returns>
        public static string SetTrim(this string inString, string replace, ref bool save)
        {
            if (inString != replace)
            {
                save = true;
                if (!IsEmpty(replace))
                {
                    return replace.Trim();
                }
                else
                {
                    return string.Empty;
                }
            }

            return inString;
        }

        /// <summary>
        /// Update string with update value if there is a difference.
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="replace">new string value</param>
        /// <returns>Result string</returns>
        public static string SetTrim(this string inString, string replace)
        {
            bool save = false;
            return inString.SetTrim(replace, ref save);
        }

        /// <summary>
        /// Update string with update value if there is a difference.
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="replace">new string value</param>
        /// <param name="empty">Allow empty string</param>
        /// <returns>Result string</returns>
        public static string SetTrim(this string inString, string replace, bool empty)
        {
            if (inString != replace)
            {
                if (!IsEmpty(inString) || !IsEmpty(replace))
                {
                    return replace.Trim();
                }
                else
                {
                    return empty ? string.Empty : inString;
                }
            }

            return inString;
        }

        #endregion SetTrim

        #region Trim

        /// <summary>
        /// Trim end of string
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="match">trim string</param>
        /// <returns>Result string</returns>
        public static string Trim(this string inString, string match)
        {
            if (IsEmpty(inString) || IsEmpty(match))
            {
                return inString;
            }

            if (inString.Length > match.Length && inString.Substring(0, match.Length) == match)
            {
                inString = inString.Substring(match.Length);
            }

            if (inString.Length > match.Length && inString.Substring(inString.Length - match.Length) == match)
            {
                inString = inString.Substring(0, inString.Length - match.Length);
            }

            return inString;
        }

        /// <summary>
        /// Trim start of string
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="match">trim string</param>
        /// <returns>Result string</returns>
        public static string TrimFirst(this string inString, string match)
        {
            if (IsEmpty(inString) || IsEmpty(match))
            {
                return inString;
            }

            if (inString.Length > match.Length && inString.Substring(0, match.Length) == match)
            {
                return inString.Substring(match.Length);
            }

            return inString;
        }

        /// <summary>
        /// Trim end of string
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="match">trim string</param>
        /// <returns>Result string</returns>
        public static string TrimEnd(this string inString, string match)
        {
            if (IsEmpty(inString) || IsEmpty(match))
            {
                return inString;
            }

            if (inString.Length > match.Length && inString.Substring(inString.Length - match.Length) == match)
            {
                return inString.Substring(0, inString.Length - match.Length);
            }

            return inString;
        }

        /// <summary>
        /// Trim all string members in object
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="m">Model</param>
        public static void TrimModel<T>(T m)
        {
            if (m == null)
            {
                return;
            }

            Type t = m.GetType();
            var propertyInfos = t.GetProperties();
            foreach (System.Reflection.PropertyInfo pi in propertyInfos)
            {
                object[] o = pi.GetCustomAttributes(true).ToArray();

                // Check if logging is allowed
                if (pi.PropertyType == typeof(string))
                {
                    if (pi.CanWrite)
                    {
                        var val = pi.GetValue(m);
                        if (val != null)
                        {
                            string v = val.ToString().TrimNotEmpty();
                            if (!string.IsNullOrEmpty(v))
                            {
                                pi.SetValue(m, v);
                            }
                        }
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Trim string if is not empty, else return empty string
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <returns>Result string</returns>
        public static string TrimNotEmpty(this string inString)
        {
            if (!IsEmpty(inString))
            {
                return inString.Trim();
            }

            return string.Empty;
        }

        #endregion Trim

        #region Phone Number

        /// <summary>
        /// Removes leading + and 00, or all leading zeros
        /// </summary>
        /// <param name="number">Given Number</param>
        /// <param name="all">Remove all Zeros at beginning</param>
        /// <returns>Clean phone number</returns>
        public static string CleanPhoneNumber(this string number, bool all = false)
        {
            number = number.Trim();

            // Remove +
            number = number.TrimStart('+');

            if (all)
            {
                number = number.TrimStart('0');
            }
            else
            {
                // Remove Country Code 00
                if (number.StartsWith("00"))
                {
                    number = number.Substring(2);
                }
            }

            number = number.Replace(" ", string.Empty);

            return number;
        }

        #endregion Phone Number
    }
}