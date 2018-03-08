namespace SiCo.Utilities.Generics.Test.Models
{
    using System;
    using System.Net;
    public class UpdateModel
    {
        public UpdateModel()
        {
            this.Bool1 = true;
            this.Bool2 = true;
            this.Bool3 = null;

            this.Date1 = DateTime.UtcNow;
            this.Date2 = null;

            this.Double1 = 0;
            this.Double2 = 1;
            this.Double3 = null;

            this.Float1 = 0;
            this.Float2 = 1;
            this.Float3 = null;

            this.Int1 = 0;
            this.Int2 = 1;
            this.Int3 = null;

            this.Ip1 = IPAddress.Parse("127.0.0.1");
            this.Ip2 = null;

            this.Enum1 = Enums.YesNo.Yes;
            this.Enum2 = Enums.Code.Unset;

            this.String1 = string.Empty;
            this.String2 = "Test";
        }

        public bool Bool1 { get; set; }
        public bool Bool2 { get; set; }
        public bool? Bool3 { get; set; }

        public double Double1 { get; set; }
        public double Double2 { get; set; }
        public double? Double3 { get; set; }

        public DateTime Date1 { get; set; }
        public DateTime? Date2 { get; set; }

        public Enums.YesNo Enum1 { get; set; }
        public Enums.Code Enum2 { get; set; }

        public float Float1 { get; set; }
        public float Float2 { get; set; }
        public float? Float3 { get; set; }

        public int Int1 { get; set; }
        public int Int2 { get; set; }
        public int? Int3 { get; set; }

        public IPAddress Ip1 { get; set; }
        public IPAddress Ip2 { get; set; }

        public string String1 { get; set; }
        public string String2 { get; set; }
    }
}