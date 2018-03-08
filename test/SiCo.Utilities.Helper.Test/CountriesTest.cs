namespace SiCo.Utilities.Helper.Test
{
    using System.Linq;
    using Helper;
    using Xunit;
    using Xunit.Abstractions;

    public class CountriesTest
    {
        private readonly ITestOutputHelper output;

        public CountriesTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("DE", "DEU")]
        [InlineData("de", "DEU")]
        [InlineData("DEU", "DE")]
        [InlineData("deu", "DE")]
        public void ConvertIso(string iso, string result)
        {
            //Countries.Initialize();
            var c = Countries.ConvertIso(iso);
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("Germany", "DEU")]
        [InlineData("germany", "DEU")]
        [InlineData("Saint Pierre and Miquelon", "SPM")]
        public void GetCountryByName(string iso, string result)
        {
            //Countries.Initialize();
            var c = Countries.GetCountryByName(iso);
            Assert.NotNull(c);
            Assert.Equal(result, c.ISO3);
        }

        //[Fact]
        //public void Dispose()
        //{
        //    //Countries.Initialize();
        //    Assert.Equal(255, Countries.Lenght);
        //    Countries.Dispose();
        //    Assert.Equal(0, Countries.Lenght);
        //}

        [Fact]
        public void GetCountryByIso()
        {
            //Countries.Initialize();

            // ISO A2
            var result = Countries.GetCountryByIso("DE");
            Assert.NotNull(result);
            Assert.NotNull(result.FlagData);
            Assert.NotEmpty(result.FlagMime);
            Assert.NotEmpty(result.ISO2);
            Assert.NotEmpty(result.ISO3);
            Assert.NotEmpty(result.NameFallback);

            // Check flag
            Assert.NotEmpty(result.GetFlagBase64());

            result = Countries.GetCountryByIso("de");
            Assert.NotNull(result);
            Assert.NotNull(result.FlagData);
            Assert.NotEmpty(result.FlagMime);
            Assert.NotEmpty(result.ISO2);
            Assert.NotEmpty(result.ISO3);
            Assert.NotEmpty(result.NameFallback);

            // ISO A3
            result = Countries.GetCountryByIso("DEU");
            Assert.NotNull(result);
            Assert.NotNull(result.FlagData);
            Assert.NotEmpty(result.FlagMime);
            Assert.NotEmpty(result.ISO2);
            Assert.NotEmpty(result.ISO3);
            Assert.NotEmpty(result.NameFallback);

            result = Countries.GetCountryByIso("dEu");
            Assert.NotNull(result);
            Assert.NotNull(result.FlagData);
            Assert.NotEmpty(result.FlagMime);
            Assert.NotEmpty(result.ISO2);
            Assert.NotEmpty(result.ISO3);
            Assert.NotEmpty(result.NameFallback);

            // Invalid
            result = Countries.GetCountryByIso("PP");
            Assert.Null(result);

            result = Countries.GetCountryByIso("PPP");
            Assert.Null(result);

            result = Countries.GetCountryByIso("P");
            Assert.Null(result);

            result = Countries.GetCountryByIso("PPPP");
            Assert.Null(result);
        }

        [Fact]
        public void GetKeyValueList()
        {
            //Countries.Initialize();
            var result = Countries.GetKeyValueList();
            Assert.Equal(255, result.Count());

            foreach (var item in result)
            {
                Assert.NotEmpty(item.Key);
                Assert.NotEmpty(item.Value);
            }
        }

        [Fact]
        public void GetList()
        {
            //Countries.Initialize();
            var result = Countries.GetList();
            Assert.Equal(255, result.Count());

            foreach (var item in result)
            {
                Assert.NotEmpty(item.ISO2);
                Assert.NotEmpty(item.ISO3);
                Assert.NotEmpty(item.Name);
            }
        }

        [Fact]
        public void GetISOList()
        {
            //Countries.Initialize();
            var result = Countries.GetIsoList();
            Assert.Equal(255, result.Count());

            foreach (var item in result)
            {
                Assert.NotEmpty(item);
                Assert.Equal(3, item.Length);
            }

            result = Countries.GetIsoList(true);
            Assert.Equal(255, result.Count());

            foreach (var item in result)
            {
                Assert.NotEmpty(item);
                Assert.Equal(2, item.Length);
            }
        }

        [Theory]
        [InlineData("DE")]
        [InlineData("DEU")]
        public void GetName(string iso)
        {
            //Countries.Initialize();
            var c = Countries.GetName(iso);
            Assert.NotEmpty(c);
        }

        [Fact]
        public void Initialize()
        {
            //Countries.Initialize();
            Assert.Equal(255, Countries.Lenght);
        }

        [Theory]
        [InlineData("DE", true)]
        [InlineData("de", true)]
        [InlineData("DEU", true)]
        [InlineData("deu", true)]
        [InlineData("p", false)]
        [InlineData("pp", false)]
        [InlineData("ppp", false)]
        [InlineData("pppp", false)]
        public void IsValid(string iso, bool result)
        {
            //Countries.Initialize();
            var c = Countries.IsValid(iso);
            Assert.Equal(result, c);
        }
    }
}