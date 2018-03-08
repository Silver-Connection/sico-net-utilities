namespace SiCo.Utilities.Crypto.Test
{
    using System;
    using Crypto;
    using Xunit;

    public class TripleDESTest
    {
        [Theory]
        [InlineData(0, "bgaR7gIt(sl)r0(eq)")]
        [InlineData(1, "T558Xw6PEaI(eq)")]
        [InlineData(159, "hnHPsAsrrJs(eq)")]
        public void Encrypt(object text, string result)
        {
            var c = TripleDES.Encrypt(text);
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("bgaR7gIt(sl)r0(eq)", 0)]
        [InlineData("bgaR7gIt(sl)r0(eq)INVALID", 0)]
        [InlineData("T558Xw6PEaI(eq)", 1)]
        [InlineData("hnHPsAsrrJs(eq)", 159)]
        public void DecryptIDZero(string text, int result)
        {
            var c = TripleDES.DecryptIDZero(text);
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("bgaR7gIt(sl)r0(eq)", null)]
        [InlineData("bgaR7gIt(sl)r0(eq)INAVLID", null)]
        [InlineData("T558Xw6PEaI(eq)", 1)]
        [InlineData("hnHPsAsrrJs(eq)", 159)]
        public void DecryptIDNull(string text, int? result)
        {
            var c = TripleDES.DecryptIDNull(text);
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("T558Xw6PEaI(eq)", 1)]
        [InlineData("hnHPsAsrrJs(eq)", 159)]
        public void DecryptID(string text, int result)
        {
            var c = TripleDES.DecryptID(text);
            Assert.Equal(result, c);
        }

        [Fact]
        public void Encrypt_Input_NullEmpty()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => TripleDES.Encrypt(string.Empty));
            Assert.NotNull(ex);

            ex = Assert.Throws<ArgumentNullException>(() => TripleDES.Encrypt(null));
            Assert.NotNull(ex);
        }

        [Fact]
        public void Encrypt_Input_Invalid()
        {
            NotImplementedException ex = Assert.Throws<NotImplementedException>(() => TripleDES.Encrypt(true));
            Assert.NotNull(ex);
        }

        [Fact]
        public void Dencrypt_Input_NullEmpty()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => TripleDES.Decrypt(string.Empty));
            Assert.NotNull(ex);
        }

        [Fact]
        public void Dencrypt_Input_Invalid()
        {
            Exception ex = Assert.Throws<Exception>(() => TripleDES.Decrypt("Invalid"));
            Assert.NotNull(ex);
        }

        [Fact]
        public void DecryptID_Input_0()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => TripleDES.DecryptID("bgaR7gIt(sl)r0(eq)"));
            Assert.NotNull(ex);
        }
    }
}