namespace SiCo.Utilities.Pgsql.Test
{
    using System.Linq;
    using System.Threading;
    using Xunit;

    [Collection("Database collection")]
    public class FunctionTest
    {
        private DatabaseFixture fixture;

        public FunctionTest(DatabaseFixture fixture)
        {
            this.Common = new Common();
            this.fixture = fixture;
            this.Cancellation = new CancellationTokenSource();
        }

        public Common Common { get; set; }

        public CancellationTokenSource Cancellation { get; set; }


        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Boolean(bool check)
        {
            var param = new Npgsql.NpgsqlParameter[] {
                new Npgsql.NpgsqlParameter("p_boolean", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = check },
                };

            var query = Function.Boolean("public.return_boolean", param, this.Common.ConnectionString);
            Assert.Equal(!check, query);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void BooleanAsync(bool check)
        {
            var param = new Npgsql.NpgsqlParameter[] {
                new Npgsql.NpgsqlParameter("p_boolean", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = check },
                };

            var query = await Function.BooleanAsync("public.return_boolean", param, this.Common.ConnectionString, this.Cancellation.Token);
            Assert.Equal(!check, query);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void Integer(int check)
        {
            var param = new Npgsql.NpgsqlParameter[] {
                new Npgsql.NpgsqlParameter("p_integer", NpgsqlTypes.NpgsqlDbType.Integer) { Value = check },
                };

            var query = Function.Integer("public.return_number", param, this.Common.ConnectionString);
            Assert.Equal(check + 1, query);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async void IntegerAsync(int check)
        {
            var param = new Npgsql.NpgsqlParameter[] {
                new Npgsql.NpgsqlParameter("p_integer", NpgsqlTypes.NpgsqlDbType.Integer) { Value = check },
                };

            var query = await Function.IntegerAsync("public.return_number", param, this.Common.ConnectionString, this.Cancellation.Token);
            Assert.Equal(check + 1, query);
        }

        [Fact]
        public void Model()
        {
            var res = Function.Model<Models.SampleModel>("public.return_model", null, this.Common.ConnectionString);
            Assert.NotNull(res);

            Assert.Equal(1, res.Id);
            Assert.Equal("Test", res.Name);
            Assert.Equal(23, res.Age);
            Assert.True(res.IsValid);
        }

        [Fact]
        public async void ModelAsync()
        {
            var res = await Function.ModelAsync<Models.SampleModel>("public.return_model", null, this.Common.ConnectionString, this.Cancellation.Token);
            Assert.NotNull(res);

            Assert.Equal(1, res.Id);
            Assert.Equal("Test", res.Name);
            Assert.Equal(23, res.Age);
            Assert.True(res.IsValid);
        }

        [Theory]
        [InlineData("Test", 1234)]
        [InlineData("AbZc123", 789)]
        public void String(string text, int number)
        {
            var param = new Npgsql.NpgsqlParameter[] {
                new Npgsql.NpgsqlParameter("p_string", NpgsqlTypes.NpgsqlDbType.Text) { Value = text },
                new Npgsql.NpgsqlParameter("p_integer", NpgsqlTypes.NpgsqlDbType.Integer) { Value = number },
                };

            var res = Function.String("public.return_string", param, this.Common.ConnectionString);
            Assert.NotEmpty(res);
            Assert.Equal($"{text}{number}", res);
        }

        [Theory]
        [InlineData("Test", 1234)]
        [InlineData("AbZc123", 789)]
        public async void StringAsync(string text, int number)
        {
            var param = new Npgsql.NpgsqlParameter[] {
                new Npgsql.NpgsqlParameter("p_string", NpgsqlTypes.NpgsqlDbType.Text) { Value = text },
                new Npgsql.NpgsqlParameter("p_integer", NpgsqlTypes.NpgsqlDbType.Integer) { Value = number },
                };

            var res = await Function.StringAsync("public.return_string", param, this.Common.ConnectionString, this.Cancellation.Token);
            Assert.NotEmpty(res);
            Assert.Equal($"{text}{number}", res);
        }

        [Fact]
        public void Table()
        {
            // Add data
            var result = Query.Void($"DELETE FROM log;{this.Common.InsertLog}", this.Common.ConnectionString);
            Assert.True(result);

            var res = Function.Table<Models.LogModel>("public.return_table", null, this.Common.ConnectionString);
            Assert.NotNull(res);
            Assert.Equal(3, res.Count());

            foreach (var item in res)
            {
                //Assert.NotNull(item.Created);
                Assert.NotEmpty(item.Message);
                Assert.NotEmpty(item.Path);
                Assert.NotEmpty(item.Section);
            }
        }

        [Fact]
        public async void TableAsync()
        {
            // Add data
            var result = await Query.VoidAsync($"DELETE FROM log;{this.Common.InsertLog}", this.Common.ConnectionString, this.Cancellation.Token);
            Assert.True(result);

            var res = await Function.TableAsync<Models.LogModel>("public.return_table", null, this.Common.ConnectionString, this.Cancellation.Token);
            Assert.NotNull(res);
            Assert.Equal(3, res.Count());

            foreach (var item in res)
            {
                //Assert.NotNull(item.Created);
                Assert.NotEmpty(item.Message);
                Assert.NotEmpty(item.Path);
                Assert.NotEmpty(item.Section);
            }
        }
    }
}