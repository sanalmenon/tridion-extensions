using System;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace Tridion.Extensions.ContentManager
{
  public static partial class ComponentExtensions
  {
    public static ItemFields Fields(this Component component)
    {
      return component.Content.IfNotNull(content => new ItemFields(content, component.Schema)) ?? new ItemFields(component.Schema);

    }
    
    public static ItemFields MetaFields(this RepositoryLocalObject component)
    {
        return
            null != component.Metadata
                ? new ItemFields(component.Metadata, component.MetadataSchema)
                : component.MetadataSchema.IfNotNull(meta => new ItemFields(meta));
    }

  }
}