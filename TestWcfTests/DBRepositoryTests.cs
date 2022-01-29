
namespace TestWcfTests
{
    using System;
    using NUnit.Framework;
    using Moq;
    using TestWcf;
    using System.Collections.Generic;
    using Moq.Dapper;
    using System.Linq;
    using System.Data;
    using Dapper;

    [TestFixture]
    class DBRepositoryTests
    {
        [Test]
        public void GetLastCheques_GetsCountofCheques_ReturnLastCheques()
        {
            //Arrange
            IEnumerable<SqlCheque> fakeQueryResults = new List<SqlCheque>
            {
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
            };

            var connectionString = "connection string";
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(m => m
                .Query<SqlCheque>(It.IsAny<string>(), null, null, true, null, null))
                .Returns(fakeQueryResults);


            var repo = new DBRepository(
                connectionString, (conStr) =>
                {
                    mockConnection.Object.ConnectionString = conStr;
                    return mockConnection.Object;
                });

            //Act
            var actualCheques = repo.GetLastCheques(3);

            //Assert
            Assert.That(actualCheques.Count(), Is.EqualTo(3));
            Assert.That(actualCheques.AsList()[0].Articles, Is.TypeOf<string[]>());
            Assert.That(actualCheques.AsList()[0].Articles.Count(), Is.EqualTo(3));
            
        }
    }
}
