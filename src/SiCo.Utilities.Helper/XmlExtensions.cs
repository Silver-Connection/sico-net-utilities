namespace SiCo.Utilities.Helper
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;

    /// <summary>
    /// XML Helper
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Convert Enum to XML string
        /// </summary>
        /// <param name="val">Enum value</param>
        /// <returns>XML</returns>
        public static string ToXmlString(this Enum val)
        {
            XmlEnumAttribute[] attributes = (XmlEnumAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(XmlEnumAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Name;
            }
            else
            {
                throw new NotSupportedException(@"Enum does not support attribute ""Display""");
            }
        }

        /// <summary>
        /// Try to deserialize stream to a model
        /// </summary>
        /// <typeparam name="T">Model Type</typeparam>
        /// <param name="stream">Stream</param>
        /// <param name="xmlObject">OUT Filled Model</param>
        /// <returns>True on success</returns>
        public static bool TryDeserialize<T>(Stream stream, out T xmlObject)
        {
            xmlObject = default(T);

            using (var sr = new StreamReader(stream))
            {
                return TryDeserialize(sr.ReadToEnd(), out xmlObject);
            }
        }

        /// <summary>
        /// Try to deserialize XML string to a model
        /// </summary>
        /// <typeparam name="T">Model Type</typeparam>
        /// <param name="xmlText">XML string</param>
        /// <param name="xmlObject">OUT Filled Model</param>
        /// <returns>True on success</returns>
        public static bool TryDeserialize<T>(string xmlText, out T xmlObject)
        {
            xmlObject = default(T);

            using (var r = System.Xml.XmlReader.Create(new StringReader(xmlText)))
            {
                var serializer = new XmlSerializer(typeof(T));
                try
                {
                    xmlObject = (T)serializer.Deserialize(r);

                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Try to serialize a Model to XML string
        /// </summary>
        /// <param name="xmlObject">Model</param>
        /// <param name="xmlText">OUT XML string</param>
        /// <returns>True on success</returns>
        public static bool TrySerialize(object xmlObject, out string xmlText)
        {
            xmlText = string.Empty;
            var serializer = new XmlSerializer(xmlObject.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    serializer.Serialize(ms, xmlObject);
                    ms.Flush();

                    xmlText = System.Text.Encoding.UTF8.GetString(ms.ToArray());

                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}