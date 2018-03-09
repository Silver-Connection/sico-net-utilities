namespace SiCo.Utilities.Web.Vue
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Customer date time converter. It converts DateTime to string in correct format based on DataType attribute and browser settings
    /// </summary>
    public class JsonDateTimeConverter : DateTimeConverterBase
    {
        /// <summary>
        /// Converter used for reading JSON
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (DateTime.TryParse(reader.Value.ToString(), out var auto))
            {
                return auto;
            }

            if (DateTime.TryParseExact(reader.Value.ToString(), "g", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var dateTime))
            {
                return dateTime;
            }

            if (DateTime.TryParseExact(reader.Value.ToString(), "d", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var date))
            {
                return date;
            }

            if (DateTime.TryParseExact(reader.Value.ToString(), "t", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var time))
            {
                return time;
            }

            return DateTime.Parse(reader.Value.ToString());
        }

        /// <summary>
        /// Converter used for creating JSON
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DataTypeAttribute[] attributes = (DataTypeAttribute[])value
                .GetType()
                .GetTypeInfo()
                .GetCustomAttributes(typeof(DataTypeAttribute), false);

            var a = value.GetType().GetTypeInfo().Attributes;
            var b = value.GetType().GetTypeInfo().GetCustomAttributes(true);

            if (attributes?.Count() > 0)
            {
                if (attributes.FirstOrDefault() is DataTypeAttribute dataType)
                {
                    switch (dataType.DataType)
                    {
                        case DataType.DateTime:
                            var dateTime = (DateTime)value;
                            writer.WriteValue(((DateTime)value).ToString("g", CultureInfo.CurrentUICulture));
                            break;

                        case DataType.Date:
                            writer.WriteValue(((DateTime)value).ToString("d", CultureInfo.CurrentUICulture));
                            break;

                        case DataType.Time:
                            writer.WriteValue(((DateTime)value).ToString("t", CultureInfo.CurrentUICulture));
                            break;

                        case DataType.Duration:
                        default:
                            writer.WriteValue(((DateTime)value).ToString("o"));
                            break;
                    }

                    return;
                }
            }

            writer.WriteValue(((DateTime)value).ToString("o"));
        }
    }
}