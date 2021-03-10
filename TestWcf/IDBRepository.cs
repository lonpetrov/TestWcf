using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWcf
{
    interface IDBRepository
    {
        void SaveCheck(Cheque cheque);

        IEnumerable<Cheque> GetLastCheques(int count);
    }
}
