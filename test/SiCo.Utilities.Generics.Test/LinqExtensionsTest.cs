namespace SiCo.Utilities.Generics.Test
{
    using Generics;
    using Xunit;

    public class LinqExtensionsTest
    {
        [Fact]
        public void Deduplicate()
        {
            var root = new string[] { "a", "a", "b", "c", "c ", "b" };
            var result = new string[] { "a", "b", "c", "c " };
            var text = root.Deduplicate();
            Assert.Equal(result, text);
        }
    }
}