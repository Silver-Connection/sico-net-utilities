namespace SiCo.Utilities.Pgsql.Attributes
{
    using System;

    /// <summary>
    /// Property Attribute for setting column position
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class ColumnPositionAttribute : Attribute
    {
        /// <summary>
        /// Init
        /// </summary>
        public ColumnPositionAttribute()
        {

        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="position"></param>
        public ColumnPositionAttribute(int position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Position of property in table
        /// </summary>
        public int Position { get; set; }
    }
}
