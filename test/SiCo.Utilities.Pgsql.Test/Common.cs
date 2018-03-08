namespace SiCo.Utilities.Pgsql.Test
{
    using System;
    using Generics;
    using Microsoft.Extensions.Configuration;

    public class Common
    {
        public Common()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            this.ConnectionString = config.GetConnectionString("TestConnection");

            // db
            this.QueryDrop = "DROP SCHEMA IF EXISTS public CASCADE; CREATE SCHEMA public; ALTER SCHEMA public OWNER TO admin;";
            this.QueryCreate = ResourcesExtensions.ConcatFile(typeof(Common), "SiCo.Utilities.Pgsql.Test.Resources.db_raw.sql");
            this.QueryClean = @"
    TRUNCATE public.enum CASCADE;
    TRUNCATE public.log CASCADE;
    TRUNCATE public.sample CASCADE";

            // log
            this.InsertLog = @"INSERT INTO log (membership_id, path, section, action, status, message, created, triggered_by) VALUES
('3004fbb1-da84-426a-a669-0cbc111dcc97', 'Log.Sample', 'Manual', 0, 0, 'Add sample log 1', '2016-04-06 20:39:33.66683', NULL),
('3004fbb1-da84-426a-a669-0cbc111dcc97', 'Log.Sample', 'Manual', 1, 1, 'Add sample log 2', '2016-04-06 21:20:33.66683', NULL),
('3004fbb1-da84-426a-a669-0cbc111dcc97', 'Log.Sample', 'Manual', 2, 2, 'Add sample log 3', '2016-04-06 22:41:33.66683', NULL);";
        }

        public string ConnectionString { get; set; }

        public string InsertLog { get; set; }

        public string QueryClean { get; set; }

        public string QueryCreate { get; set; }

        public string QueryDrop { get; set; }
    }
}