namespace SiCo.Utilities.Web.Vue.Model
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;

    /// <summary>
    /// Transaction Model
    /// </summary>
    /// <typeparam name="TModel">Model which is worked on</typeparam>
    public class TransactionModel<TModel>
    //where TModel : object
    {
        /// <summary>
        /// Create empty Transaction
        /// </summary>
        public TransactionModel()
        {
            this.Action = string.Empty;
            this.Code = Enums.TransactionCode.NoAction;
            this.Message = string.Empty;
        }

        /// <summary>
        /// Create Transaction
        /// </summary>
        /// <param name="model">Model</param>
        public TransactionModel(TModel model)
            : this()
        {
            this.Data = model;
        }

        /// <summary>
        /// Create Transaction
        /// </summary>
        /// <param name="action">Action String</param>
        public TransactionModel(string action)
            : this()
        {
            this.Action = action;
        }

        /// <summary>
        /// Create Transaction
        /// </summary>
        /// <param name="action">Action String</param>
        /// <param name="model">Model</param>
        public TransactionModel(string action, TModel model)
            : this()
        {
            this.Action = action;
            this.Data = model;
        }

        /// <summary>
        /// Action String
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Transaction Return Code
        /// </summary>
        public Enums.TransactionCode Code { get; set; }

        /// <summary>
        /// Transaction Data / Model
        /// </summary>
        public TModel Data { get; set; }

        /// <summary>
        /// Transaction Message
        /// </summary>
        public string Message { get; set; }

        #region Methods

        /// <summary>
        /// Set Success
        /// </summary>
        /// <param name="message">Message String</param>
        public void Canceled(string message = "Transactions was canceled by user")
        {
            this.Code = Enums.TransactionCode.Canceled;
            this.Message = message;
        }

        /// <summary>
        /// Set Error
        /// </summary>
        /// <param name="message">Error String</param>
        public void Error(string message = "Error during transaction")
        {
            this.Code = Enums.TransactionCode.Error;
            this.Message = message;
        }

        /// <summary>
        /// Set Exception
        /// </summary>
        /// <param name="e">Exception</param>
        public void Error(Exception e)
        {
            this.Code = Enums.TransactionCode.Error;

            if (LogLevel.ShowException)
            {
                this.Message = ExpectionInner(e).ToString();
            }
            else
            {
                this.Message = ExpectionInner(e).Message;
            }
        }

        /// <summary>
        /// Get Errors from ModelState
        /// </summary>
        /// <param name="modelState"></param>
        public bool IsValid(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                this.Action = Utilities.I18n.Error.input_invalid;
                this.Code = Enums.TransactionCode.Error;
                this.Message = string.Empty;

                var errors = modelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();

                var tmp = modelState.Where(x => x.Value.Errors.Count > 0);

                foreach (var item in errors)
                {
                    string entry = item.Key;
                    if (item.Key.Contains('.'))
                    {
                        entry = item.Key.Substring(item.Key.IndexOf('.') + 1);
                    }

                    entry = string.Format(Utilities.I18n.Validation.validation_property_msg, entry) + "<ul>";
                    foreach (var e in item.Errors)
                    {
                        entry += "<li>" + e.ErrorMessage + "</li>";
                    }

                    this.Message += entry + "</ul>";
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Set no action
        /// </summary>
        /// <param name="message">Error String</param>
        public void NoAction(string message = "No Action")
        {
            this.Code = Enums.TransactionCode.NoAction;
            this.Message = message;
        }

        /// <summary>
        /// Set Success
        /// </summary>
        /// <param name="message">Message String</param>
        public void Success(string message = "Transactions was successful")
        {
            this.Code = Enums.TransactionCode.Success;
            this.Message = message;
        }

        private Exception ExpectionInner(Exception e)
        {
            if (e.InnerException != null)
            {
                return ExpectionInner(e.InnerException);
            }
            else
            {
                return e;
            }
        }

        #endregion Methods

        #region Vue

        /// <summary>
        /// Get Vue Model Data as a function
        /// </summary>
        /// <returns></returns>
        public string GetVueModelData(bool indet = false)
        {
            var model = JsonConvert.SerializeObject(this, indet ? Formatting.Indented : Formatting.None);
            return $"function(){{ return {model} }}";
        }

        #endregion Vue
    }
}