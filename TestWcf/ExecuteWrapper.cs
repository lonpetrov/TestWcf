//-----------------------------------------------------------------------
// <copyright file="ExecuteWrapper.cs" company="Manzana">
//     CheckService
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcf
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using Dapper;

    /// <summary>
    /// Execute Wrapper
    /// </summary>
    public class ExecuteWrapper : IExecuteWrapper
    {
        /// <summary>
        /// Dapper query method <see cref="Execute"/> class.
        /// </summary>
        /// <param name="cnn">Connection string</param>
        /// <param name="sql">Connection factory</param>
        /// <param name="param">Execute Wrapper</param>
        /// <returns>Rows affected</returns>
        public int Execute(IDbConnection cnn, string sql, object param)
        {
            return cnn.Execute(sql, param);
        }
    }
}