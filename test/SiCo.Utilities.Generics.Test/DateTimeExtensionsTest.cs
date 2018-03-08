namespace SiCo.Utilities.Generics.Test
{
    using System;
    using SiCo.Utilities.Generics;
    using Xunit;

    public class DateTimeExtensionsTest
    {
        [Fact]
        public void DateTimeToApp()
        {
            Generics.DateTimeExtensions.CurrentUICulture = false;
            Generics.DateTimeExtensions.DateTime = "dd.MM.yyyy HH:mm:ss";
            Generics.DateTimeExtensions.Date = "dd.MM.yyyy";
            Generics.DateTimeExtensions.Time = "HH:mm:ss";

            var date = new DateTime(2000, 12, 24, 0, 0, 0);

            var c = date.ToAppDateString();
            Assert.Equal("24.12.2000", c);

            c = date.ToAppString();
            Assert.Equal("24.12.2000 00:00:00", c);

            c = date.ToAppTimeString();
            Assert.Equal("00:00:00", c);
        }
    }
}