using System;
using System.Linq;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace Tridion.Extensions.ContentManager
{
    public static class PageExtensions
    {
        public static Publication GetPublication(this Page page)
        {
            return (Publication)page.ContextRepository;
            
        }
    
        public static StructureGroup GetParentStructureGroup(this Page page)
        {
            return (StructureGroup) page.OrganizationalItem;
        }

        /// <summary>
        /// resolves pagetitle, 
        /// 1. 'Title' field of the first componentpresenstation.
        /// 2. 'PageTitle' field of the page metadata
        /// 3. Tridion page Title {Page.Title}
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Page Title</returns>
        public static string GetPageTitle(this Page page)
        {   
           var componentTitle = page.ComponentPresentations.Take(1).Select(cp => cp.Component.Fields().Text("Title")).FirstOrDefault();
           var PageMetadata = page.MetaFields().Text("PageTitle");
           if(!string.IsNullOrEmpty(componentTitle))
           {
               return componentTitle;
           }
           if (PageMetadata != string.Empty)
           {
               return PageMetadata;
           }

           return page.Title;
        }
        
    }
}



