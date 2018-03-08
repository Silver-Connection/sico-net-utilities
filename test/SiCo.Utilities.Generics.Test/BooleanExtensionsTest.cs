namespace SiCo.Utilities.Generics.Test
{
    using SiCo.Utilities.Generics;
    using Xunit;

    public class BooleanExtensionsTest
    {
        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void ParseInt(bool number, int result)
        {
            var c = number.ParseInt();
            Assert.Equal(result, c);
        }
    }
}