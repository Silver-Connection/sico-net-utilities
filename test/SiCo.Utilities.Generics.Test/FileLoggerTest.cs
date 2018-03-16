namespace SiCo.Utilities.Generics.Test
{
    using System.Threading.Tasks;
    using Xunit;

    [Collection("CommonCollection")]
    public class FileLoggerTest
    {
        public FileLoggerTest()
        {
        }

        //[Fact]
        //public void Error()
        //{
        //    FileLogger.Error("Test Error Log", typeof(FileLoggerTest));
        //}

        //[Fact]
        //public void Info()
        //{
        //    FileLogger.Info("Test Info Log", typeof(FileLoggerTest));
        //}

        //[Fact]
        //public void Json()
        //{
        //    var result = new Resources.ModelSimple()
        //    {
        //        Age = 0,
        //        Comments = "Comment",
        //        IsValid = true,
        //        Name = "Test",
        //        Password = "safe1234",
        //        Pin = "1234"
        //    };
        //    FileLogger.Json(result);
        //}

        [Fact]
        public async Task ErrorAsync()
        {
            await FileLogger.ErrorAsync("Test Error Log", typeof(FileLoggerTest));
        }

        [Fact]
        public async Task InfoAsync()
        {
            await FileLogger.InfoAsync("Test Info Log", typeof(FileLoggerTest));
        }

        [Fact]
        public async Task JsonAsync()
        {
            var result = new Models.ModelSimple()
            {
                Age = 0,
                Comments = "Comment",
                IsValid = true,
                Name = "Test",
            };
            await FileLogger.JsonAsync(result);
        }
    }
}