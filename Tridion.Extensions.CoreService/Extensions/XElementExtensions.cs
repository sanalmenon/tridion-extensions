using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
namespace Tridion.Extensions.CoreService.Extensions
{
    public static class XElementExtensions
    {
        public static XmlDocument ToXmlDocument(this XElement xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XElement ToXElement(this XmlElement element)
        {
            return XElement.Load(element.CreateNavigator().ReadSubtree());
        }

        public static XmlElement ToXmlElement(this XElement element)
        {
            var doc = new XmlDocument();
            return doc.ReadNode(element.CreateReader()) as XmlElement;
        }

        public static IEnumerable<string> GetIds(this XElement element)
        {
            return element.Descendants().Select(e => e.Attribute("ID").Value);
        }
    }
}