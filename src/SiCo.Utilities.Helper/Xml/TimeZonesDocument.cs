namespace SiCo.Utilities.Helper.Xml
{
    using System.Xml.Serialization;

    /// <summary>
    /// XML Map Model
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(IsNullable = false, ElementName = "TimeZones")]
    public class TimeZonesDocument
    {
        private TimeZoneItem[] itemsField;

        /// <summary>
        /// Items
        /// </summary>
        [XmlElement("MapZone", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TimeZoneItem[] Items
        {
            get
            {
                return this.itemsField;
            }

            set
            {
                this.itemsField = value;
            }
        }
    }
}