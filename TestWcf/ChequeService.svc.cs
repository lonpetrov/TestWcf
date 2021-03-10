using System.Collections.Generic;
using System.Configuration;

namespace TestWcf
{
    /// <summary>
    /// Сервис для добавления и получения чеков
    /// </summary>
    public class CheckService : IChequeService
    {
        /// <summary>
        /// Объект логгера
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Ссылка на репозиторий
        /// </summary>
        IDBRepository repository;

        public CheckService()
        {
            repository = new FakeDBRepository();

            //var connectionString = ConfigurationManager.ConnectionStrings["SomeDBconnectionString"].ConnectionString;
            //repository = new DBRepository(connectionString);
        }

        /// <summary>
        /// Метод для получения списка последних добавленных чеков
        /// </summary>
        /// <param name="count">Число чеков</param>
        /// <returns>Список чеков</returns>
        public IEnumerable<Cheque> GetLastCheques(int pack_size)
        {
            if (pack_size <= 0)
            {
                log.Error($"Запрошенное количество чеков \"{pack_size}\" недопустимо");
                return new List<Cheque>();
            }

            var lastCheques = repository.GetLastCheques(pack_size);

            return lastCheques;
        }

        /// <summary>
        /// Метод для отправки чека
        /// </summary>
        /// <param name="cheque">Объект чека</param>
        public void PassCheque(Cheque cheque)
        {
            if (cheque != null)
            {
                repository.SaveCheque(cheque);
            }
            else
            {
                log.Error($"Объект чека не определён \"cheque = null\"");
            }
        }
    }
}
