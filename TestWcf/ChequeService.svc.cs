using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TestWcf
{
    public class CheckService : IChequeService
    {
        IDBRepository repository;

        public CheckService()
        {
            repository = new FakeDBRepository();

            //var connectionString = ConfigurationManager.ConnectionStrings["SomeDBconnectionString"].ConnectionString;
            //repository = new DBRepository(connectionString);
        }

        public IEnumerable<Cheque> GetLastCheques(int pack_size)
        {
            var lastCheques = repository.GetLastCheques(pack_size);

            return lastCheques;
        }

        public void ReceiveCheck(Cheque cheque)
        {
            repository.SaveCheck(cheque);
        }
    }
}
