namespace SiCo.Utilities.Pgsql
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.ExceptionServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Run stored functions
    /// </summary>
    public static class Function
    {
        #region Void

        /// <summary>
        /// Runs a query and returns void
        /// </summary>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>boolean</returns>
        public static bool Void(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString)
        {
            if (Generics.StringExtensions.IsEmpty(name) || Generics.StringExtensions.IsEmpty(connectionString))
            {
                return false;
            }

            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(name, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (param != null)
                    {
                        foreach (var item in param)
                        {
                            command.Parameters.Add(item);
                        }
                    }
                    else
                    {
                        command.Parameters.Clear();
                    }

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
        /// Runs a query and returns void
        /// </summary>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>boolean</returns>
        public static async Task<bool> VoidAsync(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Generics.StringExtensions.IsEmpty(name)
                || Generics.StringExtensions.IsEmpty(connectionString)
                || cancellationToken.IsCancellationRequested)
            {
                return false;
            }

            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(name, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (param != null)
                    {
                        foreach (var item in param)
                        {
                            command.Parameters.Add(item);
                        }
                    }
                    else
                    {
                        command.Parameters.Clear();
                    }

                    try
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            throw new OperationCanceledException(cancellationToken);
                        }

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
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>boolean</returns>
        public static bool Boolean(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString)
        {
            return (bool)Helper(name, param, connectionString, Transformers.ConvertToBoolean);
        }

        /// <summary>
        /// Runs a query and returns a boolean value
        /// </summary>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>boolean</returns>
        public static async Task<bool> BooleanAsync(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await HelperAsync(name, param, connectionString, Transformers.ConvertToBooleanAsync, cancellationToken);
            return (bool)result;
        }

        /// <summary>
        /// Runs a query and returns a integer value
        /// </summary>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>integer</returns>
        public static int Integer(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString)
        {
            return (int)Helper(name, param, connectionString, Transformers.ConvertToInteger);
        }

        /// <summary>
        /// Runs a query and returns a integer value
        /// </summary>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>integer</returns>
        public static async Task<int> IntegerAsync(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await HelperAsync(name, param, connectionString, Transformers.ConvertToIntegerAsync, cancellationToken);
            return (int)result;
        }

        /// <summary>
        /// Runs a query and returns a given model
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>Model</returns>
        public static TModel Model<TModel>(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString)
         where TModel : new()
        {
            return (TModel)Helper(name, param, connectionString, Transformers.ConvertToModel<TModel>);
        }

        /// <summary>
        /// Runs a query and returns a given model
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Model</returns>
        public static async Task<TModel> ModelAsync<TModel>(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
         where TModel : new()
        {
            var result = await HelperAsync(name, param, connectionString, Transformers.ConvertToModelAsync<TModel>, cancellationToken);
            return (TModel)result;
        }

        /// <summary>
        /// Runs a query and returns a string value
        /// </summary>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>string</returns>
        public static string String(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString)
        {
            return Helper(name, param, connectionString, Transformers.ConvertToString).ToString();
        }

        /// <summary>
        /// Runs a query and returns a string value
        /// </summary>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>string</returns>
        public static async Task<string> StringAsync(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await HelperAsync(name, param, connectionString, Transformers.ConvertToStringAsync, cancellationToken);
            return result.ToString();
        }

        /// <summary>
        /// Runs a query and returns a list of models
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <returns>List of models</returns>
        public static IEnumerable<TModel> Table<TModel>(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString)
                    where TModel : new()
        {
            return Helper(name, param, connectionString, Transformers.ConvertToList<TModel>) as IEnumerable<TModel>;
        }

        /// <summary>
        /// Runs a query and returns a list of models
        /// </summary>
        /// <typeparam name="TModel">Return model, needs to have constructor which accepts NpgsqlDataReader.</typeparam>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of models</returns>
        public static async Task<IEnumerable<TModel>> TableAsync<TModel>(string name, IEnumerable<Npgsql.NpgsqlParameter> param, string connectionString, CancellationToken cancellationToken = default(CancellationToken))
                    where TModel : new()
        {
            return await HelperAsync(name, param, connectionString, Transformers.ConvertToListAsync<TModel>, cancellationToken) as IEnumerable<TModel>;
        }

        #endregion Generics

        /// <summary>
        /// Runs a stored function and returns value by using a converter
        /// </summary>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="transformer">Transformer function</param>
        /// <returns>Object</returns>
        public static object Helper(
            string name,
            IEnumerable<Npgsql.NpgsqlParameter> param,
            string connectionString,
            Func<Npgsql.NpgsqlCommand, object> transformer)
        {
            object result = null;

            if (Generics.StringExtensions.IsEmpty(name) || Generics.StringExtensions.IsEmpty(connectionString))
            {
                return result;
            }

            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(name, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (param != null)
                    {
                        foreach (var item in param)
                        {
                            command.Parameters.Add(item);
                        }
                    }
                    else
                    {
                        command.Parameters.Clear();
                    }

                    try
                    {
                        connection.Open();

                        result = transformer(command);
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
        /// Runs a stored function and returns value by using a converter
        /// </summary>
        /// <param name="name">Stored function name</param>
        /// <param name="param">List of parameter to use. Set null for no parameters.</param>
        /// <param name="connectionString">Connection sting to use</param>
        /// <param name="transformer">Transformer function</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Object</returns>
        public static async Task<object> HelperAsync(
            string name,
            IEnumerable<Npgsql.NpgsqlParameter> param,
            string connectionString,
            Func<Npgsql.NpgsqlCommand, CancellationToken, Task<object>> transformer,
            CancellationToken cancellationToken)
        {
            object result = null;

            if (Generics.StringExtensions.IsEmpty(name) 
                || Generics.StringExtensions.IsEmpty(connectionString)
                || cancellationToken.IsCancellationRequested)
            {
                return result;
            }

            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(name, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (param != null)
                    {
                        foreach (var item in param)
                        {
                            command.Parameters.Add(item);
                        }
                    }
                    else
                    {
                        command.Parameters.Clear();
                    }

                    try
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            throw new OperationCanceledException(cancellationToken);
                        }

                        await connection.OpenAsync(cancellationToken);

                        result = await transformer(command, cancellationToken);
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