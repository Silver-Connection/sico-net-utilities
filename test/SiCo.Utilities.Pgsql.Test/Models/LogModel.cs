namespace SiCo.Utilities.Pgsql.Test.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Attributes;

    [Table("log", Schema = "public")]
    public class LogModel
    {
        public LogModel()
        {
            this.Created = DateTime.UtcNow;
            this.Message = string.Empty;
            this.Path = string.Empty;
            this.Section = string.Empty;
        }

        public LogModel(Npgsql.NpgsqlDataReader reader)
        {
            this.Created = reader.GetDateTime(3);
            this.Message = reader.GetString(2);
            this.Path = reader.GetString(0);
            this.Section = reader.GetString(1);
        }

        [ColumnPosition(0)]
        [Column("id")]
        public long Id { get; set; }

        [ColumnPosition(7)]
        [Column("created")]
        public DateTime Created { get; set; }

        [ColumnPosition(6)]
        [Column("message")]
        public string Message { get; set; }

        [ColumnPosition(2)]
        [Column("path")]
        public string Path { get; set; }

        [ColumnPosition(3)]
        [Column("section")]
        public string Section { get; set; }
    }
}