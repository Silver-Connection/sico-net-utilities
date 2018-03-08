namespace SiCo.Utilities.Pgsql.Test.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Attributes;

    [Table("enum", Schema = "public")]
    public class EnumModel
    {
        public EnumModel()
        {
            this.Direction = Enums.Direction.Incoming;
            this.Name = string.Empty;
        }

        public EnumModel(Npgsql.NpgsqlDataReader reader)
        {
            if (!reader.IsDBNull(0))
            {
                this.Name = reader.GetString(0); 
            }

            if (!reader.IsDBNull(1))
            {
                this.Direction = reader.GetFieldValue<Enums.Direction>(1); 
            }
        }

        [ColumnPosition(1)]
        [Column("direction")]
        public Enums.Direction Direction { get; set; }

        [ColumnPosition(0)]
        [Column("name")]
        public string Name { get; set; }
    }
}