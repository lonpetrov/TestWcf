//-----------------------------------------------------------------------
// <copyright file="FakeDBRepository.cs" company="Manzana">
//     CheckService.
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcf
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web.Hosting;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Fake repository.
    /// </summary>
    public class FakeDBRepository : IDBRepository
    {
        /// <summary>
        /// Path to data file.
        /// </summary>
        private string dataXmlFileName = null;

        /// <summary>
        /// Base Directory
        /// </summary>
        private string baseDir = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDBRepository"/> class.
        /// </summary>
        public FakeDBRepository()
        {
            if (HostingEnvironment.IsHosted)
            {
                this.baseDir = HostingEnvironment.ApplicationPhysicalPath;
            }
            else
            {
                this.baseDir = Path.Combine(
                                Directory.GetParent(
                                AppDomain.CurrentDomain.BaseDirectory)
                    .Parent.Parent
                    .Parent.Parent
                    .FullName, 
                    "TestWcf");
            }

            this.dataXmlFileName = Path.Combine(
                this.baseDir, "App_Data", "data.xml");

            if (!File.Exists(this.dataXmlFileName))
            {
                // Create file
                File.Create(this.dataXmlFileName);
            }
        }

        /// <summary>
        /// Method for getting the last added cheques.
        /// </summary>
        /// <param name="count">Number of cheques</param>
        /// <returns>List of cheques</returns>
        public IEnumerable<Cheque> GetLastCheques(int count)
        {
            // get from App_Data
            if (new FileInfo(this.dataXmlFileName).Length == 0)
            {
                return new List<Cheque>();
            }

            var doc = XDocument.Load(this.dataXmlFileName);

            var cheques = doc.Descendants("Cheque")
                .Select(node => new Cheque()
                {
                    Id = Guid.Parse(node.Element("Id").Value),
                    Number = node.Element("Number")?.Value,
                    Summ = decimal.Parse(node.Element("Summ").Value),
                    Discount = decimal.Parse(node.Element("Discount").Value),
                    Articles = node.Element("Articles")?.Value?.Split(';')
                }).ToList();

            cheques = cheques.Skip(Math.Max(0, cheques.Count() - count)).ToList();

            return cheques;
        }

        /// <summary>
        /// Method for saving a cheque.
        /// </summary>
        /// <param name="cheque">Cheque object</param>
        public void SaveCheque(Cheque cheque)
        {
            // save to App_Data
            var formatter = new XmlSerializer(typeof(List<Cheque>));

            if (new FileInfo(this.dataXmlFileName).Length == 0)
            {
                using (FileStream fs = new FileStream(this.dataXmlFileName, FileMode.Open))
                {
                    formatter.Serialize(fs, new List<Cheque>());
                }
            }

            var cheques = new List<Cheque>();

            using (FileStream fs = new FileStream(this.dataXmlFileName, FileMode.OpenOrCreate))
            {
                cheques = formatter.Deserialize(fs) as List<Cheque>;
            }

            cheques.Add(cheque);

            using (FileStream fs = new FileStream(this.dataXmlFileName, FileMode.Open))
            {
                formatter.Serialize(fs, cheques);
            }
        }
    }
}