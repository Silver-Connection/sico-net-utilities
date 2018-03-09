namespace SiCo.Utilities.Pgsql.Models.Connection
{
    using Crypto;
    using Newtonsoft.Json;

    /// <summary>
    /// Basic Connection Model
    /// </summary>
    public class BaseModel : IBaseModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseModel()
        {
            this.Database = "postgres";
            this.Name = "SiCo.Utilities.Psql";
            this.Password = string.Empty;
            this.Port = 5432;
            this.Server = "127.0.0.1";
            this.User = "postgres";
            this.Encoding = "UTF8";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public BaseModel(string connectionString)
            : this()
        {
            var builder = new Npgsql.NpgsqlConnectionStringBuilder(connectionString);

            this.Database = builder.Database;
            this.Name = builder.ApplicationName;
            this.Password = builder.Password;
            this.Server = builder.Host;
            this.User = builder.Username;
        }

        /// <summary>
        /// Database Name
        /// </summary>
        [JsonProperty(Order = 10)]
        public string Database { get; set; }

        /// <summary>
        /// Encoding type
        /// </summary>
        [JsonIgnore]
        public string Encoding { get; set; }

        /// <summary>
        /// Connection Name
        /// </summary>
        [JsonIgnore]
        //[JsonProperty(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        [JsonIgnore]
        ////[JsonProperty(Order = 5)]
        public string Password { get; set; }

        /// <summary>
        /// Password Encrypted
        /// </summary>
        //[JsonIgnore]
        [JsonProperty(Order = 6)]
        public string PasswordCrypt
        {
            get
            {
                if (Generics.StringExtensions.IsEmpty(this.Password))
                {
                    return string.Empty;
                }

                return TripleDES.Encrypt(this.Password);
            }

            set
            {
                if (!Generics.StringExtensions.IsEmpty(value))
                {
                    try
                    {
                        this.Password = TripleDES.Decrypt(value);
                    }
                    catch (System.Exception)
                    {
                        this.Password = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Port Number
        /// </summary>
        [JsonProperty(Order = 3)]
        public int Port { get; set; }

        /// <summary>
        /// Server Address
        /// </summary>
        [JsonProperty(Order = 2)]
        public string Server { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        [JsonProperty(Order = 4)]
        public string User { get; set; }

        /// <summary>
        /// Clean
        /// </summary>
        public virtual void Clean()
        {
            Generics.StringExtensions.TrimModel<BaseModel>(this);
        }
    }
}