using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;

namespace Tridion.Extensions.ContentManager
{
    public static class PublicationTargetExtensions
    {
        public static bool IsSiteEditEnabled(this PublicationTarget target)
        {
            try
            {
                if (target == null)
                    return true;
                ApplicationData appData = target.LoadApplicationData("SiteEdit");
                string data = Encoding.UTF8.GetString(appData.Data);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(data);

                XmlNamespaceManager nsManager = new XmlNamespaceManager(new NameTable());
                nsManager.AddNamespace("se", "http://www.sdltridion.com/2011/SiteEdit");

                XmlNode seNode = xmlDoc.SelectSingleNode("/se:configuration/se:PublicationTarget/se:EnableSiteEdit",
                                 nsManager);
                if (seNode != null && seNode.InnerText.Equals("true"))
                    return true;
                return false;
            }
            catch (Exception)
            {
                // Quick and dirty fix
                return false;
            }
        }
    }
}
