namespace SiCo.Utilities.Pgsql.Test.Models
{
    using System;

    public class WrongModel
    {
        public WrongModel()
        {
            this.Created = DateTime.UtcNow;
        }

        public WrongModel(Npgsql.NpgsqlDataReader reader)
        {
            this.Created = reader.GetDateTime(0);
        }

        public DateTime Created { get; set; }
    }
}