using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace Tridion.Extensions.ContentManager
{
    public static partial class ComponentExtensions
  {
    public static Publication GetPublication(this Component component)
    {
        return (Publication)component.ContextRepository;
    }

      public static void ExtendEmbeddedComponents(this Component component)
      {
          
      }

      public static ItemFields FieldsOrDefault(this Component component)
      {
          return component.Fields();
      }
  }
}