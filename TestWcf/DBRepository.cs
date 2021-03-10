using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TestWcf
{
    /// <summary>
    /// Репозиторий БД
    /// </summary>
    public class DBRepository : IDBRepository
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        string connectionString = null;

        /// <summary>
        /// Конструктор репозитория БД
        /// </summary>
        /// <param name="conn"></param>
        public DBRepository(string conn)
        {
            connectionString = conn;
        }

        /// <summary>
        /// Метод для получения списка последних добавленных чеков
        /// </summary>
        /// <param name="count">Число чеков</param>
        /// <returns>Список чеков</returns>
        public IEnumerable<Cheque> GetLastCheques(int count)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var cheques = db.Query<Cheque>("SELECT * FROM Cheques")
                        .Select(ch => new Cheque()
                        {
                            Id = ch.Id,
                            Number = ch.Number,
                            Discount = ch.Discount,
                            Summ = ch.Summ,
                            Articles = ch.Articles.First()?.Split(';')
                        })
                        .ToList();

                    return cheques.Skip(Math.Max(0, cheques.Count() - count)).ToList();
                }
            }
            catch (Exception ex)
            {
                // TODO: логгировать
                throw new Exception("Не удалось получить список чеков", ex);
            }
        }

        /// <summary>
        /// Метод для сохранения чека
        /// </summary>
        /// <param name="cheque">Объект чека</param>
        public void SaveCheque(Cheque cheque)
        {
            try 
            {
                var joinedArticles = string.Join(";", cheque.Articles);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var query = "INSERT INTO Cheques (Id, Number, Summ, Discount, Articles) VALUES(@Id, @Number, @Summ, @Discount, @Articles)";

                    var dp = new DynamicParameters();

                    dp.Add("@Id", cheque.Id);
                    dp.Add("@Number", cheque.Number);
                    dp.Add("@Summ", cheque.Id);
                    dp.Add("@Discount", cheque.Number);
                    dp.Add("@Articles", joinedArticles);

                    db.Execute(query, dp);
                }
            }
            catch (Exception ex)
            {
                // TODO: логгировать
                throw new Exception("Не удалось сохранить чек", ex);
            }     
        }
    }
}