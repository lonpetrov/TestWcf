//-----------------------------------------------------------------------
// <copyright file="DBRepository.cs" company="Manzana">
//     CheckService
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcf
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// BD Repository
    /// </summary>
    public class DBRepository : IDBRepository
    {
        /// <summary>
        /// Logger object.
        /// </summary>
        private static readonly log4net.ILog Log = 
            log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Connection string.
        /// </summary>
        private string connectionString = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBRepository"/> class.
        /// </summary>
        /// <param name="conn">Connection string</param>
        public DBRepository(string conn)
        {
            this.connectionString = conn;
        }

        /// <summary>
        /// Method for getting the last added cheques.
        /// </summary>
        /// <param name="count">Number of cheques</param>
        /// <returns>List of cheques</returns>
        public IEnumerable<Cheque> GetLastCheques(int count)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(this.connectionString))
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
                Log.Error("Не удалось получить список чеков" + Environment.NewLine + ex.Message);
                return new List<Cheque>();
            }
        }

        /// <summary>
        /// Method for saving a cheque.
        /// </summary>
        /// <param name="cheque">Cheque object</param>
        public void SaveCheque(Cheque cheque)
        {
            try 
            {
                var joinedArticles = string.Join(";", cheque.Articles);

                using (IDbConnection db = new SqlConnection(this.connectionString))
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
                Log.Error("Не удалось сохранить чек" + Environment.NewLine + ex.Message);
            }     
        }
    }
}