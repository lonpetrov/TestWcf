using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWcf
{
    public class DBRepository : IChequeService
    {
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cheque> GetLastChecks(int count)
        {
            throw new NotImplementedException();
        }

        public void ReceiveCheck(Cheque cheque)
        {
            throw new NotImplementedException();
        }
    }
}