using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TestWcf
{
    /// <summary>
    /// Интерфейс сервиса
    /// </summary>
    [ServiceContract]
    public interface IChequeService
    {
        /// <summary>
        /// Метод для получения списка последних добавленных чеков
        /// </summary>
        /// <param name="count">Число чеков</param>
        /// <returns>Список чеков</returns>
        [OperationContract]
        IEnumerable<Cheque> GetLastCheques(int count);

        /// <summary>
        /// Метод для отправки чека
        /// </summary>
        /// <param name="cheque">Объект чека</param>
        [OperationContract]
        void PassCheque(Cheque cheque);
    }
}
