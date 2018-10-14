namespace SiCo.Utilities.Generics.Test
{
    using System;

    //using Helper;
    using System.Net;
    using SiCo.Utilities.Generics;
    using Xunit;

    public class UpdateExtensionsTest
    {
        [Fact]
        public void Update_Bool()
        {
            var model = new Models.UpdateModel();
            var save = false;

            // Boolean
            model.Bool1 = model.Bool1.Update(false, ref save);
            Assert.False(model.Bool1);
            Assert.True(save);

            // Boolean
            save = false;
            model.Bool2 = model.Bool2.Update(true, ref save);
            Assert.True(model.Bool2);
            Assert.False(save);

            // Boolean Null
            save = false;
            model.Bool3 = model.Bool3.Update(true, ref save);
            Assert.True(model.Bool3);
            Assert.True(save);

            // Boolean Null
            save = false;
            model.Bool3 = model.Bool3.Update(null, ref save);
            Assert.Null(model.Bool3);
            Assert.True(save);
        }

        [Fact]
        public void Update_Double()
        {
            var model = new Models.UpdateModel();
            var save = false;

            // Double
            model.Double1 = model.Double1.Update(5, ref save);
            Assert.Equal(5, model.Double1);
            Assert.True(save);

            // Double
            save = false;
            model.Double2 = model.Double2.Update(1, ref save);
            Assert.Equal(1, model.Double2);
            Assert.False(save);

            // Double Null
            save = false;
            model.Double3 = model.Double3.Update(1, ref save);
            Assert.Equal(1, model.Double3);
            Assert.True(save);

            // Double Null
            save = false;
            model.Double3 = model.Double3.Update(null, ref save);
            Assert.Null(model.Double3);
            Assert.True(save);
        }

        [Fact]
        public void Update_Enum()
        {
            var model = new Models.UpdateModel();
            var save = false;

            // Enum
            model.Enum1 = model.Enum1.Update(Enums.YesNo.No, ref save);
            Assert.Equal(Enums.YesNo.No, model.Enum1);
            Assert.True(save);

            // Enum
            save = false;
            model.Enum2 = model.Enum2.Update(Enums.Code.Active, ref save);
            Assert.Equal(Enums.Code.Active, model.Enum2);
            Assert.True(save);

            // Enum
            save = false;
            model.Enum2 = model.Enum2.Update(Enums.Code.Active, ref save);
            Assert.Equal(Enums.Code.Active, model.Enum2);
            Assert.False(save);
        }

        [Fact]
        public void Update_DateTime()
        {
            var model = new Models.UpdateModel();
            var save = false;

            // Change
            var date = DateTime.UtcNow.AddDays(2);
            var dateString = date.ToAppString();
            model.Date1 = model.Date1.Update(dateString, DateTimeExtensions.DateTime, ref save);
            Assert.Equal(date.ToString(), model.Date1.ToString());
            Assert.True(save);

            // No change
            save = false;
            model.Date1 = model.Date1.Update(string.Empty, DateTimeExtensions.DateTime, ref save);
            Assert.Equal(date.ToString(), model.Date1.ToString());
            Assert.False(save);

            // Invalid
            save = false;
            model.Date1 = model.Date1.Update("INVALID", DateTimeExtensions.DateTime, ref save);
            Assert.Equal(date.ToString(), model.Date1.ToString());
            Assert.False(save);

            // Change
            save = false;
            model.Date2 = model.Date2.Update(dateString, DateTimeExtensions.DateTime, ref save);
            Assert.Equal(date.ToString(), model.Date1.ToString());
            Assert.True(save);

            // Change
            save = false;
            model.Date2 = model.Date2.Update(string.Empty, DateTimeExtensions.DateTime, ref save);
            Assert.Null(model.Date2);
            Assert.True(save);

            // Invalid
            save = false;
            model.Date2 = model.Date2.Update("INVALID", DateTimeExtensions.DateTime, ref save);
            Assert.Null(model.Date2);
            Assert.False(save);
        }

        [Fact]
        public void Update_IpAddress()
        {
            var model = new Models.UpdateModel();
            var save = false;

            // Change
            var ip = IPAddress.Parse("127.0.0.2");
            model.Ip1 = model.Ip1.Update(ip, ref save);
            Assert.Equal(ip, model.Ip1);
            Assert.True(save);

            // Set null
            save = false;
            model.Ip1 = model.Ip1.Update(string.Empty, ref save);
            Assert.Null(model.Ip1);
            Assert.True(save);

            // No Change
            save = false;
            model.Ip2 = model.Ip2.Update(string.Empty, ref save);
            Assert.Null(model.Ip2);
            Assert.False(save);

            // Change
            save = false;
            model.Ip2 = model.Ip2.Update("127.0.0.2", ref save);
            Assert.Null(model.Ip2);
            Assert.True(save);

            // Invalid
            save = false;
            model.Ip2 = model.Ip2.Update("INVALID", ref save);
            Assert.Null(model.Ip2);
            Assert.False(save);
        }

        [Fact]
        public void Update_Float()
        {
            var model = new Models.UpdateModel();
            var save = false;

            // Float
            model.Float1 = model.Float1.Update(5, ref save);
            Assert.Equal(5, model.Float1);
            Assert.True(save);

            // Float
            save = false;
            model.Float2 = model.Float2.Update(1, ref save);
            Assert.Equal(1, model.Float2);
            Assert.False(save);

            // Float Null
            save = false;
            model.Float3 = model.Float3.Update(1, ref save);
            Assert.Equal(1, model.Float3);
            Assert.True(save);

            // Float Null
            save = false;
            model.Float3 = model.Float3.Update(null, ref save);
            Assert.Null(model.Float3);
            Assert.True(save);
        }

        [Fact]
        public void Update_Int()
        {
            var model = new Models.UpdateModel();
            var save = false;

            // Int
            model.Int1 = model.Int1.Update(5, ref save);
            Assert.Equal(5, model.Int1);
            Assert.True(save);

            // Int
            save = false;
            model.Int2 = model.Int2.Update(1, ref save);
            Assert.Equal(1, model.Int2);
            Assert.False(save);

            // Int Null
            save = false;
            model.Int3 = model.Int3.Update(1, ref save);
            Assert.Equal(1, model.Int3);
            Assert.True(save);

            // Int Null
            save = false;
            model.Int3 = model.Int3.Update(null, ref save);
            Assert.Null(model.Int3);
            Assert.True(save);
        }

        [Fact]
        public void Update_String()
        {
            var model = new Models.UpdateModel();
            var save = false;

            // String
            model.String1 = model.String1.Update("Test", ref save);
            Assert.Equal("Test", model.String1);
            Assert.True(save);

            save = false;
            model.String1 = model.String1.Update(" Test  ", ref save);
            Assert.Equal("Test", model.String1);
            Assert.False(save);

            // String
            save = false;
            model.String2 = model.String2.Update(" ", ref save);
            Assert.Equal(string.Empty, model.String2);
            Assert.True(save);

            // String
            save = false;
            model.String2 = model.String2.Update(string.Empty, ref save);
            Assert.Equal(string.Empty, model.String2);
            Assert.False(save);
        }
    }
}