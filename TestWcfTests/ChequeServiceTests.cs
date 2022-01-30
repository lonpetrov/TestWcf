//-----------------------------------------------------------------------
// <copyright file="ChequeServiceTests.cs" company="Manzana">
//     CheckService
// </copyright>
// <summary>This is Test class</summary>
//-----------------------------------------------------------------------
namespace TestWcfTests
{
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using TestWcf;

    /// <summary>
    /// ChequeServices Test.
    /// </summary>
    [TestFixture]
    public class ChequeServiceTests
    {
        /// <summary>
        /// CheqqueServicesGetLastCheques GreaterThanZeroCount ReturnListOfChecquesWithCount3.
        /// </summary>
        [Test]
        public void CheqqueServicesGetLastCheques_GreaterThanZeroCount_ReturnListOfChecquesWithCount3()
        {
            ////Arrange
            var mockRepository = Mock
                .Of<IDBRepository>(m => m
                .GetLastCheques(3) == new List<Cheque> 
                {
                  new Cheque { Number = "1" },
                  new Cheque { Number = "2" },
                  new Cheque { Number = "3" }, 
                });

            var chequeService = new ChequeService(mockRepository);

            ////Act
            var lastCheques = (IList<Cheque>)chequeService.GetLastCheques(3);
                
            ////Assert
            Assert.That(lastCheques.Count, Is.EqualTo(3));
            Assert.That(lastCheques[0].Number, Is.EqualTo("1"));
            Assert.That(lastCheques[1].Number, Is.EqualTo("2"));
            Assert.That(lastCheques[2].Number, Is.EqualTo("3"));
        }

        /// <summary>
        /// GetLastCheques LessOrEqualsZeroCount ReturnEmptyListOfChecques.
        /// </summary>
        /// <param name="count">Count case.</param>
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void CheqqueServicesGetLastCheques_LessOrEqualsZeroCount_ReturnEmptyListOfChecques(int count)
        {
            ////Arrange
            var mockRepository = new Mock<IDBRepository>();
            mockRepository.Setup(mock => mock.GetLastCheques(count)).Returns(
                new List<Cheque>());

            var chequeService = new ChequeService(mockRepository.Object);

            ////Act
            var lastCheques = (IList<Cheque>)chequeService.GetLastCheques(count);

            ////Assert
            Assert.That(lastCheques, Is.Empty);
        }

        /// <summary>
        /// PassCheque GetsCheque ChequeSavedInRepo.
        /// </summary>
        [Test]
        public void PassCheque_GetsCheque_ChequeSavedInRepo()
        {
            ////Arrange
            var mockRepository = new Mock<IDBRepository>();
            var chequeService = new ChequeService(mockRepository.Object);

            ////Act
            chequeService.PassCheque(new Cheque());

            ////Assert
            mockRepository.Verify(mock => mock.SaveCheque(It.IsAny<Cheque>()), Times.Once);
        }

        /// <summary>
        /// Method PassCheque GetsNull ChequeNotSaved.
        /// </summary>
        [Test]
        public void PassCheque_GetsNull_ChequeNotSaved()
        {
            ////Arrange
            var mockRepository = new Mock<IDBRepository>();
            var chequeService = new ChequeService(mockRepository.Object);

            ////Act
            chequeService.PassCheque(null);

            ////Assert
            mockRepository.Verify(mock => mock.SaveCheque(It.IsAny<Cheque>()), Times.Never);
        }
    }
}