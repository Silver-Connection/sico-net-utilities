namespace SiCo.Utilities.Pgsql.Models.Schema
{
    /// <summary>
    /// Builder Constrain Model
    /// </summary>
    public class ConstraintModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public ConstraintModel()
        {
            this.Name = string.Empty;
            this.Type = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="model">Constrain Information Model</param>
        public ConstraintModel(InformationSchema.ConstraintModel model)
        {
            this.Name = model.Name;
            this.Type = model.Type;
        }

        /// <summary>
        /// Constrain Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constrain Type
        /// </summary>
        public string Type { get; set; }
    }
}