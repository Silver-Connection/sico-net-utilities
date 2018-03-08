namespace SiCo.Utilities.Generics.Models
{
    /// <summary>
    /// Model for Enum Informations 
    /// </summary>
    public class EnumInfoModel
    {
        ///<Summary>
        /// Create empty class
        ///</Summary>
        public EnumInfoModel()
        {
            this.Description = string.Empty;
            this.DisplayName = string.Empty;
            this.ID = 0;
            this.Name = string.Empty;
        }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Display name set in attribute
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }
}