using System.Collections.Generic;

namespace TestWcf
{
    /// <summary>
    /// Интерфейс репозитория
    /// </summary>
    interface IDBRepository
    {
        /// <summary>
        /// Метод для сохранения чека
        /// </summary>
        /// <param name="cheque">Объект чека</param>
        void SaveCheque(Cheque cheque);

        /// <summary>
        /// Метод для получения списка последних добавленных чеков
        /// </summary>
        /// <param name="count">Число чеков</param>
        /// <returns>Список чеков</returns>
        IEnumerable<Cheque> GetLastCheques(int count);
    }
}
