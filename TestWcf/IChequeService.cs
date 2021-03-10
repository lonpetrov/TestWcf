using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TestWcf
{
    [ServiceContract]
    public interface IChequeService
    {
        [OperationContract]
        IEnumerable<Cheque> GetLastCheques(int count);

        [OperationContract]
        void ReceiveCheck(Cheque cheque);
    }
}
