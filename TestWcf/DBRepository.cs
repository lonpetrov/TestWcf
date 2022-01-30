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
    using TestWcf.Exceptions;

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

        private Func<string, IDbConnection> connectionFactory;

        private IExecuteWrapper executeWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBRepository"/> class.
        /// </summary>
        /// <param name="conn">Connection string</param>
        public DBRepository(string conn)
        {
            this.connectionString = conn;
        }

        public DBRepository(string conn, Func<string, IDbConnection> dbConnFac, IExecuteWrapper exWrap=null)
        {
            this.connectionFactory = dbConnFac;
            this.connectionString = conn;
            if (exWrap == null)
            {
                this.executeWrapper = new ExecuteWrapper();
            }
            else
            {
                this.executeWrapper = exWrap;
            }
            
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
                if (count > 0)
                {
                    using (IDbConnection db = connectionFactory.Invoke(connectionString))
                    {
                        var cheques = db.Query<SqlCheque>("SELECT * FROM Cheques")
                            .Select(ch => new Cheque()
                            {
                                Id = ch.Id,
                                Number = ch.Number,
                                Discount = ch.Discount,
                                Summ = ch.Summ,
                                Articles = ch?.Articles?.Split(';')
                            })
                            .ToList();

                        return cheques.Skip(Math.Max(0, cheques.Count() - count)).ToList();
                    }
                }
                else
                {
                    throw new InvalidCountException(count);
                }
                
            }
            catch (InvalidCountException ex)
            {
                Log.Error("Не удалось получить список чеков" + Environment.NewLine + ex.Message +
                    Environment.NewLine + "Invalid cheque count equals: " + ex.Count);
                return new List<Cheque>();
            }
            catch
            {
                throw;
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
                if (cheque != null)
                {
                    var joinedArticles = string.Join(";", cheque.Articles);

                    using (IDbConnection db = connectionFactory.Invoke(connectionString))
                    {
                        var query = "INSERT INTO Cheques (Id, Number, Summ, Discount, Articles) VALUES(@Id, @Number, @Summ, @Discount, @Articles)";

                        var dp = new DynamicParameters();

                        dp.Add("@Id", cheque.Id);
                        dp.Add("@Number", cheque.Number);
                        dp.Add("@Summ", cheque.Id);
                        dp.Add("@Discount", cheque.Number);
                        dp.Add("@Articles", joinedArticles);

                        //db.Execute(query, dp);
                        executeWrapper.Execute(db, query, dp);
                    }
                }
                else
                {
                    throw new InvalidChequeException();
                }   
            }
            catch (InvalidChequeException ex)
            {
                Log.Error("Не удалось сохранить чек" + Environment.NewLine + ex.Message);
            }
            //Чтобы не выбрасывалось исключение следует раскоментировать данный код
            //catch (ArgumentNullException ex)
            //{
            //    Log.Error("Не удалось сохранить чек потому что значения одного или нескольких свойств равно NULL" + Environment.NewLine + ex.Message);
            //}
            catch
            {
                throw;
            }
        }
    }
}