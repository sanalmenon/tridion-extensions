using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tridion.ContentManager.CoreService.Client;

namespace Tridion.Extensions.CoreService.Extensions
{
    public static class PageExtensions
    {
        //TODO
        public static void AddComponentPresentation(this PageData page, ComponentPresentationData cp)
        {
            page.ComponentPresentations.Concat(new[] { cp });

        }
    }
}
