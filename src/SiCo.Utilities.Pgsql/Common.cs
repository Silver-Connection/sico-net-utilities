namespace SiCo.Utilities.Pgsql
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using Attributes;

    /// <summary>
    /// Common Helper
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Get Column Position from attribute
        /// </summary>
        /// <param name="val">Property</param>
        /// <returns>Column Position</returns>
        public static int GetColumnPosition(object val)
        {
            ColumnPositionAttribute attributes = (ColumnPositionAttribute)val
                .GetType()
                .GetTypeInfo()
                .GetCustomAttribute(typeof(ColumnPositionAttribute), false);

            if (attributes != null)
            {
                return attributes.Position;
            }
            else
            {
                throw new NotSupportedException(@"This Property does not have ""ColumnPositionAttribute""");
            }
        }

        /// <summary>
        /// Get Table Name set via Table Attribute
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static string GetTableName<TModel>()
        {
            var restult = string.Empty;
            var table = (TableAttribute)typeof(TModel).GetTypeInfo().GetCustomAttribute(typeof(TableAttribute));
            if (table != null)
            {
                restult = $"{table.Schema}.{table.Name}".Trim('.');
            }

            return restult;
        }

        /// <summary>
        /// Gets PgName value for enum
        /// </summary>
        /// <param name="enumVal">The enum value</param>
        /// <returns>PgName or empty if not found</returns>
        public static string GetPgName(this Enum enumVal)
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            if (memInfo.Count() == 0)
            {
                return null;
            }

            var attributes = memInfo[0].GetCustomAttributes(typeof(NpgsqlTypes.PgNameAttribute), false).ToArray();
            if (attributes?.Length > 0)
            {
                var name = (NpgsqlTypes.PgNameAttribute)attributes[0];
                if (name != null)
                {
                    return name.PgName;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get enum value by PgName. Use as workaround for bug in EfCore.
        /// </summary>
        /// <param name="defaultValue">Default value to set if could not find a match</param>
        /// <param name="pgname">PgName used in database</param>
        /// <returns>Mapped value or default value</returns>
        public static TEnum GetEnumValueByPgName<TEnum>(this TEnum defaultValue, string pgname)
        {
            if (string.IsNullOrEmpty(pgname))
            {
                return defaultValue;
            }

            var type = typeof(TEnum).GetTypeInfo();
            if (!type.IsEnum)
            {
                throw new TypeLoadException("Given type is not an enum.");
            }

            foreach (var item in type.GetMembers())
            {
                var attributes = item.GetCustomAttributes(typeof(NpgsqlTypes.PgNameAttribute), false).ToArray();

                if (attributes?.Length > 0)
                {
                    var name = (NpgsqlTypes.PgNameAttribute)attributes[0];
                    if (name != null && name.PgName == pgname)
                    {
                        return (TEnum)Enum.Parse(typeof(TEnum), item.Name);
                    }
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Get SQL value as a string
        /// </summary>
        /// <param name="item">Property Info</param>
        /// <param name="value">Property</param>
        /// <returns></returns>
        public static string GetSqlValue(PropertyInfo item, object value)
        {
            var name = item.PropertyType.Name;

            // Nullable
            if (item.PropertyType.IsConstructedGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value == null)
                {
                    return "null";
                }

                name = item.PropertyType.GetGenericArguments()[0].Name;
            }

            // Enum
            if (item.PropertyType.GetTypeInfo().IsEnum)
            {
                var enumName = (value as Enum).GetPgName();
                if (!string.IsNullOrEmpty(enumName))
                {
                    return $"'{enumName}'";
                }

                return ((int)(value)).ToString();
            }

            return GetSqlValue(name, value);
        }

        internal static string GetSqlValue(string item, object value)
        {
            var sql = string.Empty;

            switch (item)
            {
                case "Boolean":
                    var b = (bool)value;
                    if (b)
                    {
                        sql = "true";
                    }
                    else
                    {
                        sql = "false";
                    }

                    break;

                case "Int16":
                case "Int32":
                case "Int64":
                case "Double":
                    sql = value.ToString();
                    break;

                case "DateTime":
                    var dt = (DateTime)value;
                    // "2011-04-22 22:33:44.099625+01"
                    sql = $"'{dt.ToString("yyyy-MM-dd HH:mm:ss")}'";
                    break;

                case "String":
                    sql = $"'{value.ToString().Replace("'", "''")}'";
                    break;

                case "IPAddress":
                    sql = $"'{value.ToString()}'";
                    break;

                default:
                    break;
            }

            return sql;
        }

        /// <summary>
        /// Check if valid pg_dump / pg_restore format
        /// </summary>
        /// <param name="value">Format</param>
        /// <returns></returns>
        public static string FormatChecker(string value)
        {
            if (!Generics.StringExtensions.IsEmpty(value))
            {
                string com = value.Trim().ToLower();

                switch (com)
                {
                    case "c":
                    case "custom":
                    case "custome":
                        return "c";

                    case "d":
                    case "dir":
                    case "directory":
                        return "d";

                    case "t":
                    case "tar":
                        return "t";

                    case "p":
                    case "plain":
                    default:
                        return "p";
                }
            }

            return "p";
        }

        /// <summary>
        /// Escape names
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string EscapeName(string name)
        {
            if (Generics.StringExtensions.IsEmpty(name))
            {
                return string.Empty;
            }

            var split = name.Split('.');
            var res = string.Empty;

            foreach (var item in split)
            {
                if (!string.IsNullOrEmpty(res))
                {
                    res += ".";
                }

                res += "\"" + item + "\"";
            }

            return res;
        }
    }
}