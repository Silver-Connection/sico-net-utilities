namespace SiCo.Utilities.Pgsql.Test
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class BuilderTest
    {
        [Fact]
        public void Insert_Column_Value_Missmatch()
        {
            var model = new Builder.InsertModel();
            model.Columns.Add("id");

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => model.Build());

            Assert.NotNull(ex);
        }

        [Fact]
        public void Insert_Test()
        {
            var model = new Models.SampleModel()
            {
                Age = 15,
                Created = new DateTime(2016, 05, 01, 23, 23, 0),
                Id = 56,
                IsValid = true,
                Name = "Test"
            };

            var insert = Builder.InsertModel.Create(model);
            Assert.NotNull(insert);
            Assert.NotNull(insert.Columns);
            Assert.NotNull(insert.Values);

            Assert.Equal(5, insert.Columns.Count);
            Assert.Equal(5, insert.Values.Count);

            Assert.Equal("age", insert.Columns[0]);
            Assert.Equal("created", insert.Columns[1]);
            Assert.Equal("id", insert.Columns[2]);
            Assert.Equal("is_valid", insert.Columns[3]);
            Assert.Equal("name", insert.Columns[4]);

            Assert.Equal("15", insert.Values[0]);
            Assert.Equal("'2016-05-01 23:23:00'", insert.Values[1]);
            Assert.Equal("56", insert.Values[2]);
            Assert.Equal("true", insert.Values[3]);
            Assert.Equal("'Test'", insert.Values[4]);

            Assert.Equal("\"public\".\"sample\"", insert.FullName);

            var query = $"INSERT INTO \"public\".\"sample\"(\"age\", \"created\", \"id\", \"is_valid\", \"name\") VALUES (15, '2016-05-01 23:23:00', 56, true, 'Test') RETURNING id;";
            Assert.Equal(query, insert.Build());

            query = $"INSERT INTO \"public\".\"sample\"(\"age\", \"created\", \"is_valid\", \"name\") VALUES (15, '2016-05-01 23:23:00', true, 'Test') RETURNING id;";
            Assert.Equal(query, insert.Build(true));

            var add = new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("gender", "'male'") };
            query = $"INSERT INTO \"public\".\"sample\"(\"age\", \"created\", \"is_valid\", \"name\", \"gender\") VALUES (15, '2016-05-01 23:23:00', true, 'Test', 'male') RETURNING id;";
            Assert.Equal(query, insert.Build(true, add));
        }

        [Fact]
        public void Insert_Value_Test()
        {
            var model = new Builder.InsertModel();
            model.Columns.Add("id");
            model.Values.Add("0");

            model.ValueSet("id", "23");
            Assert.Equal(1, model.Columns.Count);
            Assert.Equal(1, model.Values.Count);
            Assert.Equal("23", model.Values[0]);

            model.ValueAdd("name", "'Test'");
            Assert.Equal(2, model.Columns.Count);
            Assert.Equal(2, model.Values.Count);
            Assert.Equal("23", model.Values[0]);
            Assert.Equal("name", model.Columns[1]);
            Assert.Equal("'Test'", model.Values[1]);

            model.ValueAdd("id", "56");
            Assert.Equal(2, model.Columns.Count);
            Assert.Equal(2, model.Values.Count);
            Assert.Equal("56", model.Values[0]);
            Assert.Equal("name", model.Columns[1]);
            Assert.Equal("'Test'", model.Values[1]);

            model.ValueRemove("name");
            Assert.Equal(1, model.Columns.Count);
            Assert.Equal(1, model.Values.Count);
            Assert.Equal("56", model.Values[0]);
        }

        [Theory]
        [InlineData("public", "table", "\"public\".\"table\"")]
        [InlineData("", "table", "\"table\"")]
        public void Query_FullName(string schema, string table, string fullname)
        {
            var model = new Builder.SelectModel(schema, table);
            Assert.Equal(fullname, model.FullName);
        }

        [Fact]
        public void Select_Test()
        {
            var model = new Models.SampleModel();
            var select = Builder.SelectModel.Create(model);
            Assert.NotNull(select);
            Assert.NotNull(select.Columns);
            Assert.Equal(5, select.Columns.Count);
            Assert.Equal("id", select.Columns[0]);
            Assert.Equal("name", select.Columns[1]);
            Assert.Equal("age", select.Columns[2]);
            Assert.Equal("is_valid", select.Columns[3]);
            Assert.Equal("created", select.Columns[4]);

            Assert.Equal("\"public\".\"sample\"", select.FullName);

            var query = "SELECT \"id\", \"name\", \"age\", \"is_valid\", \"created\" FROM \"public\".\"sample\";";
            Assert.Equal(query, select.Build());
        }
    }
}