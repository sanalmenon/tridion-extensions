using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Tridion.ContentManager;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;
using Tridion.ContentManager.Templating;

namespace Tridion.Extensions.ContentManager
{
    public static class SchemaExtensions
    {
        public static bool ContainsField(this Schema schema, string fieldName)
        {
            XNamespace tcm = "http://www.tridion.com/ContentManager/5.0";
            XNamespace xlink = "http://www.w3.org/1999/xlink";

            return
                new ItemFields(schema).Any(f => string.Equals(f.Name, fieldName, System.StringComparison.OrdinalIgnoreCase))
                ||
                XElement.Load(schema.Xsd.CreateNavigator().ReadSubtree())
                    .Descendants(tcm + "EmbeddedSchema")
                    .Select(n => n.Attribute(xlink + "href").Value)
                    .Any(schemaUri => (schema.Session.GetObject(schemaUri) as Schema).ContainsField(fieldName));
        }

        public static List<TcmUri> GetLinkedComponentTemplates(this Schema schema)
        {
            UsingItemsFilter filter = new UsingItemsFilter(schema.Session)
            {
                ItemTypes = new List<ItemType> { ItemType.ComponentTemplate }
            };

            return (from XmlNode ctNode in schema.GetListUsingItems(filter)
                    select new TcmUri(ctNode.Attributes["ID"].Value)).ToList();
        }

        public static bool ContainsMetadataField(this Schema schema, string fieldname)
        {
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

         
            schema.Load(new LoadFlags());

           
            var nodes =
                XElement.Load(schema.Xsd.CreateNavigator().ReadSubtree())
                                    .Descendants(xsd + "element")
                                    .Where(elem => elem.Attribute("name").Value.Equals("Metadata"))
                                            .Descendants(xsd + "element")
                                            .Select(e => e.Attribute("name").Value);


            return nodes.Select(value => value.Equals(fieldname)).FirstOrDefault();
        }
    }
}
