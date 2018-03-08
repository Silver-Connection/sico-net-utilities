namespace SiCo.Utilities.Generics.Attributes
{
    using System;

    /// <summary>
    /// Get Display Name for Class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DisplayClassAttribute : Attribute
    {
        ///<Summary>
        /// Create empty class
        ///</Summary>
        public DisplayClassAttribute()
        {
        }

        /// <summary>
        /// Class Display Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Resource type if used
        /// </summary>
        public Type ResourceType { get; set; }

        /// <summary>
        /// Get Name
        /// </summary>
        public string GetName()
        {
            if (!string.IsNullOrEmpty(this.Name) && !string.IsNullOrWhiteSpace(this.Name))
            {
                if (this.ResourceType != null)
                {
                    var resourceManager = ResourceManagers.GetResourceManager(this.ResourceType);

                    string value = resourceManager.GetString(this.Name);
                    return value ?? string.Empty;
                }
                else
                {
                    return this.Name;
                }
            }

            return string.Empty;
        }
    }
}