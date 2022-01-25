//-----------------------------------------------------------------------
// <copyright file="FakeRepositoryTests.cs" company="Manzana">
//     CheckService
// </copyright>
// <summary>This is Test class</summary>
//-----------------------------------------------------------------------
namespace TestWcfTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using NUnit.Framework;
    using TestWcf;

    /// <summary>
    /// FakeRepository Tests.
    /// </summary>
    [TestFixture]
    public class FakeRepositoryTests
    {
        /// <summary>
        /// LastCheques GetsCountofCheques ReturnLastCheques.
        /// </summary>
        [Test]
        public void GetLastCheques_GetsCountofCheques_ReturnLastCheques()
        {
            ////Arrange
            var cheque = new Cheque()
            {
                Id = new Guid(),
                Number = "some",
                Discount = 200,
                Summ = 2000,
                Articles = new[] { "article1", "article2", "article3" }
            };

            var dirOfXml = Path.Combine(
                Directory
                    .GetParent(
                    AppDomain.CurrentDomain.BaseDirectory)
                    .Parent.Parent.Parent.Parent
                    .FullName,
                    "TestWcf", 
                    "App_Data",
                    "data.xml");

            var formatter = new XmlSerializer(typeof(List<Cheque>));

            var cheques = new List<Cheque>();

            using (FileStream fs = new FileStream(dirOfXml, FileMode.OpenOrCreate))
            {
                cheques = formatter.Deserialize(fs) as List<Cheque>;
            }

            cheques.Add(cheque);
            cheques.Add(cheque);
            cheques.Add(cheque);

            using (FileStream fs = new FileStream(dirOfXml, FileMode.Open))
            {
                formatter.Serialize(fs, cheques);
            }
            ////Arrange
            var repo = new FakeDBRepository();

            ////Act
            var actualCheques = repo.GetLastCheques(3);

            ////Assert
            Assert.That(actualCheques, Has.Count.EqualTo(3));
        }

        /// <summary>
        /// GetLastCheques GetsZeroOrLessCount ReturnEmptyList.
        /// </summary>
        /// <param name="count">Count case.</param>
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetLastCheques_GetsZeroOrLessCount_ReturnEmptyList(int count)
        {
            ////Arrange
            var repo = new FakeDBRepository();

            ////Act
            var actualCheques = repo.GetLastCheques(count);

            ////Assert
            Assert.That(actualCheques, Is.Empty);
        }

        /// <summary>
        /// SaveCheque GetsChequeObject ChequeAddedToDB.
        /// </summary>
        [Test]
        public void SaveCheque_GetsChequeObject_ChequeAddedToDB()
        {
            ////Arrange
            var xmlFile = Path.Combine(
                Directory
                    .GetParent(AppDomain.CurrentDomain.BaseDirectory)
                    .Parent.Parent.Parent.Parent
                    .FullName,
                    "TestWcf",
                    "App_Data",
                    "data.xml");

            var cheque = new Cheque()
            {
                Id = new Guid("10000000-0000-0000-0000-000000000000"),
                Number = "some",
                Discount = 200,
                Summ = 2000,
                Articles = new[] { "article1", "article2", "article3" }
            };
            var repo = new FakeDBRepository();

            ////Act
            repo.SaveCheque(cheque);

            ////Assert
            var doc = XDocument.Load(xmlFile);
            var cheques = doc.Descendants("Cheque")
                .Select(node => new Cheque()
                {
                    Id = Guid.Parse(node.Element("Id").Value),
                    Number = node.Element("Number")?.Value,
                    Summ = decimal.Parse(node.Element("Summ").Value),
                    Discount = decimal.Parse(node.Element("Discount").Value),
                    Articles = node.Element("Articles")?.Value?.Split(';')
                }).ToList();

            var retrivedCheque = cheques.Skip(Math.Max(0, cheques.Count() - 1)).ToList();

            ////Assert
            Assert.That(retrivedCheque, Has.Count.EqualTo(1));
            Assert.That(
                retrivedCheque[0].Id, 
                Is.EqualTo(new Guid("10000000-0000-0000-0000-000000000000")));
        }
    }
}
