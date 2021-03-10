using System.Collections.Generic;

namespace TestWcf
{
    interface IDBRepository
    {
        void SaveCheck(Cheque cheque);

        IEnumerable<Cheque> GetLastCheques(int count);
    }
}
