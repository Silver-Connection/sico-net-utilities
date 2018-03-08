namespace SiCo.Utilities.Generics.Test
{
    using Generics;
    using Xunit;

    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("+123456", false, "123456")]
        [InlineData("00123456", false, "123456")]
        [InlineData("00 123 456", false, "123456")]
        [InlineData("++123456", false, "123456")]
        [InlineData("++00123456", false, "123456")]
        [InlineData("000123456", false, "0123456")]
        [InlineData("000123456", true, "123456")]
        public void CleanPhoneNumber(string text, bool all, string result)
        {
            var c = text.CleanPhoneNumber(all);
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("abcde1", "1edcba")]
        public void Flip(string text, string result)
        {
            var c = text.Flip();
            Assert.Equal(result, c);
        }

        [Fact]
        public void IndexOfAllChar()
        {
            var text = "xabcdeabcaX";

            // Find x
            var c = text.IndexOfAll('x');
            Assert.Equal(new int[] { 0 }, c);

            // Find a
            c = text.IndexOfAll('a');
            Assert.Equal(new int[] { 1, 6, 9 }, c);

            // Find o
            c = text.IndexOfAll('o');
            Assert.Equal(new int[0], c);
        }

        [Fact]
        public void IndexOfAllString()
        {
            var text = "abc124abc56789abcYxzAbc";

            // Find x
            var c = text.IndexOfAll("abc");
            Assert.Equal(new int[] { 0, 6, 14 }, c);

            // Find o
            c = text.IndexOfAll('o');
            Assert.Equal(new int[0], c);
        }

        [Theory]
        [InlineData("Not Empty")]
        [InlineData(" Not Empty ")]
        public void IsEmptyFalse(string text)
        {
            var c = StringExtensions.IsEmpty(text);
            Assert.False(c);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void IsEmptyTrue(string text)
        {
            var c = StringExtensions.IsEmpty(text);
            Assert.True(c);
        }

        [Fact]
        public void SetTrimEmpty()
        {
            var text = "aA";
            var result = "bB";

            // Replace
            var c = text.SetTrim(result, true);
            Assert.Equal(result, c);

            c = text.SetTrim(result, false);
            Assert.Equal(result, c);

            text = string.Empty;
            c = text.SetTrim(result, false);
            Assert.Equal(result, c);

            text = string.Empty;
            c = text.SetTrim(result, true);
            Assert.Equal(result, c);

            c = text.SetTrim(string.Empty, false);
            Assert.Equal(text, c);

            c = text.SetTrim(string.Empty, true);
            Assert.Equal(string.Empty, c);
        }

        [Fact]
        public void SetTrimSave()
        {
            var text = "aA";
            var result = "bB";
            var save = false;

            // Replace
            var c = text.SetTrim(result, ref save);
            Assert.Equal(result, c);
            Assert.True(save);

            // Same value
            save = false;
            c = c.SetTrim(result, ref save);
            Assert.Equal(result, c);
            Assert.False(save);

            // Check trim
            save = false;
            c = c.SetTrim(" " + result + " ", ref save);
            Assert.Equal(result, c);
            Assert.True(save);

            // Set empty
            save = false;
            c = text.SetTrim(string.Empty, ref save);
            Assert.Equal(string.Empty, c);
            Assert.True(save);

            // Set empty trim
            save = false;
            text = result;
            c = text.SetTrim(" ", ref save);
            Assert.Equal(string.Empty, c);
            Assert.True(save);
        }

        [Theory]
        [InlineData("aa bb cc", "Aa bb cc")]
        public void ToUpperOnlyFirst(string text, string result)
        {
            var c = text.ToUpperOnlyFirst();
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("aa<TAG>aa", "<TAG>")]
        [InlineData("aaa<TAG>aaa", "a<TAG>a")]
        public void Trim(string text, string result)
        {
            var c = text.Trim("aa");
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("aa<TAG>aa", "<TAG>aa")]
        [InlineData("aaa<TAG>aaa", "a<TAG>aaa")]
        public void TrimFirst(string text, string result)
        {
            var c = text.TrimFirst("aa");
            Assert.Equal(result, c);
        }

        [Theory]
        [InlineData("aa<TAG>aa", "aa<TAG>")]
        [InlineData("aaa<TAG>aaa", "aaa<TAG>a")]
        public void TrimEnd(string text, string result)
        {
            var c = text.TrimEnd("aa");
            Assert.Equal(result, c);
        }

        [Fact]
        public void TrimModel()
        {
            var model = new Models.ModelSimple()
            {
                Age = 0,
                Comments = " Comment ",
                IsValid = true,
                Name = " Test "
            };

            var result = new Models.ModelSimple()
            {
                Age = 0,
                Comments = "Comment",
                IsValid = true,
                Name = "Test"
            };

            StringExtensions.TrimModel<Models.ModelSimple>(model);

            Assert.Equal(result.Comments, model.Comments);
            Assert.Equal(result.Name, model.Name);
        }

        [Theory]
        [InlineData("  <TAG>  ", "<TAG>")]
        public void TrimNotEmpty(string text, string result)
        {
            var c = text.TrimNotEmpty();
            Assert.Equal(result, c);

            // Trim empty string
            text = string.Empty;
            c = text.TrimNotEmpty();
            Assert.Equal(string.Empty, c);
        }
    }
}