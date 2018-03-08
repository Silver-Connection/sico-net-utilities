using System;
using System.Collections.Generic;
using System.Text;

namespace SiCo.Utilities.Pgsql.Models.JobConfig
{
    /// <summary>
    /// Default output format for Jobs
    /// </summary>
    public static class FormatCli
    {
        /// <summary>
        /// Key Value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static string KeyValue(string key, object val)
        {
            return string.Format("{0,-25} : {1}", key, val?.ToString()) + Environment.NewLine;
        }

        /// <summary>
        /// Head line 1
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string H1(string msg)
        {
            return Environment.NewLine + $"#### { msg }" + Environment.NewLine;
        }

        /// <summary>
        /// Head line 2
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string H2(string msg)
        {
            return Environment.NewLine + $"## { msg }" + Environment.NewLine;
        }

        /// <summary>
        /// Head line 3
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string H3(string msg)
        {
            return Environment.NewLine + $"# { msg }" + Environment.NewLine;
        }
    }
}
