namespace SiCo.Utilities.Helper.Test
{
    using System;
    using System.Linq;
    using Helper;
    using Xunit;

    public class TimeZonesTest
    {
        //[Fact]
        //public void Dispose()
        //{
        //    Assert.Equal(433, TimeZones.Lenght);
        //    TimeZones.Dispose();
        //    Assert.Equal(0, TimeZones.Lenght);
        //}

        [Theory]
        [InlineData("DE", 1)]
        [InlineData("DEU", 1)]
        public void GetZonesByIso(string iso, int result)
        {
            var c = TimeZones.GetZonesByIso(iso);
            Assert.Equal(result, c.Count());

            foreach (var item in c)
            {
                Assert.NotEmpty(item.Human);
                Assert.NotEmpty(item.Territory);
                Assert.NotEmpty(item.Type);
                Assert.NotEqual(DateTime.UtcNow, item.DateTime);
            }
        }

        [Fact]
        public void GetZonesByIso_Invalid()
        {
            // Invalid
            var c = TimeZones.GetZonesByIso("INVALID");
            Assert.Null(c);
        }

        [Fact]
        public void Initialize()
        {
            Assert.Equal(433, TimeZones.Lenght);
        }
    }
}