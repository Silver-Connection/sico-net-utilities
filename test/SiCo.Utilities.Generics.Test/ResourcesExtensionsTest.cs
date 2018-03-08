namespace SiCo.Utilities.Generics.Test
{
    using Generics;
    using Xunit;

    public class ResourcesExtensionsTest
    {
        [Fact]
        public void ConcatFile()
        {
            var root = "SiCo.Utilities.Generics.Test.Resources.";
            var result = "FILE1CONTENTFILE2CONTENT";
            var text = ResourcesExtensions.ConcatFile(
                typeof(StringExtensionsTest),
                root + "File1.txt",
                root + "File2.txt");
            Assert.Equal(result, text);
        }
    }
}