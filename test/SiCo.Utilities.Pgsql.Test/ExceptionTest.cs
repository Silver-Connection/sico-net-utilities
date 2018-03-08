namespace SiCo.Utilities.Pgsql.Test
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("Database collection")]
    public class ExceptionTest
    {
        private DatabaseFixture fixture;

        public ExceptionTest(DatabaseFixture fixture)
        {
            this.Common = new Common();
            this.fixture = fixture;
            this.Cancellation = new CancellationTokenSource();
        }

        public Common Common { get; set; }

        public CancellationTokenSource Cancellation { get; set; }


        [Fact]
        public void Model_IndexOutOfRange()
        {
            IndexOutOfRangeException ex = Assert.Throws<IndexOutOfRangeException>(() => Function.Model<Models.IndexMissmatchModel>("public.return_model", null, this.Common.ConnectionString));

            Assert.NotNull(ex);
        }

        [Fact]
        public void Model_InvalidCast()
        {
            InvalidCastException ex = Assert.Throws<InvalidCastException>(() => Function.Model<Models.WrongModel>("public.return_model", null, this.Common.ConnectionString));

            Assert.NotNull(ex);
        }

        [Fact]
        public void Model_MissingConstructor()
        {
            NotSupportedException ex = Assert.Throws<NotSupportedException>(() => Function.Model<Models.MissingModel>("public.return_model", null, this.Common.ConnectionString));

            Assert.NotNull(ex);
        }

        [Fact]
        public async void Cancellation_Before()
        {
            this.Cancellation.Cancel();
            var model = await Function.ModelAsync<Models.MissingModel>("public.return_model", null, this.Common.ConnectionString, this.Cancellation.Token);
            Assert.Null(model);
        }
    }
}