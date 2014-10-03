using System;
using System.Text.RegularExpressions;

using Tridion.ContentManager.CoreService.Client;

namespace Tridion.Extensions.CoreService.Utils
{
    /// <summary>
    /// Creates TcmUri class from a string. Makes it easy to retrieve TcmUri properties like PublicationId, ItemId, ItemTypeId and Version.
    /// Usage: var PubliationId = new TcmUri("tcm:45-3456-64").PublicationId;
    /// </summary>
    public class TcmUri
    {
        public const string Null = "tcm:0-0-0";
        public int ItemId { get; set; }
        private int _publicationId;
        public int PublicationId
        { 
            get { return  _publicationId;}
            set { _publicationId = value;}
        }
        public int ItemTypeId { get; set; }
        public int Version { get; set; }
        public ItemType ItemType
        {
            get { return (ItemType)ItemTypeId; }
        }

        public TcmUri(string uri)
        {
            Regex re = new Regex(@"tcm:(\d+)-(\d+)-?(\d*)-?v?(\d*)");
            Match m = re.Match(uri);
            if (m.Success)
            {
                _publicationId = Convert.ToInt32(m.Groups[1].Value);
                              
                ItemId = Convert.ToInt32(m.Groups[2].Value);
                if (m.Groups.Count > 3 && !string.IsNullOrEmpty(m.Groups[3].Value))
                {
                    ItemTypeId = Convert.ToInt32(m.Groups[3].Value);
                }
                else
                {
                    ItemTypeId = (int)ItemType.Component;
                }
                if (m.Groups.Count > 4 && !string.IsNullOrEmpty(m.Groups[4].Value))
                {
                    Version = Convert.ToInt32(m.Groups[4].Value);
                }
                else
                {
                    Version = 0;
                }

                
            }
            //add logging if fails, outputting the uri parameter
        }

        public TcmUri(int publicationId, int itemId, int itemTypeId, int version)
        {
            PublicationId = publicationId;
            ItemId = itemId;
            ItemTypeId = itemTypeId;
            Version = version;
        }

        public override string ToString()
        {
            return IsComponentUri() ?
                string.Format("tcm:{0}-{1}", PublicationId, ItemId) : string.Format("tcm:{0}-{1}-{2}", PublicationId, ItemId, ItemTypeId);
        }

        private bool IsComponentUri()
        {
            return ItemType == ItemType.Component;
        }
    }


}