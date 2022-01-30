//-----------------------------------------------------------------------
// <copyright file="DBRepositoryTests.cs" company="Manzana">
//     CheckService
// </copyright>
// <summary>This is Test class</summary>
//-----------------------------------------------------------------------
namespace TestWcfTests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Moq;
    using Moq.Dapper;
    using NUnit.Framework;
    using TestWcf;

    /// <summary>
    /// DBRepository Tests.
    /// </summary>
    [TestFixture]
    public class DBRepositoryTests
    {
        /// <summary>
        /// GetLastCheques GetsCountofCheques ReturnLastCheques.
        /// </summary>
        [Test]
        public void GetLastCheques_GetsCountofCheques_ReturnLastCheques()
        {
            ////Arrange
            IEnumerable<SqlCheque> fakeQueryResults = new List<SqlCheque>
            {
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
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
                connectionString, 
                (conStr) =>
                {
                    mockConnection.Object.ConnectionString = conStr;
                    return mockConnection.Object;
                });

            ////Act
            var actualCheques = repo.GetLastCheques(3);

            ////Assert
            Assert.That(actualCheques.Count(), Is.EqualTo(3));
            Assert.That(actualCheques.AsList()[0].Articles, Is.TypeOf<string[]>());
            Assert.That(actualCheques.AsList()[0].Articles.Count(), Is.EqualTo(3));
        }

        /// <summary>
        /// GetLastCheques GetsZeroOrLessCount ReturnEmptyList.
        /// </summary>
        /// <param name="count">Case count.</param>
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetLastCheques_GetsZeroOrLessCount_ReturnEmptyList(int count)
        {
            ////Arrange
            IEnumerable<SqlCheque> fakeQueryResults = new List<SqlCheque>
            {
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
                    Discount = 2.0M,
                    Summ = 2.0M,
                    Articles = "article1;article2;article3"
                },
                new SqlCheque()
                {
                    Id = new Guid(),
                    Number = "some",
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
                connectionString, 
                (conStr) =>
                {
                    mockConnection.Object.ConnectionString = conStr;
                    return mockConnection.Object;
                });

            ////Act
            var actualCheques = repo.GetLastCheques(count);

            ////Assert
            Assert.That(actualCheques, Is.Empty);
        }

        /// <summary>
        /// GetLastCheques GetsNullsFromDB DoesNotThrowExeption.
        /// </summary>
        [Test]
        public void GetLastCheques_GetsNullsFromDB_DoesNotThrowExeption()
        {
            ////Arrange
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
                connectionString, 
                (conStr) =>
                {
                    mockConnection.Object.ConnectionString = conStr;
                    return mockConnection.Object;
                });
            TestDelegate whenNullsCameFromDb;

            ////Act
            whenNullsCameFromDb = () => repo.GetLastCheques(3);
            var lastCheques = repo.GetLastCheques(3);
            ////Assert
            Assert.DoesNotThrow(whenNullsCameFromDb);
            Assert.That(lastCheques, Has.Count.EqualTo(3));
            ////Assert.Throws<NullReferenceException>(whenNullsCameFromDb);
        }

        /// <summary>
        /// SaveCheque GetsChequeObject ChequeAddedToDB.
        /// </summary>
        [Test]
        public void SaveCheque_GetsChequeObject_ChequeAddedToDB()
        {
            ////Arrange
            var cheque = new Cheque()
            {
                Id = new Guid(),
                Number = string.Empty,
                Discount = 2.0M,
                Summ = 2.0M,
                Articles = new[] { "article1", "article2", "article3" },
            };

            var connectionString = "connection string";
            var mockExecuteObject = Mock.Of<IExecuteWrapper>(
                m => m.Execute(
                    It.IsAny<IDbConnection>(),
                    It.IsAny<string>(), 
                    It.IsAny<DynamicParameters>()) == 1);

            var mockExecute = Mock.Get(mockExecuteObject);

            var mockConnection = new Mock<IDbConnection>();

            var repo = new DBRepository(
                connectionString, 
                (conStr) =>
                {
                    mockConnection.Object.ConnectionString = conStr;
                    return mockConnection.Object;
                },
                mockExecuteObject);

            ////Act
            repo.SaveCheque(cheque);

            ////Assert
            mockExecute.Verify(
                m => m.Execute(
                It.IsAny<IDbConnection>(),
                It.IsAny<string>(), 
                It.IsAny<DynamicParameters>()),
                Times.Once);
        }

        /// <summary>
        /// SaveCheque GetsNullObject ChequeNotAddedToDB.
        /// </summary>
        [Test]
        public void SaveCheque_GetsNullObject_ChequeNotAddedToDB()
        {
            ////Arrange
            Cheque cheque = null;

            var connectionString = "connection string";
            var mockExecuteObject = Mock.Of<IExecuteWrapper>(
                m => m.Execute(
                    It.IsAny<IDbConnection>(),
                    It.IsAny<string>(),
                    It.IsAny<DynamicParameters>()) == 1);

            var mockExecute = Mock.Get(mockExecuteObject);

            var mockConnection = new Mock<IDbConnection>();

            var repo = new DBRepository(
                connectionString,
                (conStr) =>
                {
                    mockConnection.Object.ConnectionString = conStr;
                    return mockConnection.Object;
                },
                mockExecuteObject);

            ////Act
            repo.SaveCheque(cheque);

            ////Assert
            mockExecute.Verify(
                m => m.Execute(
                It.IsAny<IDbConnection>(),
                It.IsAny<string>(),
                It.IsAny<DynamicParameters>()),
                Times.Never);
        }

        /// <summary>
        /// SaveCheque GetsChequeWithNulls ThrowsArgumentNullException.
        /// </summary>
        [Test]
        public void SaveCheque_GetsChequeWithNulls_ThrowsArgumentNullException()
        {
            ////Arrange
            var cheque = new Cheque()
            {
                Id = null,
                Number = null,
                Discount = null,
                Summ = null,
                Articles = null
            };

            var connectionString = "connection string";
            var mockExecuteObject = Mock.Of<IExecuteWrapper>(
                m => m.Execute(
                    It.IsAny<IDbConnection>(),
                    It.IsAny<string>(),
                    It.IsAny<DynamicParameters>()) == 1);

            var mockExecute = Mock.Get(mockExecuteObject);

            var mockConnection = new Mock<IDbConnection>();

            var repo = new DBRepository(
                connectionString,
                (conStr) =>
                {
                    mockConnection.Object.ConnectionString = conStr;
                    return mockConnection.Object;
                },
                mockExecuteObject);
            TestDelegate whenValuesAreNulls;

            ////Act
            whenValuesAreNulls = () => repo.SaveCheque(cheque);

            ////Assert
            mockExecute.Verify(
                m => m.Execute(
                It.IsAny<IDbConnection>(),
                It.IsAny<string>(),
                It.IsAny<DynamicParameters>()),
                Times.Never);
            Assert.Throws<ArgumentNullException>(whenValuesAreNulls);
            ////Assert.DoesNotThrow(whenValuesAreNulls);
            ////Чтобы исправить следует добавить блок catch в 
            ////DBRepository
        }
    }
}
