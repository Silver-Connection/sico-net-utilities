namespace SiCo.Utilities.Pgsql.Test
{
    using System;

    public class DatabaseFixture : IDisposable
    {
        private bool disposedValue = false;

        public DatabaseFixture()
        {
            this.Common = new Common();
            // drop db
            //Query.Void(this.Common.QueryDrop, this.Common.ConnectionString);

            // clean db
            //Query.Void(this.Common.QueryClean, this.Common.ConnectionString);

            // create db
            //Query.Void(this.Common.QueryCreate, this.Common.ConnectionString);

            Npgsql.NpgsqlConnection.MapEnumGlobally<Enums.Direction>();
        }

        public Common Common { get; set; }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                //Query.Void(this.Common.QueryDrop, this.Common.ConnectionString);

                disposedValue = true;
            }
        }
    }
}