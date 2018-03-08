namespace SiCo.Utilities.Pgsql.Test.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Attributes;

    [Table("sample", Schema = "public")]
    public class SampleModel
    {
        public SampleModel()
        {
            this.Age = 0;
            this.Created = DateTime.UtcNow;
            this.Id = 0;
            this.IsValid = false;
            this.Name = string.Empty;
        }

        public SampleModel(Npgsql.NpgsqlDataReader reader)
        {
            this.Age = reader.GetInt32(2);
            this.Created = reader.GetDateTime(4);
            this.Id = reader.GetInt32(0);
            this.IsValid = reader.GetBoolean(3);
            this.Name = reader.GetString(1);
        }

        [ColumnPosition(2)]
        [Column("age")]
        public int Age { get; set; }

        [ColumnPosition(4)]
        [Column("created")]
        public DateTime Created { get; set; }

        [ColumnPosition(0)]
        [Column("id")]
        public int Id { get; set; }

        [ColumnPosition(3)]
        [Column("is_valid")]
        public bool IsValid { get; set; }

        [ColumnPosition(1)]
        [Column("name")]
        public string Name { get; set; }
    }
}