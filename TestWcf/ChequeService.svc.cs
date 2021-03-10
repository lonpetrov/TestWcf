using System;
using System.Collections.Generic;
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
            if (repository == null)
            {
                repository = new FakeDBRepository();
            }
        }

        public IEnumerable<Cheque> GetLastChecks(int count)
        {
            var lastCheques = repository.GetLastCheques(count);

            return lastCheques;
        }

        public void ReceiveCheck(Cheque cheque)
        {
            repository.SaveCheck(cheque);
        }

        public string GetCheck(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

    }
}
