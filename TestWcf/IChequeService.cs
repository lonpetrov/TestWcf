//-----------------------------------------------------------------------
// <copyright file="IChequeService.cs" company="Manzana">
//     CheckService
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcf
{
    using System.Collections.Generic;
    using System.ServiceModel;

    /// <summary>
    /// Service interface
    /// </summary>
    [ServiceContract]
    public interface IChequeService
    {
        /// <summary>
        /// Method for getting the last added cheques.
        /// </summary>
        /// <param name="count">Number of cheques</param>
        /// <returns>List of cheques</returns>
        [OperationContract]
        IEnumerable<Cheque> GetLastCheques(int count);

        /// <summary>
        /// Method for passing cheque.
        /// </summary>
        /// <param name="cheque">Cheque object</param>
        [OperationContract]
        void PassCheque(Cheque cheque);
    }
}
