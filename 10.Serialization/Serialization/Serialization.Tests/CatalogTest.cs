using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;

namespace Serialization.Tests
{
    public class CatalogTest
    {
        [Test]
        public void Test()
        {
            Catalog catalog = null;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "Serialization.Tests.books.xml";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Catalog));
                    catalog = serializer.Deserialize(reader) as Catalog;
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception occured during deserialization");
            }

            try
            {
                var outputPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\booksSerialized.xml";
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, catalog.Namespace);

                using (var writer = XmlWriter.Create(outputPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Catalog));
                    serializer.Serialize(writer, catalog, namespaces);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception occured during serialization");
            }
        }
    }
}
