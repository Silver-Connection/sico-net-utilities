namespace SiCo.Utilities.Helper.Xml
{
    using System.Xml.Serialization;

    /// <summary>
    /// XML Map Model
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class TimeZoneItem
    {
        private string otherField;

        private string territoryField;

        private string typeField;

        /// <summary>
        /// Init
        /// </summary>
        public TimeZoneItem()
        {
            this.otherField = string.Empty;
            this.territoryField = string.Empty;
            this.typeField = string.Empty;
        }

        /// <summary>
        /// Other
        /// </summary>
        [XmlAttribute]
        public string Other
        {
            get
            {
                return this.otherField;
            }

            set
            {
                this.otherField = value;
            }
        }

        /// <summary>
        /// Territory
        /// </summary>
        [XmlAttribute]
        public string Territory
        {
            get
            {
                return this.territoryField;
            }

            set
            {
                this.territoryField = value;
            }
        }

        /// <summary>
        /// Type
        /// </summary>
        [XmlAttribute]
        public string Type
        {
            get
            {
                return this.typeField;
            }

            set
            {
                this.typeField = value;
            }
        }
    }
}