using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;
using Tridion.Extensions.CoreService.Utils;

namespace Tridion.Extensions.CoreService.Extensions
{
    public static class FolderExtension
    {
        public static XElement GetListXml(this FolderData folder, ItemType[] itemTypes, bool recursive)
        {
            return folder.Id.ToTcmUri().GetListXml(itemTypes, recursive);
        }

        public static XElement GetListXml(this FolderData folder, ItemType itemType, bool recursive)
        {
            return folder.Id.ToTcmUri().GetListXml(itemType, recursive);
        }
    }
}
