namespace SiCo.Utilities.Pgsql.InformationSchema
{
    /// <summary>
    /// Foreign Key Information Model
    /// </summary>
    public class ForeignKeyModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public ForeignKeyModel()
        {
            this.Column = string.Empty;
            this.Name = string.Empty;
            this.ReferencedColumn = string.Empty;
            this.ReferencedTable = string.Empty;
            this.Table = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="reader">Reader</param>
        public ForeignKeyModel(Npgsql.NpgsqlDataReader reader)
        {
            this.Column = reader.GetString(2);
            this.Name = reader.GetString(4);
            this.ReferencedColumn = reader.GetString(1);
            this.ReferencedTable = reader.GetString(0);
            this.Table = reader.GetString(2);
        }

        /// <summary>
        /// Column Name
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// Foreign Key Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Referenced Column Name
        /// </summary>
        public string ReferencedColumn { get; set; }

        /// <summary>
        /// Referenced Table Name
        /// </summary>
        public string ReferencedTable { get; set; }

        /// <summary>
        /// Table Name
        /// </summary>
        public string Table { get; set; }
    }
}