using System;
using System.Linq;
using System.Xml;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace Tridion.Extensions.ContentManager
{
    public static class ComponentExtender
    {
        /// <summary>
        /// Extends Component.Content with keywords meta information the same way as it is standard with component links.
        /// http://stackoverflow.com/a/12780807/35438
        /// </summary>
        public static XmlElement ExtendedContent(this Component c)
        {
            var componentXml = ExtendedSource(c);

            var ns = new XmlNamespaceManager(componentXml.OwnerDocument.NameTable);
            ns.AddNamespace("ns", c.Schema.NamespaceUri);

            return componentXml.SelectSingleNode("//ns:" + c.Schema.RootElementName, ns) as XmlElement;
        }

        /// <summary>
        /// Extends the whole Component source with keywords meta information.
        /// http://stackoverflow.com/a/12780807/35438
        /// </summary>
        public static XmlElement ExtendedSource(this Component c)
        {
            var componentXml = c.ToXml();

            var ns = new XmlNamespaceManager(componentXml.OwnerDocument.NameTable);
            ns.AddNamespace("ns", c.Schema.NamespaceUri);

            InflateKeywords(c.Fields(), (XmlElement)componentXml.SelectSingleNode("//ns:" + c.Schema.RootElementName, ns));

            c.MetaFields().IfNotNull(metaFields =>
                (componentXml.SelectSingleNode("//ns:Metadata", ns) as XmlElement).IfNotNull(metaXml =>
                    InflateKeywords(metaFields, metaXml)
                )
            );
            
            return componentXml;
        }

        static void InflateKeywords(ItemFields fields, XmlElement element)
        {
            var ns = new XmlNamespaceManager(element.OwnerDocument.NameTable);
            ns.AddNamespace("ns", element.NamespaceURI);
            
            fields
                .OfType<KeywordField>()
                .ToList()
                .ForEach(keywordField =>
                    element.SelectNodes("./ns:" + keywordField.Name, ns)
                        .OfType<XmlElement>()
                        .ToList()
                        .ForEach(kwelement =>
                    {
                        kwelement.SetAttribute("href", "http://www.w3.org/1999/xlink", keywordField.Values.First(v => v.Title.Equals(kwelement.InnerText)).Id);
                        kwelement.SetAttribute("type", "http://www.w3.org/1999/xlink", "simple");
                        kwelement.SetAttribute("title", "http://www.w3.org/1999/xlink", kwelement.InnerText);
                    })
                );
            
            fields
                .OfType<EmbeddedSchemaField>()
                .ToList()
                .ForEach(embedField =>
                    element.SelectNodes("./ns:" + embedField.Name, ns)
                        .OfType<XmlElement>()
                        .Select((embedElement, i) => new { Fields = embedField.Values[i], Element = embedElement })
                        .ToList()
                        .ForEach(a => InflateKeywords(a.Fields, a.Element))
                );
        }
    }
}