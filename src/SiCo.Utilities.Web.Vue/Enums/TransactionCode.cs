namespace SiCo.Utilities.Web.Vue.Enums
{
    /// <summary>
    /// Transaction Return Codes
    /// </summary>
    public enum TransactionCode
    {
        /// <summary>
        /// No Action to take
        /// </summary>
        NoAction = 0,

        /// <summary>
        /// Transaction was successful
        /// </summary>
        Success = 1,

        /// <summary>
        /// Transaction returned an error
        /// </summary>
        Error = 2,

        /// <summary>
        /// Transaction canceled by user
        /// </summary>
        Canceled = 3,

        /// <summary>
        /// Access Denied
        /// </summary>
        AccessDenied = 11,
    }
}