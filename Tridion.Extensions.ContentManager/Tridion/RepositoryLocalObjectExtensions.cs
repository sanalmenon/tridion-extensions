using System.Collections.Generic;
using Tridion.ContentManager.ContentManagement;


namespace Tridion.Extensions.ContentManager
{
  public static class RepositoryLocalObjectExtensions
  {
    public static IEnumerable<OrganizationalItem> Parents(this RepositoryLocalObject page)
    {
      var sg = page.OrganizationalItem;

      while (null != sg.OrganizationalItem)
      {
        yield return sg;
        sg = sg.OrganizationalItem;
      }
    }
  }
}