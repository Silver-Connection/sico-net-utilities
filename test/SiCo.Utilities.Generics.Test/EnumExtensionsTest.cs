namespace SiCo.Utilities.Generics.Test
{
    using SiCo.Utilities.Generics;
    using Xunit;

    public class EnumExtensionsTest
    {
        [Fact]
        public void GetFullInfo()
        {
            // Has no display attribute
            var result = EnumExtensions.GetFullInfo(typeof(Enums.EnumSimple));
            Assert.NotNull(result);

            // Has display attribute
            result = EnumExtensions.GetFullInfo(typeof(Enums.EnumDisplay));
            Assert.NotNull(result);

            // Has display attribute with resource
            result = EnumExtensions.GetFullInfo(typeof(Enums.EnumDisplayResources));
            Assert.NotNull(result);

            foreach (var item in result)
            {
                Assert.NotEmpty(item.DisplayName);
                Assert.NotEmpty(item.Name);
            }
        }

        [Fact]
        public void GetKeyValueList()
        {
            // Has no display attribute
            var result = EnumExtensions.GetKeyValueList(typeof(Enums.EnumSimple));
            Assert.NotNull(result);

            // Has display attribute
            result = EnumExtensions.GetKeyValueList(typeof(Enums.EnumDisplay));
            Assert.NotNull(result);

            foreach (var item in result)
            {
                Assert.NotEmpty(item.Key);
                Assert.NotNull(item.Value);
            }
        }

        [Fact]
        public void ToDisplayString()
        {
            // Has display attribute
            var result = Enums.EnumDisplay.Stable.ToDisplayString();
            Assert.NotEmpty(result);

            // Has display attribute with resource
            result = Enums.EnumDisplayResources.Development.ToDisplayString();
            Assert.NotEmpty(result);
        }
    }
}