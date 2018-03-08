namespace SiCo.Utilities.Crypto.Test
{
    using SiCo.Utilities.Crypto;
    using Xunit;

    public class UpdateExtensionsTest
    {
        [Fact]
        public void UpdateId_Test()
        {
            int id = 5;
            var save = false;

            // Normal ID
            id = id.UpdateId(TripleDES.Encrypt(1), ref save);
            Assert.Equal(1, id);
            Assert.True(save);

            // Normal ID
            save = false;
            id = id.UpdateId(TripleDES.Encrypt(0), ref save);
            Assert.Equal(0, id);
            Assert.True(save);

            // Normal ID
            save = false;
            id = id.UpdateId(string.Empty, ref save);
            Assert.Equal(0, id);
            Assert.False(save);

            // Normal ID
            save = false;
            id = id.UpdateId(" ", ref save);
            Assert.Equal(0, id);
            Assert.False(save);

            // Normal ID
            save = false;
            id = id.UpdateId("Invalid", ref save);
            Assert.Equal(0, id);
            Assert.False(save);

            // Normal ID
            save = false;
            id = id.UpdateId(TripleDES.Encrypt(9), ref save);
            Assert.Equal(9, id);
            Assert.True(save);
        }

        [Fact]
        public void UpdateIdNull_Test()
        {
            int? id = 5;
            var save = false;

            // Normal ID
            id = id.UpdateId(TripleDES.Encrypt(1), ref save);
            Assert.Equal(1, id);
            Assert.True(save);

            // Normal ID
            save = false;
            id = id.UpdateId(TripleDES.Encrypt(0), ref save);
            Assert.Null(id);
            Assert.True(save);

            // Normal ID
            save = false;
            id = id.UpdateId(string.Empty, ref save);
            Assert.Null(id);
            Assert.False(save);

            // Normal ID
            save = false;
            id = id.UpdateId(" ", ref save);
            Assert.Null(id);
            Assert.False(save);

            // Normal ID
            save = false;
            id = id.UpdateId("Invalid", ref save);
            Assert.Null(id);
            Assert.False(save);

            // Normal ID
            save = false;
            id = id.UpdateId(TripleDES.Encrypt(9), ref save);
            Assert.Equal(9, id);
            Assert.True(save);
        }
    }
}