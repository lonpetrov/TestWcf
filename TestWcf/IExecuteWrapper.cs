using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace TestWcf
{
    public interface IExecuteWrapper
    {
        int Execute(IDbConnection cnn, string sql, object param);
    }
}
