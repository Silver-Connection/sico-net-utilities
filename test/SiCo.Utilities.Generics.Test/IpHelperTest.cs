namespace SiCo.Utilities.Generics.Test
{
    using System.Linq;
    using System.Net;
    using Generics;
    using Xunit;

    public class IpHelperTest
    {
        [Fact]
        public void GetIpRangeEdge()
        {
            var c = IpHelper.GetIpRangeEdge("127.0.0.1", 32);
            Assert.Equal(IPAddress.Parse("127.0.0.1"), c.First());
            Assert.Equal(IPAddress.Parse("127.0.0.1"), c.Last());

            c = IpHelper.GetIpRangeEdge("127.0.0.1", 31);
            Assert.Equal(IPAddress.Parse("127.0.0.0"), c.First());
            Assert.Equal(IPAddress.Parse("127.0.0.1"), c.Last());

            c = IpHelper.GetIpRangeEdge("127.0.0.1", 30);
            Assert.Equal(IPAddress.Parse("127.0.0.0"), c.First());
            Assert.Equal(IPAddress.Parse("127.0.0.3"), c.Last());

            c = IpHelper.GetIpRangeEdge("127.0.0.1", 24);
            Assert.Equal(IPAddress.Parse("127.0.0.0"), c.First());
            Assert.Equal(IPAddress.Parse("127.0.0.255"), c.Last());

            c = IpHelper.GetIpRangeEdge("127.0.0.1", 20);
            Assert.Equal(IPAddress.Parse("127.0.0.0"), c.First());
            Assert.Equal(IPAddress.Parse("127.0.15.255"), c.Last());

            var log = c.First().ToString() + " : " + c.Last().ToString();
            //Log.FileLogger.Info(log, typeof(IpHelperTest));
        }

        [Fact]
        public void GetIpAdressList()
        {
            var c = IpHelper.GetIpAdressList("127.0.0.1", 32);
            Assert.Single(c);
            Assert.Equal(IPAddress.Parse("127.0.0.1"), c.First());

            c = IpHelper.GetIpAdressList("127.0.0.1", 31);
            Assert.Equal(2, c.Count());
            Assert.Equal(IPAddress.Parse("127.0.0.0"), c.First());
            Assert.Equal(IPAddress.Parse("127.0.0.1"), c.Last());

            c = IpHelper.GetIpAdressList("127.0.0.1", 24);
            ////var log = c.Select(a => a.ToString()).ToArray();
            ////Log.FileLogger.Json(log, typeof(IpHelperTest));
            Assert.Equal(256, c.Count());
            Assert.Equal(IPAddress.Parse("127.0.0.0"), c.First());
            Assert.Equal(IPAddress.Parse("127.0.0.255"), c.Last());
        }
    }
}