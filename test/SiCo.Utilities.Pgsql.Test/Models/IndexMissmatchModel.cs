namespace SiCo.Utilities.Pgsql.Test.Models
{
    using System;

    public class IndexMissmatchModel
    {
        public IndexMissmatchModel()
        {
            this.Created = DateTime.UtcNow;
        }

        public IndexMissmatchModel(Npgsql.NpgsqlDataReader reader)
        {
            this.Created = reader.GetDateTime(23);
        }

        public DateTime Created { get; set; }
    }
}