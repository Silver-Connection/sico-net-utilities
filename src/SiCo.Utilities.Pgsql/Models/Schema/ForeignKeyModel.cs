namespace SiCo.Utilities.Pgsql.Models.Schema
{
    /// <summary>
    /// Builder Foreign Model
    /// </summary>
    public class ForeignKeyModel : ConstraintModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public ForeignKeyModel()
        {
            this.Name = string.Empty;
            this.Type = "FOREIGN KEY";
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="model">Foreign Information Model</param>
        public ForeignKeyModel(InformationSchema.ForeignKeyModel model)
        {
            this.Name = model.Name;
            this.Type = "FOREIGN KEY";
            this.ReferencedColumn = model.ReferencedColumn;
            this.ReferencedTable = model.ReferencedTable;
        }

        /// <summary>
        /// Referenced Column Name
        /// </summary>
        public string ReferencedColumn { get; set; }

        /// <summary>
        /// Referenced Table Name
        /// </summary>
        public string ReferencedTable { get; set; }
    }
}