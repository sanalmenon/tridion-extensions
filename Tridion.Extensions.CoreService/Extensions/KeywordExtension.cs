using System.Xml.Linq;

using Tridion.ContentManager.CoreService.Client;

namespace Tridion.Extensions.CoreService.Extensions
{
    public static class KeywordExtension
    {
        public static XElement GetChildern(this KeywordData keyword, ListBaseColumns list = ListBaseColumns.Default)
        {
            ChildKeywordsFilterData keywordsDataFilter = new ChildKeywordsFilterData
            {
                BaseColumns = list,
            };
           return Wrapper.Instance.GetListXml(keyword.Id, keywordsDataFilter);  
        }


        public static XElement GetClassifiedItems(this KeywordData keyword, ItemType[] itemTypes, ListBaseColumns list = ListBaseColumns.Default)
        {
            ClassifiedItemsFilterData filter = new ClassifiedItemsFilterData
            {
                ItemTypes = itemTypes,
                BaseColumns = list
            };

            return Wrapper.Instance.GetListXml(keyword.Id, filter);
        }
      
     
        public static XElement GetClassifiedItems(this KeywordData keyword, ItemType itemType, ListBaseColumns list = ListBaseColumns.Default)
        {
            ItemType[] itemList = { itemType };
            return GetClassifiedItems(keyword, itemList, list);
        }
    }
}
