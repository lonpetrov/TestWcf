//-----------------------------------------------------------------------
// <copyright file="IExecuteWrapper.cs" company="Manzana">
//     CheckService
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcf
{
    using System.Data;

    /// <summary>
    /// IExecuteWrapper interface
    /// </summary>
    public interface IExecuteWrapper
    {
        /// <summary>
        /// Dapper query method <see cref="Execute"/> class.
        /// </summary>
        /// <param name="cnn">Connection string</param>
        /// <param name="sql">Connection factory</param>
        /// <param name="param">Execute Wrapper</param>
        /// <returns>Rows affected</returns>
        int Execute(IDbConnection cnn, string sql, object param);
    }
}
