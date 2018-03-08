namespace SiCo.Utilities.Generics.Test
{
    using Generics;
    using Xunit;

    public class DisplayClassExtensionsTest
    {
        [Fact]
        public void ToClassDisplayString_String()
        {
            var model = new Models.DisplayAttributeStringModel();
            var name = model.ToClassDisplayString();

            Assert.NotEmpty(name);
            Assert.Equal("Test Class Name", name);
        }

        [Fact]
        public void ToClassDisplayString_Resources()
        {
            var model = new Models.DisplayAttributeResourcesModel();
            var name = model.ToClassDisplayString();

            Assert.NotEmpty(name);
            Assert.Equal("Name", name);
        }
    }
}