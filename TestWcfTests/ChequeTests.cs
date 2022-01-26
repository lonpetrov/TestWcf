//-----------------------------------------------------------------------
// <copyright file="DBRepository.cs" company="Manzana">
//     CheckService
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcfTests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using TestWcf;

    [TestFixture]
    public class ChequeTests
    {

        [Test]
        public void Id_AssignGuid_AssignationSuccess()
        {
            //Arrange
            var cheque = new Cheque();
            var guid = new Guid("00000000-0000-0000-0000-000000000000");

            //Act
            cheque.Id = guid;

            //Assert
            Assert.AreEqual(new Guid("00000000-0000-0000-0000-000000000000"), cheque.Id);
        }

        [Test]
        public void Id_AssignNull_AssignationSuccess()
        {
            //Arrange
            var cheque = new Cheque();
            object testGuid = null;
            
            //Act
            cheque.Id = (Guid)testGuid;

            //Assert
            Assert.IsNull(cheque.Id);
        }

        [Test]
        public void Number_AssignString_AssignationSuccess()
        {
            //Arrange
            var cheque = new Cheque();
            var number = "test string";

            //Act
            cheque.Number = number;

            //Assert
            Assert.AreEqual("test string", cheque.Number);
        }

        [Test]
        public void Summ_AssignDecimal_AssignationSuccess()
        {
            //Arrange
            var cheque = new Cheque();
            var summ = 20.000M;

            //Act 
            cheque.Summ = summ;

            //Assert
            Assert.AreEqual(20.000M, cheque.Summ);
        }

        [Test]
        public void Summ_AssignNull_AssignationSuccess()
        {
            //Arrange
            var cheque = new Cheque();
            object summ = null;

            //Act 
            cheque.Summ = (decimal)summ;

            //Assert
            Assert.IsNull(cheque.Summ);
        }

        [Test]
        public void Discount_AssignDecimal_AssignationSuccess()
        {
            //Arrange
            var cheque = new Cheque();
            var discount = 20.000M;

            //Act 
            cheque.Discount = discount;

            //Assert
            Assert.AreEqual(20.000M, cheque.Discount);
        }

        [Test]
        public void Discount_AssignNull_AssignationSuccess()
        {
            //Arrange
            var cheque = new Cheque();
            object discount = null;

            //Act 
            cheque.Discount = (decimal)discount;

            //Assert
            Assert.IsNull(cheque.Discount);
        }

        [Test]
        public void Articles_AssignArray_AssignationSuccess()
        {
            //Arrange
            var cheque = new Cheque();
            string[] array = { "string1", "string2", "string3" };

            //Act
            cheque.Articles = array;

            //Assert
            Assert.That(cheque.Articles.Length == 3);
            Assert.AreEqual(new[] { "string1", "string2", "string3" }, cheque.Articles);

        }
    }
}