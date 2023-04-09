using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Lab2.XML
{
    public static class XMLRedactor
    {
        public static void Create<T>(IEnumerable<T> collection, string collectionName)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            string entityName = GetEntityName(collectionName);

            using (XmlWriter xmlWriter = XmlWriter.Create($"XML\\{collectionName}.xml", xmlWriterSettings))
            {
                xmlWriter.WriteStartElement(collectionName);

                foreach (T item in collection)
                {
                    xmlWriter.WriteStartElement(entityName);
                    foreach (var p in item.GetType().GetProperties())
                    {
                        xmlWriter.WriteElementString(p.Name, p.GetValue(item).ToString());
                    }
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }
        }
        public static void OutputWithLinq(string collectionName)
        {
            string entityName = GetEntityName(collectionName);

            XDocument document = XDocument.Load($"XML\\{collectionName}.xml");

            Console.WriteLine($"Collection name: {collectionName}");

            foreach (XElement element in document.Root.Elements(entityName))
            {
                foreach (XElement child in element.Elements())
                {
                    Console.Write($"{child.Name}: {child.Value}; ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        public static void OutputWithXmlDocument(string collectionName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load($"XML\\{collectionName}.xml");

            Console.WriteLine($"Collection name: {collectionName}");
            foreach(XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                foreach(XmlElement child in node.ChildNodes)
                {
                    Console.Write($"{child.Name}: {child.InnerText}; ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        private static string GetEntityName(string collectionName)
        {
            return collectionName == "Addresses" ? "Address" :
                collectionName.Substring(0, collectionName.Length - 1);
        }
    }
}
