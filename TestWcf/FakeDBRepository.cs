using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;
using System.Xml.Serialization;
using TestWcf.Utils;

namespace TestWcf
{
    public class FakeDBRepository : IDBRepository
    {
        string dataXmlFileName;

        public FakeDBRepository()
        {
            dataXmlFileName = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "data.xml");

            if (!File.Exists(dataXmlFileName))
            {
                // создать файл
                File.Create(dataXmlFileName);
            }
        }

        public IEnumerable<Cheque> GetLastCheques(int count)
        {
            // получить чеки из App_Data

            if (new FileInfo(dataXmlFileName).Length == 0)
            {
                return new List<Cheque>();
            }

            var doc = XDocument.Load(dataXmlFileName);

            var cheques = doc.Descendants("Cheque")
                .Select(node => new Cheque()
                {
                    Id = Guid.Parse(node.Element("Id").Value),
                    Number = node.Element("Number").Value,
                    Summ = decimal.Parse(node.Element("Summ").Value),
                    Discount = decimal.Parse(node.Element("Discount").Value),
                    Articles = node.Element("Articles")?.Value?.Split(';')
                }).ToList();

            cheques = cheques.Skip(Math.Max(0, cheques.Count() - count)).ToList();

            return cheques;
        }

        public void SaveCheck(Cheque cheque)
        {
            // сохранить чек в App_Data

            var formatter = new XmlSerializer(typeof(List<Cheque>));

            if (new FileInfo(dataXmlFileName).Length == 0)
            {
                using (FileStream fs = new FileStream(dataXmlFileName, FileMode.Open))
                {
                    formatter.Serialize(fs, new List<Cheque>());
                }
            }

            List<Cheque> cheques = new List<Cheque>();

            using (FileStream fs = new FileStream(dataXmlFileName, FileMode.OpenOrCreate))
            {
                cheques = (List<Cheque>)formatter.Deserialize(fs);
            }

            cheques.Add(cheque);

            using (FileStream fs = new FileStream(dataXmlFileName, FileMode.Open))
            {
                formatter.Serialize(fs, cheques);
            }
        }
    }
}