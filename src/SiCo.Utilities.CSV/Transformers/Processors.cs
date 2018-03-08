namespace SiCo.Utilities.CSV.Transformers
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Processor Management
    /// </summary>
    /// <example>
    /// // Populate processors
    /// var processors = new Processors();
    /// processors.Transformers = new IBaseModel[] 
    /// {
    ///     new ITU.E1648B(),
    /// }
    /// </example>
    public class Processors
    {
        /// <summary>
        /// Init
        /// </summary>
        public Processors()
        {
            this.Transformers = new IBaseModel[] { };
        }

        /// <summary>
        /// List of Transformers
        /// </summary>
        public IEnumerable<IBaseModel> Transformers { get; set; }

        /// <summary>
        /// Get Processor by Name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Processor</returns>
        public IBaseModel GetByName(string name)
        {
            return this.Transformers.SingleOrDefault(p => p.TrName == name);
        }

        /// <summary>
        /// Get Processor by Display Name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Processor</returns>
        public IBaseModel GetByDisplayName(string name)
        {
            return this.Transformers.SingleOrDefault(p => p.TrDisplayName == name);
        }

        /// <summary>
        /// Get a List of all registered Processors
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, IBaseModel>> GetTransfomers()
        {
            var list = new List<KeyValuePair<string, IBaseModel>>();
            list.Add(new KeyValuePair<string, IBaseModel>("String", null));

            if (this.Transformers != null || this.Transformers.Count() > 0)
            {
                foreach (var item in this.Transformers)
                {
                    list.Add(new KeyValuePair<string, IBaseModel>(item.TrDisplayName, item));
                }
            }

            return list;
        }
    }
}