using System.Collections.Generic;
using Tridion.ContentManager.ContentManagement.Fields;
using System;
using Tridion.ContentManager.ContentManagement;

namespace Tridion.Extensions.ContentManager
{
    public static class KeywordExtensions
    {
        public static Keyword GetRootParentKeyword(this Keyword keyword)
        {
            return !keyword.IsRoot ? GetRootParentKeyword(keyword.ParentKeywords[0]) : keyword;
        }

        public static IList<Keyword> GetKeywordTrail(Keyword keyword)
        {
            List<Keyword> keywords = new List<Keyword>();
            Keyword kw = keyword;

            while(!kw.IsRoot)
            {
                keywords.Add(kw);
                kw = kw.ParentKeywords[0];
            }

            return keywords;
        }

        public static ItemFields MetaFields(this Keyword keyword)
        {
            return
                null != keyword.Metadata
                    ? new ItemFields(keyword.Metadata, keyword.MetadataSchema)
                    : keyword.MetadataSchema.IfNotNull(meta => new ItemFields(meta));
        } 
    }

}
