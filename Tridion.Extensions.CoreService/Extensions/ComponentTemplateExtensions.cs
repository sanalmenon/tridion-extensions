using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;

namespace Tridion.Extensions.CoreService.Extensions
{
    public static class ComponentTemplateExtensions
    {
        public static string GetFieldAsString(this ComponentTemplateData componentData, string fieldName, ContentType ctype)
        {
            var element = XElement.Parse(GetContent(componentData, ctype));

            var e = element.Descendants().FirstOrDefault(x => x.Name.LocalName == fieldName);
            if (null != e)
                return e.Value;
            else
                return String.Empty;
        }

        public static string GetContent(this ComponentTemplateData componentTemplateData, ContentType ctype)
        {
            if (ctype == ContentType.Content) return componentTemplateData.Content;
            else return componentTemplateData.Metadata;
        }
    }
}
