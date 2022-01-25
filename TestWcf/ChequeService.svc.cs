//-----------------------------------------------------------------------
// <copyright file="ChequeService.svc.cs" company="Manzana">
//     CheckService.
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcf
{
    using System.Collections.Generic;

    /// <summary>
    /// ChequeService class
    /// </summary>
    public class ChequeService : IChequeService
    {
        /// <summary>
        /// Logger object.
        /// </summary>
        private static readonly log4net.ILog Log = 
            log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Field for Repository.
        /// </summary>
        private IDBRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChequeService"/> class.
        /// </summary>
        public ChequeService()
        {
            // TODO: реализовать внедрение с помощью DI контейнера
            this.repository = new FakeDBRepository();
            ////var connectionString = ConfigurationManager.ConnectionStrings["SomeDBconnectionString"].ConnectionString;
            ////repository = new DBRepository(connectionString);
        }

        /// <summary>
        /// Gets list of last cheques.
        /// </summary>
        /// <param name="pack_size">Size of last cheques</param>
        /// <returns>List of cheques</returns>
        public IEnumerable<Cheque> GetLastCheques(int pack_size)
        {
            if (pack_size <= 0)
            {
                Log.Error($"Запрошенное количество чеков \"{pack_size}\" недопустимо");
                return new List<Cheque>();
            }

            var lastCheques = this.repository.GetLastCheques(pack_size);
            return lastCheques;
        }

        /// <summary>
        /// Save cheques to repo.
        /// </summary>
        /// <param name="cheque">Cheque for passing</param>
        public void PassCheque(Cheque cheque)
        {
            if (cheque != null)
            {
                this.repository.SaveCheque(cheque);
            }
            else
            {
                Log.Error($"Объект чека не определён \"cheque = null\"");
            }
        }
    }
}
