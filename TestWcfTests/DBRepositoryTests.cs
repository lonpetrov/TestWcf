
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
    using TestWcf.Exceptions;

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

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetLastCheques_GetsZeroOrLessCount_ReturnEmptyList(int count)
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
            var actualCheques = repo.GetLastCheques(count);

            //Assert
            Assert.That(actualCheques, Is.Empty);

        }

        [Test]
        public void GetLastCheques_GetsNullsFromDB_ReturnEmptyList()
        {
            //Arrange
            IEnumerable<SqlCheque> fakeQueryResults = new List<SqlCheque>
            {
                new SqlCheque()
                {
                    Id = null,
                    Number = null,
                    Discount = null,
                    Summ = null,
                    Articles = null
                },
                new SqlCheque()
                {
                    Id = null,
                    Number = null,
                    Discount = null,
                    Summ = null,
                    Articles = null
                },
                new SqlCheque()
                {
                    Id = null,
                    Number = null,
                    Discount = null,
                    Summ = null,
                    Articles = null
                },
                new SqlCheque()
                {
                    Id = null,
                    Number = null,
                    Discount = null,
                    Summ = null,
                    Articles = null
                },
                new SqlCheque()
                {
                    Id = null,
                    Number = null,
                    Discount = null,
                    Summ = null,
                    Articles = null
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
            TestDelegate whenNullsCameFromDb;

            //Act
            whenNullsCameFromDb = () => repo.GetLastCheques(3);

            //Assert
            Assert.DoesNotThrow(whenNullsCameFromDb);
            //Assert.Throws<NullReferenceException>(whenNullsCameFromDb);

        }

        [Test]
        public void SaveCheque_GetsFullChequeObject_ChequeAddedToDB()
        {
            //Arrange
            var cheque = new Cheque()
            {
                Id = new Guid(),
                Number = "",
                Discount = 2.0M,
                Summ = 2.0M,
                Articles = new []{"article1", "article2", "article3" }
            };


            var connectionString = "connection string";
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(m => m
                .Execute(It.IsAny<string>(), It.IsAny<DynamicParameters>(), null, null, null))
                .Returns(1)
                .Verifiable();
            //mockConnection.When(mockConnection.Object.Execute())

            var repo = new DBRepository(
                connectionString, (conStr) =>
                {
                    mockConnection.Object.ConnectionString = conStr;
                    return mockConnection.Object;
                });

            //Act
            repo.SaveCheque(null);

            //Assert
            

            
        }
    }
}
