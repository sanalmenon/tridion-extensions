using System;
using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;
using Tridion.Extensions.CoreService.Utils;

namespace Tridion.Extensions.CoreService.Extensions
{
    public static class TcmUriExtensions
    {
        public static TcmUri ToTcmUri(this string tcmUri)
        {
            return new TcmUri(tcmUri);
        }

        public static T GetItem<T>(this string webdav) where T : IdentifiableObjectData
        {
            if (!webdav.StartsWith("/")) webdav = string.Format("/{0}", webdav);
            if (!webdav.StartsWith("/webdav/")) webdav = string.Format("/webdav{0}", webdav);
            T result;
            try
            {
                result = ((T)Wrapper.Instance.Read(webdav, new ReadOptions()));
            }
            catch (Exception e)
            {
                result = default(T);
            }
            return result;
        }

        public static T GetItem<T>(this TcmUri tcmUri) where T : IdentifiableObjectData
        {
            T result;
            try
            {
                result = ((T)Wrapper.Instance.Read(tcmUri.ToString(), new ReadOptions()));
            }
            catch (Exception e)
            {
                result = default(T);
            }
            return result;
        }

        public static XElement GetListXml(this TcmUri tcmUri, ItemType[] itemTypes, bool recursive)
        {
            var filter = new OrganizationalItemItemsFilterData
            {
                ItemTypes = itemTypes,
                Recursive = recursive
            };
            return Wrapper.Instance.GetListXml(tcmUri.ToString(), filter);
        }

        public static XElement GetListXml(this TcmUri tcmUri, bool recursive)
        {
            ItemType[] list = { };
            return GetListXml(tcmUri, list, recursive);
        }

        public static XElement GetListXml(this TcmUri tcmUri, ItemType itemType, bool recursive)
        {
            ItemType[] list = { itemType };
            return GetListXml(tcmUri, list, recursive);
        }
    }
}