namespace SiCo.Utilities.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Time Span Helper
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Convert time span to human readable string
        /// </summary>
        /// <param name="t">Timespan</param>
        /// <param name="shortOutput">Shorten output text</param>
        /// <returns></returns>
        public static string ToHumanString(this TimeSpan t, bool shortOutput = false)
        {
            List<string> builder = new List<string>();

            // days
            if (t.Days > 0)
            {
                if (shortOutput)
                {
                    builder.Add(t.Days + " " + Utilities.I18n.Date.day_short);
                }
                else
                {
                    builder.Add(t.Days + " " + ((t.Days == 1) ? Utilities.I18n.Date.day : Utilities.I18n.Date.days));
                }
            }

            // hours
            if (t.Hours > 0)
            {
                if (shortOutput)
                {
                    builder.Add(t.Hours + " " + Utilities.I18n.Date.hour_short);
                }
                else
                {
                    builder.Add(t.Hours + " " + ((t.Hours == 1) ? Utilities.I18n.Date.hour : Utilities.I18n.Date.hours));
                }
            }
            else if (builder.Count() > 0)
            {
                if (shortOutput)
                {
                    builder.Add("0 " + Utilities.I18n.Date.hour_short);
                }
                else
                {
                    builder.Add("0 " + Utilities.I18n.Date.hours);
                }
            }

            // minutes
            if (t.Minutes > 0)
            {
                if (shortOutput)
                {
                    builder.Add(t.Minutes + " " + Utilities.I18n.Date.minute_short);
                }
                else
                {
                    builder.Add(t.Minutes + " " + ((t.Minutes == 1) ? Utilities.I18n.Date.minute : Utilities.I18n.Date.minutes));
                }
            }
            else if (builder.Count() > 0)
            {
                if (shortOutput)
                {
                    builder.Add("0 " + Utilities.I18n.Date.minute_short);
                }
                else
                {
                    builder.Add("0 " + Utilities.I18n.Date.minutes);
                }
            }

            // seconds
            if (t.Seconds > 0)
            {
                if (shortOutput)
                {
                    builder.Add(t.Seconds + " " + Utilities.I18n.Date.second_short);
                }
                else
                {
                    builder.Add(t.Seconds + " " + ((t.Seconds == 1) ? Utilities.I18n.Date.second : Utilities.I18n.Date.seconds));
                }
            }
            else if (builder.Count() > 0)
            {
                if (shortOutput)
                {
                    builder.Add("0 " + Utilities.I18n.Date.second_short);
                }
                else
                {
                    builder.Add("0 " + Utilities.I18n.Date.seconds);
                }
            }

            string result = string.Empty;

            foreach (var item in builder)
            {
                result += item + " ";
            }

            return result;
        }

        /// <summary>
        /// Convert Timespan to string
        /// </summary>
        /// <param name="t"></param>
        /// <param name="shortOutput">Shorten output string</param>
        /// <returns></returns>
        public static string ToDayHourMinuteSecondString(this TimeSpan t, bool shortOutput)
        {
            List<string> builder = new List<string>();

            if (t.Days > 0)
            {
                if (shortOutput)
                {
                    builder.Add(t.Days + " " + Utilities.I18n.Date.day_short);
                }
                else
                {
                    builder.Add(t.Days + " " + ((t.Days == 1) ? Utilities.I18n.Date.day : Utilities.I18n.Date.days));
                }
            }

            // days
            if (t.Days > 0)
            {
                if (shortOutput)
                {
                    builder.Add(t.Days + " " + Utilities.I18n.Date.day_short);
                }
                else
                {
                    builder.Add(t.Days + " " + ((t.Days == 1) ? Utilities.I18n.Date.day : Utilities.I18n.Date.days));
                }
            }

            // hours
            if (t.Hours > 0)
            {
                if (shortOutput)
                {
                    builder.Add(t.Hours + " " + Utilities.I18n.Date.hour_short);
                }
                else
                {
                    builder.Add(t.Hours + " " + ((t.Hours == 1) ? Utilities.I18n.Date.hour : Utilities.I18n.Date.hours));
                }
            }
            else if (builder.Count() > 0)
            {
                if (shortOutput)
                {
                    builder.Add("0 " + Utilities.I18n.Date.hour_short);
                }
                else
                {
                    builder.Add("0 " + Utilities.I18n.Date.hours);
                }
            }

            // minutes
            if (t.Minutes > 0)
            {
                if (shortOutput)
                {
                    builder.Add(t.Minutes + " " + Utilities.I18n.Date.minute_short);
                }
                else
                {
                    builder.Add(t.Minutes + " " + ((t.Minutes == 1) ? Utilities.I18n.Date.minute : Utilities.I18n.Date.minutes));
                }
            }
            else if (builder.Count() > 0)
            {
                if (shortOutput)
                {
                    builder.Add("0 " + Utilities.I18n.Date.minute_short);
                }
                else
                {
                    builder.Add("0 " + Utilities.I18n.Date.minutes);
                }
            }

            // seconds
            if (t.Seconds > 0)
            {
                if (shortOutput)
                {
                    builder.Add(t.Seconds + " " + Utilities.I18n.Date.second_short);
                }
                else
                {
                    builder.Add(t.Seconds + " " + ((t.Seconds == 1) ? Utilities.I18n.Date.second : Utilities.I18n.Date.seconds));
                }
            }
            else if (builder.Count() > 0)
            {
                if (shortOutput)
                {
                    builder.Add("0 " + Utilities.I18n.Date.second_short);
                }
                else
                {
                    builder.Add("0 " + Utilities.I18n.Date.seconds);
                }
            }

            string result = string.Empty;

            foreach (var item in builder)
            {
                result += item + " ";
            }

            return result;
        }
    }
}