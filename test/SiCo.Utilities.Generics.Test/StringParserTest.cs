namespace SiCo.Utilities.Generics.Test
{
    using SiCo.Utilities.Generics;
    using Xunit;

    public class StringParserTest
    {
        [Theory]
        [InlineData("true", true)]
        [InlineData("TRUE", true)]
        [InlineData("True", true)]
        [InlineData("1", true)]
        [InlineData("false", false)]
        [InlineData("FALSE", false)]
        [InlineData("False", false)]
        [InlineData("0", false)]
        [InlineData("INVALID", false)]
        public void ParsBool(string text, bool result)
        {
            var c = text.ParsBool();
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("INVALID", null)]
        public void ParsBoolNull(string text, bool? result)
        {
            var c = text.ParsBoolNull();
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("5982", 5982)]
        [InlineData("INVALID", 0)]
        public void ParsInt(string text, int result)
        {
            var c = text.ParsInt();
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("5982", 5982)]
        [InlineData("INVALID", null)]
        public void ParsIntNull(string text, int? result)
        {
            var c = text.ParsIntNull();
            Assert.Equal(result, c);
        }
    }
}