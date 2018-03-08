namespace SiCo.Utilities.Pgsql.Models.Connection
{
    /// <summary>
    /// Base Connection Interface
    /// </summary>
    public interface IBaseModel
    {
        /// <summary>
        /// Database Name
        /// </summary>
        string Database { get; set; }

        /// <summary>
        /// Encoding Name
        /// </summary>
        string Encoding { get; set; }

        /// <summary>
        /// Connection Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Port Number
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Server Address
        /// </summary>
        string Server { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// Clear
        /// </summary>
        void Clean();
    }
}