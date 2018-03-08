namespace SiCo.Utilities.Generics
{
    using System;
    using System.Reflection;
    using Attributes;

    /// <summary>
    /// Display Class Helpers
    /// </summary>
    public static class DisplayClassExtensions
    {
        /// <summary>
        /// Get Display Name for Enum Value
        /// </summary>
        /// <param name="val">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        public static string ToClassDisplayString<T>(this T val)
            where T: class, new()
        {
            DisplayClassAttribute[] attributes = (DisplayClassAttribute[])val
                .GetType()
                .GetTypeInfo()
                .GetCustomAttributes(typeof(DisplayClassAttribute), false);

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
                throw new NotSupportedException(@"Class does not support attribute ""Display""");
            }
        }
    }
}