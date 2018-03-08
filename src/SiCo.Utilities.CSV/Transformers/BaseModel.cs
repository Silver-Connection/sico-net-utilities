namespace SiCo.Utilities.CSV.Transformers
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Base Model for a Processor
    /// </summary>
    /// <typeparam name="TModel">Converted Model</typeparam>
    public abstract class BaseModel<TModel> : IBaseModel
        where TModel : IBaseModel, new()
    {
        /// <summary>
        /// Init
        /// </summary>
        public BaseModel()
        {
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="part"></param>
        public BaseModel(string[] part)
        {
        }

        /// <summary>
        /// Processor Display Name
        /// </summary>
        [JsonIgnore]
        public virtual string TrDisplayName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Processor Name
        /// </summary>
        [JsonIgnore]
        public virtual string TrName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get Report Data
        /// </summary>
        /// <param name="list">Converted List</param>
        /// <returns></returns>
        public virtual string GetReport(IEnumerable<object> list)
        {
            return string.Empty;
        }

        /// <summary>
        /// Main Convert Method
        /// </summary>
        /// <param name="model">Each string is a Column of one Row</param>
        /// <returns></returns>
        public object GetConverter(string[] model)
        {
            return Common.CreateModel<TModel>(model);
        }

        /// <summary>
        /// Method for Transforming to CSV
        /// </summary>
        /// <param name="opts">CSV Options</param>
        /// <returns></returns>
        public virtual string ToCsv(Options opts)
        {
            return string.Empty;
        }

        /// <summary>
        /// Method for Transforming to JSON
        /// </summary>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Method for Transforming to SQL
        /// </summary>
        public virtual string ToSql()
        {
            return string.Empty;
        }
    }
}