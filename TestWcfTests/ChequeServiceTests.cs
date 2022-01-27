namespace TestWcfTests
{
    using System;
    using NUnit.Framework;
    using Moq;
    using TestWcf;
    using System.Collections.Generic;

    [TestFixture]
    class ChequeServiceTests
    {
        [Test]
        public void CheqqueServicesGetLastCheques_GreaterThanZeroCount_ReturnListOfChecquesWithCount3()
        {
            //Arrange
            var mockRepository = new Mock<IDBRepository>();
            mockRepository.Setup(mock => mock.GetLastCheques(3)).Returns(
                new List<Cheque> {
                    new Cheque{Number="1"},
                    new Cheque{Number="2"},
                    new Cheque{Number="3"},
                });
            

            var chequeService = new ChequeService(mockRepository.Object);

            //Act
            var lastCheques = (IList<Cheque>)chequeService.GetLastCheques(3);
                
            //Assert
            Assert.That(lastCheques.Count, Is.EqualTo(3));
            Assert.That(lastCheques[0].Number, Is.EqualTo("1"));
            Assert.That(lastCheques[1].Number, Is.EqualTo("2"));
            Assert.That(lastCheques[2].Number, Is.EqualTo("3"));

        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void CheqqueServicesGetLastCheques_LessOrEqualsZeroCount_ReturnEmptyListOfChecques(int count)
        {
            //Arrange
            var mockRepository = new Mock<IDBRepository>();
            mockRepository.Setup(mock => mock.GetLastCheques(count)).Returns(
                new List<Cheque>());

            var chequeService = new ChequeService(mockRepository.Object);

            //Act
            var lastCheques = (IList<Cheque>)chequeService.GetLastCheques(count);

            //Assert
            Assert.That(lastCheques, Is.Empty);
        }

        [Test]
        public void CheqqueServicesPassCheque_GetsCheque_ChequeSavedInRepo()
        {
            //Arrange
            var cheque = new Cheque();
            var mockRepository = new Mock<IDBRepository>();
            mockRepository.Setup(mock => mock.SaveCheque(cheque);

            var chequeService = new ChequeService(mockRepository.Object);

            //Act
            var lastCheques = (IList<Cheque>)chequeService.GetLastCheques(count);

            //Assert
            Assert.That(lastCheques, Is.Empty);
        }
    }
}