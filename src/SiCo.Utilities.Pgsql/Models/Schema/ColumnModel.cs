namespace SiCo.Utilities.Pgsql.Models.Schema
{
    using Generics;

    /// <summary>
    /// Column Maodel
    /// </summary>
    public class ColumModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public ColumModel()
        {
            this.CharMaxLenght = 0;
            this.Name = string.Empty;
            this.Default = string.Empty;
            this.IsNullable = false;
            this.Position = 0;
            this.Type = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="model">Column Information Model</param>
        public ColumModel(InformationSchema.ColumnModel model)
        {
            this.CharMaxLenght = model.CharMaxLength;
            this.Name = model.Column;
            this.Default = model.Default;
            this.IsNullable = model.IsNullable;
            this.Position = model.Postition;
            this.Type = model.Type;
        }

        /// <summary>
        /// Max Char
        /// </summary>
        public int CharMaxLenght { get; set; }

        /// <summary>
        /// Column Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default Value
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Has Default Value
        /// </summary>
        public bool HasDefault
        {
            get
            {
                return StringExtensions.IsEmpty(this.Default) ? false : true;
            }
        }

        /// <summary>
        /// Is Nullable
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Column Position
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Column Type
        /// </summary>
        public string Type { get; set; }
    }
}
