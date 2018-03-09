namespace SiCo.Utilities.Web.Charts
{
    using System;
    using System.Reflection;
    using System.Runtime.ExceptionServices;

    public static class Helper
    {
        /// <summary>
        /// Create a model. Search for a constructor which accepts string
        /// </summary>
        /// <typeparam name="TOut">Output Model</typeparam>
        /// <param name="dbmodel">Object to pass to the model constructor</param>
        /// <returns>Display string</returns>
        internal static TOut CreateModel<TOut>(object dbmodel)
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