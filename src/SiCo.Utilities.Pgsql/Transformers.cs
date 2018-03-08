namespace SiCo.Utilities.Pgsql
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.ExceptionServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Common Transformers
    /// </summary>
    public static class Transformers
    {
        /// <summary>
        /// Run query and convert return to boolean
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns></returns>
        public static object ConvertToBoolean(Npgsql.NpgsqlCommand command)
        {
            using (var dr = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
            {
                dr.Read();
                return dr.GetBoolean(0);
            }
        }

        /// <summary>
        /// Run query and convert return to boolean async version
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<object> ConvertToBooleanAsync(Npgsql.NpgsqlCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var dr = await command.ExecuteReaderAsync(System.Data.CommandBehavior.SingleResult, cancellationToken))
            {
                await dr.ReadAsync(cancellationToken);
                return dr.GetBoolean(0);
            }
        }

        /// <summary>
        /// Run query and convert return to int
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns></returns>
        public static object ConvertToInteger(Npgsql.NpgsqlCommand command)
        {
            using (var dr = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
            {
                dr.Read();
                return dr.GetInt32(0);
            }
        }

        /// <summary>
        /// Run query and convert return to int
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<object> ConvertToIntegerAsync(Npgsql.NpgsqlCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var dr = await command.ExecuteReaderAsync(System.Data.CommandBehavior.SingleResult))
            {
                await dr.ReadAsync(cancellationToken);
                return dr.GetInt32(0);
            }
        }

        /// <summary>
        /// Run query and transform return to List of given Models. Important: Model needs to have a constructor which takes a NpgsqlDataReader
        /// </summary>
        /// <typeparam name="TModel">Model with correct constructor</typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public static object ConvertToList<TModel>(Npgsql.NpgsqlCommand command)
                 where TModel : new()
        {
            using (var dr = command.ExecuteReader(System.Data.CommandBehavior.Default))
            {
                var list = new List<TModel>();
                while (dr.Read())
                {
                    list.Add(CreateModel<TModel>(dr));
                }

                return list;
            }
        }

        /// <summary>
        /// Run query and transform return to List of given Models. Important: Model needs to have a constructor which takes a NpgsqlDataReader
        /// </summary>
        /// <typeparam name="TModel">Model with correct constructor</typeparam>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<object> ConvertToListAsync<TModel>(Npgsql.NpgsqlCommand command, CancellationToken cancellationToken = default(CancellationToken))
         where TModel : new()
        {
            using (var dr = await command.ExecuteReaderAsync(System.Data.CommandBehavior.Default))
            {
                var list = new List<TModel>();
                while (await dr.ReadAsync(cancellationToken))
                {
                    list.Add(CreateModel<TModel>(dr));
                }

                return list;
            }
        }

        /// <summary>
        /// Run query and transform return to given Models. Important: Model needs to have a constructor which takes a NpgsqlDataReader
        /// </summary>
        /// <typeparam name="TModel">Model with correct constructor</typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public static object ConvertToModel<TModel>(Npgsql.NpgsqlCommand command)
                 where TModel : new()
        {
            using (var dr = command.ExecuteReader(System.Data.CommandBehavior.SingleRow))
            {
                dr.Read();
                return CreateModel<TModel>(dr);
            }
        }

        /// <summary>
        /// Run query and transform return to given Models. Important: Model needs to have a constructor which takes a NpgsqlDataReader
        /// </summary>
        /// <typeparam name="TModel">Model with correct constructor</typeparam>
        /// <param name="command">Command</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<object> ConvertToModelAsync<TModel>(Npgsql.NpgsqlCommand command, CancellationToken cancellationToken = default(CancellationToken))
         where TModel : new()
        {
            using (var dr = await command.ExecuteReaderAsync(System.Data.CommandBehavior.SingleRow))
            {
                await dr.ReadAsync(cancellationToken);
                return CreateModel<TModel>(dr);
            }
        }

        /// <summary>
        /// Run query and convert return to string
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns></returns>
        public static object ConvertToString(Npgsql.NpgsqlCommand command)
        {
            using (var dr = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
            {
                dr.Read();
                return dr.GetString(0);
            }
        }

        /// <summary>
        /// Run query and convert return to string
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<object> ConvertToStringAsync(Npgsql.NpgsqlCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var dr = await command.ExecuteReaderAsync(System.Data.CommandBehavior.SingleResult))
            {
                await dr.ReadAsync(cancellationToken);
                return dr.GetString(0);
            }
        }

        /// <summary>
        /// Create a model. Search for a constructor which accepts dbmodel
        /// </summary>
        /// <typeparam name="TOut">Output Model</typeparam>
        /// <param name="dbmodel">Object to pass to the model constructor</param>
        /// <returns>Display string</returns>
        public static TOut CreateModel<TOut>(object dbmodel)
            where TOut : new()
        {
            try
            {
                // Try to get constructor
                var c = typeof(TOut).GetConstructor(new Type[] { dbmodel.GetType() });

                if (c == null)
                {
                    throw new NotSupportedException("Could not find constructor for " + typeof(TOut).FullName);
                }

                // Create model
                TOut m = (TOut)c.Invoke(new object[] { dbmodel });

                return m;
            }
            catch (NotSupportedException e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
            }

            return new TOut();
        }
    }
}