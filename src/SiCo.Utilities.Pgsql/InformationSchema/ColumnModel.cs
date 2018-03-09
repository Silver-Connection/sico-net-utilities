namespace SiCo.Utilities.Pgsql.InformationSchema
{
    /// <summary>
    /// Column Information Model
    /// </summary>
    public class ColumnModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public ColumnModel()
        {
            this.CharMaxLength = 0;
            this.Column = string.Empty;
            this.Default = string.Empty;
            this.IsNullable = false;
            this.Postition = 0;
            this.Schema = string.Empty;
            this.Table = string.Empty;
            this.Type = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="reader">Reader</param>
        public ColumnModel(Npgsql.NpgsqlDataReader reader)
            : this()
        {
            if (!reader.IsDBNull(8))
            {
                this.CharMaxLength = reader.GetInt32(8);
            }

            if (!reader.IsDBNull(5))
            {
                this.Default = reader.GetString(5);
            }

            this.IsNullable = reader.GetString(6) == "YES" ? true : false;
            this.Column = reader.GetString(3);
            this.Postition = reader.GetInt32(4);
            this.Schema = reader.GetString(1);
            this.Table = reader.GetString(2);
            this.Type = reader.GetString(7);
        }

        /// <summary>
        /// Max char length
        /// </summary>
        public int CharMaxLength { get; set; }

        /// <summary>
        /// Column Name
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// Default Value
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Is Null-able
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Column position
        /// </summary>
        public int Postition { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Table Name
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Column Type
        /// </summary>
        public string Type { get; set; }
    }
}