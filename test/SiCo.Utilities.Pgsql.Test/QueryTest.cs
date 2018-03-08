namespace SiCo.Utilities.Pgsql.Test
{
    using System;
    using System.Linq;
    using System.Threading;
    using Xunit;

    [Collection("Database collection")]
    public class QueryTest
    {
        private DatabaseFixture fixture;

        public QueryTest(DatabaseFixture fixture)
        {
            this.Common = new Common();
            this.fixture = fixture;
            this.Cancellation = new CancellationTokenSource();
        }

        public CancellationTokenSource Cancellation { get; set; }

        public Common Common { get; set; }

        [Fact]
        public void Boolean()
        {
            var query = Query.Boolean("SELECT true AS sample;", this.Common.ConnectionString);
            Assert.True(query);
        }

        [Fact]
        public async void BooleanAsync()
        {
            var query = await Query.BooleanAsync("SELECT true AS sample;", this.Common.ConnectionString, this.Cancellation.Token);
            Assert.True(query);
        }

        [Fact]
        public void Insert()
        {
            var model = new Models.SampleModel()
            {
                Age = 56,
                Created = DateTime.UtcNow,
                Id = 2,
                IsValid = false,
                Name = "Add"
            };

            // Clean
            var clean = Query.Void("DELETE FROM sample WHERE id = 2;", this.Common.ConnectionString);
            Assert.True(clean);

            // Insert
            var query = Query.Insert(model, this.Common.ConnectionString);
            Assert.Equal(2, query);
        }

        [Fact]
        public async void InsertAsync()
        {
            var model = new Models.SampleModel()
            {
                Age = 56,
                Created = DateTime.UtcNow,
                Id = 3,
                IsValid = false,
                Name = "AddAsync"
            };

            // Clean
            var clean = await Query.VoidAsync("DELETE FROM sample WHERE id = 3;", this.Common.ConnectionString, this.Cancellation.Token);
            Assert.True(clean);

            // Insert
            var query = await Query.InsertAsync(model, this.Common.ConnectionString, this.Cancellation.Token);
            Assert.Equal(3, query);
        }

        [Fact]
        public void Integer()
        {
            var query = Query.Integer("SELECT 1234 AS sample;", this.Common.ConnectionString);
            Assert.Equal(1234, query);
        }

        [Fact]
        public async void IntegerAsync()
        {
            var query = await Query.IntegerAsync("SELECT 1234 AS sample;", this.Common.ConnectionString, this.Cancellation.Token);
            Assert.Equal(1234, query);
        }

        [Fact]
        public void Model_Query()
        {
            var res = Query.Model<Models.SampleModel>("SELECT * FROM public.sample WHERE id = 1;", this.Common.ConnectionString);
            Assert.NotNull(res);

            Assert.Equal("First", res.Name);
            Assert.Equal(23, res.Age);
            Assert.Equal(1, res.Id);
            Assert.True(res.IsValid);
        }

        [Fact]
        public async void ModelAsync_Query()
        {
            var res = await Query.ModelAsync<Models.SampleModel>("SELECT * FROM public.sample WHERE id = 1;", this.Common.ConnectionString, this.Cancellation.Token);
            Assert.NotNull(res);

            Assert.Equal("First", res.Name);
            Assert.Equal(23, res.Age);
            Assert.Equal(1, res.Id);
            Assert.True(res.IsValid);
        }

        [Fact]
        public void String()
        {
            // Add data
            var result = Query.Void($"DELETE FROM log;{this.Common.InsertLog}", this.Common.ConnectionString);
            Assert.True(result);

            var query = Query.String("SELECT message FROM public.log ORDER BY id DESC LIMIT 1;", this.Common.ConnectionString);
            Assert.NotEmpty(query);
            Assert.Equal("Add sample log 3", query);
        }

        [Fact]
        public async void StringAsync()
        {
            // Add data
            var result = await Query.VoidAsync($"DELETE FROM log;{this.Common.InsertLog}", this.Common.ConnectionString, this.Cancellation.Token);
            Assert.True(result);

            var query = await Query.StringAsync("SELECT message FROM public.log ORDER BY id DESC LIMIT 1;", this.Common.ConnectionString, this.Cancellation.Token);
            Assert.NotEmpty(query);
            Assert.Equal("Add sample log 3", query);
        }

        [Fact]
        public void Table()
        {
            // Add data
            var result = Query.Void($"DELETE FROM log;{this.Common.InsertLog}", this.Common.ConnectionString);
            Assert.True(result);

            var res = Query.Table<Models.LogModel>("SELECT path, section, message, created FROM log;", this.Common.ConnectionString);
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
        public void Table_Model_Enum_Only()
        {
            //Npgsql.NpgsqlConnection.MapEnumGlobally<Enums.Direction>();
            var res = Query.Table(new Models.EnumModel(), this.Common.ConnectionString);
            Assert.NotNull(res);

            var item = res.FirstOrDefault();
            Assert.Equal("RequestA", item.Name);
            Assert.Equal(Enums.Direction.Incoming, item.Direction);
        }

        [Fact]
        public void Table_Model_Only()
        {
            var res = Query.Table(new Models.SampleModel(), this.Common.ConnectionString);
            Assert.NotNull(res);

            var item = res.FirstOrDefault();
            Assert.Equal("First", item.Name);
            Assert.Equal(23, item.Age);
            Assert.Equal(1, item.Id);
            Assert.True(item.IsValid);
        }

        [Fact]
        public async void TableAsync()
        {
            // Add data
            var result = await Query.VoidAsync($"DELETE FROM log;{this.Common.InsertLog}", this.Common.ConnectionString, this.Cancellation.Token);
            Assert.True(result);

            var res = await Query.TableAsync<Models.LogModel>("SELECT path, section, message, created FROM log;", this.Common.ConnectionString, this.Cancellation.Token);
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
        public async void TableAsync_Model_Only()
        {
            var res = await Query.TableAsync(new Models.SampleModel(), this.Common.ConnectionString, this.Cancellation.Token);
            Assert.NotNull(res);

            var item = res.FirstOrDefault();
            Assert.Equal("First", item.Name);
            Assert.Equal(23, item.Age);
            Assert.Equal(1, item.Id);
            Assert.True( item.IsValid);
        }

        [Fact]
        public void Void()
        {
            var result = Query.Void(this.Common.InsertLog, this.Common.ConnectionString);
            Assert.True(result);
        }

        [Fact]
        public async void VoidAsync()
        {
            var result = await Query.VoidAsync(this.Common.InsertLog, this.Common.ConnectionString, this.Cancellation.Token);
            Assert.True(result);
        }
    }
}