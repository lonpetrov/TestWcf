//-----------------------------------------------------------------------
// <copyright file="IDBRepository.cs" company="Manzana">
//     CheckService
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcf
{
    using System.Collections.Generic;

    /// <summary>
    /// Repository Interface
    /// </summary>
    internal interface IDBRepository
    {
        /// <summary>
        /// Method for saving a cheque.
        /// </summary>
        /// <param name="cheque">Cheque object</param>
        void SaveCheque(Cheque cheque);

        /// <summary>
        /// Method for getting the last added cheques.
        /// </summary>
        /// <param name="count">Number of cheques</param>
        /// <returns>List of cheques</returns>
        IEnumerable<Cheque> GetLastCheques(int count);
    }
}
