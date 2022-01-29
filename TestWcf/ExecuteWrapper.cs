using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;

namespace TestWcf
{
    public class ExecuteWrapper : IExecuteWrapper
    {
        public int Execute(IDbConnection cnn, string sql, object param)
        {
            return cnn.Execute(sql, param);
        }
    }
}