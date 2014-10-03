using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;

namespace Tridion.Extensions.CoreService.Extensions
{
    public static class CategoryExtension
    {
        public static XElement GetChildern(this CategoryData category, ListBaseColumns list = ListBaseColumns.Default)
        {
            KeywordsFilterData keywordsDataFilter = new KeywordsFilterData
            {
                BaseColumns = list,
                IsRoot = true
            };

            return Wrapper.Instance.GetListXml(category.Id, keywordsDataFilter);
        }

        
    }
}
