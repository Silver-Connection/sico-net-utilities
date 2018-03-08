namespace SiCo.Utilities.Generics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Enum helpers
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// Sample: enum.GetAttributeOfType&lt;DisplayAttribute&gt;().GetName();
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            if (memInfo.Count() == 0)
            {
                return null;
            }

            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false).ToArray();
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// Gets all infos for a Enum
        /// </summary>
        /// <param name="enumType">Type of Enum</param>
        /// <returns>Select List with all Enum Values, Name, Display Name, Description</returns>
        public static IEnumerable<Models.EnumInfoModel> GetFullInfo(Type enumType)
        {
            if (!enumType.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<Models.EnumInfoModel> values = new List<Models.EnumInfoModel>();

            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (FieldInfo field in fields)
            {
                int num = (int)Enum.Parse(enumType, field.Name);
                string text = field.Name;
                string description = field.Name;
                var tmp = field.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault();
                if (tmp != null)
                {
                    text = tmp.GetName();
                    description = tmp.GetDescription();
                }

                values.Add(new Models.EnumInfoModel()
                {
                    ID = num,
                    Name = field.Name,
                    DisplayName = text,
                    Description = description
                });
            }

            return values;
        }

        /// <summary>
        /// Creates a select list with all enum values.
        /// </summary>
        /// <param name="enumType">Type of Enum</param>
        /// <returns>Select List with all Enum Values and Description/Name</returns>
        public static IEnumerable<KeyValuePair<string, object>> GetKeyValueList(Type enumType)
        {
            if (!enumType.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<KeyValuePair<string, object>> values = new List<KeyValuePair<string, object>>();

            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (FieldInfo field in fields)
            {
                var valueType = Enum.GetUnderlyingType(enumType);
                object num = string.Empty;
                if (valueType.Name == "Int32")
                {
                    num = (int)Enum.Parse(enumType, field.Name);
                }
                else
                {
                    num = Enum.Parse(enumType, field.Name);
                }

                string text = field.Name;
                var tmp = field.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault();
                if (tmp != null)
                {
                    text = tmp.GetName();
                }

                values.Add(new KeyValuePair<string, object>(text, num));
            }

            return values;
        }

        // TODO: Fix T to use only Enums
        /// <summary>
        /// Get Enum Name
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        public static string GetName<T>(this T val)
        {
            return Enum.GetName(typeof(T), val);
        }

        /// <summary>
        /// Get Enum Values
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Get Display Name for Enum Value
        /// </summary>
        /// <param name="val">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        public static string ToDisplayString(this Enum val)
        {
            DisplayAttribute[] attributes = (DisplayAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes.Length > 0)
            {
                if (null == attributes[0].ResourceType)
                {
                    return attributes[0].Name;
                }

                try
                {
                    var resourceManager = ResourceManagers.GetResourceManager(attributes[0].ResourceType);

                    string value = resourceManager.GetString(attributes[0].Name);
                    return value == null ? string.Empty : value;
                }
                catch
                {
                    return attributes[0].Name;
                }
            }
            else
            {
                throw new NotSupportedException(@"Enum does not support attribute ""Display""");
            }
        }
    }
}