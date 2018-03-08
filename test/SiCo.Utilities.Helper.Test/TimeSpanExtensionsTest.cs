namespace SiCo.Utilities.Helper.Test
{
    using System;
    using Helper;
    using Xunit;

    public class TimeSpanExtensionsTest
    {
        [Fact]
        public void ToHToDayHourMinuteSecondStringumanString()
        {
            var t = new TimeSpan(5, 20, 15, 33);
            var c = t.ToDayHourMinuteSecondString(false);
            Assert.NotEmpty(c);

            c = t.ToDayHourMinuteSecondString(true);
            Assert.NotEmpty(c);
        }
    }
}