namespace SiCo.Utilities.Pgsql.InformationSchema
{
    /// <summary>
    /// Constrains Information Model
    /// </summary>
    public class ConstraintModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public ConstraintModel()
        {
            this.Name = string.Empty;
            this.Schema = string.Empty;
            this.Table = string.Empty;
            this.Type = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="reader">Reader</param>
        public ConstraintModel(Npgsql.NpgsqlDataReader reader)
        {
            this.Name = reader.GetString(2);
            this.Schema = reader.GetString(1);
            this.Table = reader.GetString(5);
            this.Type = reader.GetString(6);
        }

        /// <summary>
        /// Constrain Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Schema Name
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Table Name
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Constrain Type
        /// </summary>
        public string Type { get; set; }
    }
}