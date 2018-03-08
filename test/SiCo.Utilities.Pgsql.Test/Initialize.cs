//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SiCo.Utilities.Pgsql.Test
//{
//    public class Initialize : IDisposable
//    {
//        private bool disposedValue = false;

//        public Initialize()
//        {
//            this.Common = new Common();

//            // clean db
//            Query.Void(this.Common.QueryDrop, this.Common.ConnectionString);

//            // create db
//            Query.Void(this.Common.QueryCreate, this.Common.ConnectionString);

//            Npgsql.NpgsqlConnection.MapEnumGlobally<Enums.Direction>();
//        }

//        public Common Common { get; set; }

//        void IDisposable.Dispose()
//        {
//            Dispose(true);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!disposedValue)
//            {
//                Query.Void(this.Common.QueryDrop, this.Common.ConnectionString);

//                disposedValue = true;
//            }
//        }
//    }
//}
