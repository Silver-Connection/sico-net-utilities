namespace SiCo.Utilities.Pgsql
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.ExceptionServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Run a Query
    /// </summary>
    public static class Query
    {
        #region Void

        /// <summary>
        /// Query with no return
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="connectionString">Database connection string</param>
        /// <returns>Boolean</returns>
        public static bool Void(string query, string connectionString)
        {
            if (Generics.StringExtensions.IsEmpty(query) || Generics.StringExtensions.IsEmpty(connectionString))
            {
                return false;
            }

            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    try
                    {
                        connection.Open();
                        var transaction = connection.BeginTransaction();
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (AggregateException e)
                    {
                        ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Query with no return
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Boolean</returns>
        public static async Task<bool> VoidAsync(string query, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Generics.StringExtensions.IsEmpty(query) || Generics.StringExtensions.IsEmpty(connectionString))
            {
                return false;
            }

            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    try
                    {
                        await connection.OpenAsync(cancellationToken);
                        var transaction = connection.BeginTransaction();
                        command.Transaction = transaction;
                        var t = await command.ExecuteNonQueryAsync(cancellationToken);
                        transaction.Commit();
                    }
                    catch (AggregateException e)
                    {
                        ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return true;
        }

        #endregion Void

        #region Generics

        /// <summary>
        /// Runs a query and returns a boolean value
        /// </summary>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>boolean</returns>
        public static bool Boolean(string query, string connectionString)
        {
            return (bool)Helper(query, connectionString, Transformers.ConvertToBoolean);
        }

        /// <summary>
        /// Runs a query and returns a boolean value
        /// </summary>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>boolean</returns>
        public static async Task<bool> BooleanAsync(string query, string connectionString, CancellationToken? cancellationToken)
        {
            var result = await HelperAsync(query, connectionString, Transformers.ConvertToBooleanAsync, cancellationToken.Value);
            return (bool)result;
        }

        /// <summary>
        /// Runs a query and returns a integer value
        /// </summary>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>integer</returns>
        public static int Integer(string query, string connectionString)
        {
            return (int)Helper(query, connectionString, Transformers.ConvertToInteger);
        }

        /// <summary>
        /// Run a insert query
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>Id if has one</returns>
        public static int Insert<TModel>(TModel model, string connectionString)
            where TModel : new()
        {
            var insert = Builder.InsertModel.Create(model);

            if (insert.HasId)
            {
                return (int)Helper(insert.Build(), connectionString, Transformers.ConvertToInteger);
            }
            else
            {
                var run = Void(insert.Build(), connectionString);
                return 0;
            }
        }

        /// <summary>
        /// Run a insert query
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Id if has one</returns>
        public static async Task<int> InsertAsync<TModel>(TModel model, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
            where TModel : new()
        {
            var insert = Builder.InsertModel.Create(model);

            if (insert.HasId)
            {
                var result = (int) await HelperAsync(insert.Build(), connectionString, Transformers.ConvertToIntegerAsync, cancellationToken);
                return result;
            }
            else
            {
                var run = await VoidAsync(insert.Build(), connectionString);
                return 0;
            }
        }

        /// <summary>
        /// Runs a query and returns a integer value
        /// </summary>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>integer</returns>
        public static async Task<int> IntegerAsync(string query, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await HelperAsync(query, connectionString, Transformers.ConvertToIntegerAsync, cancellationToken);
            return (int)result;
        }

        /// <summary>
        /// Runs a query and returns a given model
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>Model</returns>
        public static TModel Model<TModel>(string query, string connectionString)
         where TModel : new()
        {
            return (TModel)Helper(query, connectionString, Transformers.ConvertToModel<TModel>);
        }

     

        /// <summary>
        /// Runs a query and returns a given model
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Model</returns>
        public static async Task<TModel> ModelAsync<TModel>(string query, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
         where TModel : new()
        {
            var result = await HelperAsync(query, connectionString, Transformers.ConvertToModelAsync<TModel>, cancellationToken);
            return (TModel)result;
        }


        /// <summary>
        /// Runs a query and returns a string value
        /// </summary>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>string</returns>
        public static string String(string query, string connectionString)
        {
            return Helper(query, connectionString, Transformers.ConvertToString).ToString();
        }

        /// <summary>
        /// Runs a query and returns a string value
        /// </summary>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>string</returns>
        public static async Task<string> StringAsync(string query, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await HelperAsync(query, connectionString, Transformers.ConvertToStringAsync, cancellationToken);
            return result.ToString();
        }


        /// <summary>
        /// Runs a select query and returns a list of given models
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="model">Model</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>Model</returns>
        public static IEnumerable<TModel> Table<TModel>(TModel model, string connectionString)
         where TModel : new()
        {
            var select = Builder.SelectModel.Create(model);
            return Helper(select.Build(), connectionString, Transformers.ConvertToList<TModel>) as IEnumerable<TModel>;
        }

        /// <summary>
        /// Runs a select query and returns a list of given models
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="model">Model</param>
        /// <param name="queryAdd">Add command to end of query</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>Model</returns>
        public static IEnumerable<TModel> Table<TModel>(TModel model, string queryAdd, string connectionString)
         where TModel : new()
        {
            var select = Builder.SelectModel.Create(model);
            return Helper(select.Build().TrimEnd(';')+ " " + queryAdd.Trim(), connectionString, Transformers.ConvertToList<TModel>) as IEnumerable<TModel>;
        }


        /// <summary>
        /// Runs a select query and returns a list of given models
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="model">Model</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Model</returns>
        public static async Task<IEnumerable<TModel>> TableAsync<TModel>(TModel model, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
         where TModel : new()
        {
            var select = Builder.SelectModel.Create(model);
            var result = await HelperAsync(select.Build(), connectionString, Transformers.ConvertToListAsync<TModel>, cancellationToken) as IEnumerable<TModel>;
            return result;
        }

        /// <summary>
        /// Runs a query and returns a list of models
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>List of models</returns>
        public static IEnumerable<TModel> Table<TModel>(string query, string connectionString)
                    where TModel : new()
        {
            return Helper(query, connectionString, Transformers.ConvertToList<TModel>) as IEnumerable<TModel>;
        }

        /// <summary>
        /// Runs a query and returns a list of models
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of models</returns>
        public static async Task<IEnumerable<TModel>> TableAsync<TModel>(string query, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
                    where TModel : new()
        {
            return await HelperAsync(query, connectionString, Transformers.ConvertToListAsync<TModel>, cancellationToken) as IEnumerable<TModel>;
        }

        #endregion Generics

        /// <summary>
        /// Runs a query and returns value by using a converter
        /// </summary>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection to use</param>
        /// <param name="transformer">Transformer function</param>
        /// <returns>Object</returns>
        public static object Helper(
            string query,
            string connectionString,
            Func<Npgsql.NpgsqlCommand, object> transformer)
        {
            object result = null;

            if (Generics.StringExtensions.IsEmpty(query) || Generics.StringExtensions.IsEmpty(connectionString))
            {
                return result;
            }

            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    try
                    {
                        connection.Open();
                        var transaction = connection.BeginTransaction();
                        command.Transaction = transaction;

                        result = transformer(command);

                        transaction.Commit();
                    }
                    catch (AggregateException e)
                    {
                        ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Runs a query and returns value by using a converter
        /// </summary>
        /// <param name="query">Query to run</param>
        /// <param name="connectionString">Connection to use</param>
        /// <param name="transformer">Cancellation Token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object</returns>
        public static async Task<object> HelperAsync(
            string query,
            string connectionString,
            Func<Npgsql.NpgsqlCommand, CancellationToken, Task<object>> transformer,
            CancellationToken cancellationToken)
        {
            object result = null;

            if (Generics.StringExtensions.IsEmpty(query)
                || Generics.StringExtensions.IsEmpty(connectionString)
                || cancellationToken.IsCancellationRequested)
            {
                return result;
            }

            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    try
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            throw new OperationCanceledException(cancellationToken);
                        }

                        await connection.OpenAsync(cancellationToken);
                        var transaction = connection.BeginTransaction();
                        command.Transaction = transaction;

                        result = await transformer(command, cancellationToken);

                        transaction.Commit();
                    }
                    catch (AggregateException e)
                    {
                        ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return result;
        }
    }
}