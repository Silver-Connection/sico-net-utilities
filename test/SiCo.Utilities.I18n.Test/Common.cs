namespace SiCo.Utilities.I18n.Test
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class Common
    {
        public static void CheckDisplayAttributes(Type model)
        {
            var properties = model.GetProperties().Where(prop => prop.IsDefined(typeof(DisplayAttribute), false));
            var log = string.Empty;
            foreach (var item in properties)
            {
                DisplayAttribute[] attributes = (DisplayAttribute[])item.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (null != attributes[0].ResourceType)
                {
                    var resourceManager = ResourceManagers.GetResourceManager(attributes[0].ResourceType);
                    string value = resourceManager.GetString(attributes[0].Name);
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new Exception("Could not find Display Name for: " + attributes[0].ResourceType.Name + "." + item.Name);
                    }
                }
            }
        }
    }
}