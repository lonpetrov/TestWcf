using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TestWcf
{
    public class DBRepository : IDBRepository
    {
        string connectionString = null;

        public DBRepository(string conn)
        {
            connectionString = conn;
        }

        public IEnumerable<Cheque> GetLastCheques(int count)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var cheques = db.Query<Cheque>("SELECT * FROM Cheques").ToList();

                return cheques.Skip(Math.Max(0, cheques.Count() - count)).ToList();
            }
        }

        public void SaveCheck(Cheque cheque)
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
    }
}