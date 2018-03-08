namespace SiCo.Utilities.Generics
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Date and time helpers
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// If set true we will use ``CultureInfo.CurrentUICulture`` in string generation
        /// </summary>
        public static bool CurrentUICulture = true;

        /// <summary>
        /// Format for dates
        /// </summary>
        public static string Date = "dd.MM.yyyy";

        /// <summary>
        /// Format for date and time
        /// </summary>
        public static string DateTime = "dd.MM.yyyy HH:mm:ss";

        /// <summary>
        /// Format for time
        /// </summary>
        public static string Time = "HH:mm:ss";

        /// <summary>
        /// Convert to UNIX epoch time format
        /// </summary>
        public static string ToUnixEpoch(this DateTime date)
        {
            var ticks = date.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts.ToString();
        }

        #region AppDate

        /// <summary>
        /// Get time in format set in the static filed Date
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppDateString(this DateTime date)
        {
            return _ToString(date, Date);
        }

        /// <summary>
        /// Get date in format set in the static filed Date
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppDateString(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }
            return _ToString(date.Value, Date);
        }

        /// <summary>
        /// Get date in format set in the static filed Date
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppDateString(this DateTimeOffset date)
        {
            return date._ToString(Date);
        }

        /// <summary>
        /// Get date in format set in the static filed Date
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppDateString(this DateTimeOffset? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }
            return date.Value._ToString(Date);
        }

        /// <summary>
        /// Get date and time in format set in the static filed DateTime
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppString(this DateTime date)
        {
            return _ToString(date, DateTime);
        }

        /// <summary>
        /// Get date and time in format set in the static filed DateTime
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppString(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }
            return _ToString(date.Value, DateTime);
        }

        /// <summary>
        /// Get date and time in format set in the static filed DateTime
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppString(this DateTimeOffset date)
        {
            return date._ToString(DateTime);
        }

        /// <summary>
        /// Get date and time in format set in the static filed DateTime
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppString(this DateTimeOffset? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }
            return date.Value._ToString(DateTime);
        }

        /// <summary>
        /// Get time in format set in the static filed Time
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppTimeString(this DateTime date)
        {
            return _ToString(date, Time);
        }

        /// <summary>
        /// Get time in format set in the static filed Time
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppTimeString(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }
            return _ToString(date.Value, Time);
        }

        /// <summary>
        /// Get time in format set in the static filed Time
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppTimeString(this DateTimeOffset date)
        {
            return date._ToString(Time);
        }

        /// <summary>
        /// Get time in format set in the static filed Time
        /// </summary>
        /// <param name="date">Date</param>
        public static string ToAppTimeString(this DateTimeOffset? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }
            return date.Value._ToString(Time);
        }

        #endregion AppDate

        private static string _ToString(this DateTime date, string format)
        {
            if (CurrentUICulture)
            {
                return date.ToString(format, CultureInfo.CurrentUICulture);
            }
            else
            {
                return date.ToString(format);
            }
        }

        private static string _ToString(this DateTimeOffset date, string format)
        {
            if (CurrentUICulture)
            {
                return date.ToString(format, CultureInfo.CurrentUICulture);
            }
            else
            {
                return date.ToString(format);
            }
        }
    }
}