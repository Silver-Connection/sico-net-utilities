namespace SiCo.Utilities.Crypto.Test
{
    using Crypto;
    using Xunit;

    public class SHATest
    {
        [Fact]
        public void HashSHA1()
        {
            var c = SHA.HashSHA1("sampleTestSHA");
            Assert.NotNull(c);

            var b = SHA.HashSHA1("sampleTestSHA");
            Assert.NotNull(b);

            Assert.Equal(c, b);
        }

        [Fact]
        public void HashSHA512()
        {
            var c = SHA.HashSHA512("sampleTestSHA");
            Assert.NotNull(c);

            var b = SHA.HashSHA512("sampleTestSHA");
            Assert.NotNull(b);

            Assert.Equal(c, b);
        }
    }
}