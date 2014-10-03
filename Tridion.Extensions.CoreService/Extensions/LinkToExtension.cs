using Tridion.ContentManager.CoreService.Client;
using Tridion.Extensions.CoreService.Utils;

namespace Tridion.Extensions.CoreService.Extensions
{
    public static class LinkToExtension
    {
        public static UserData GetUser(this LinkToUserData linkTo)
        {
            return linkTo.IdRef == TcmUri.Null ? null : linkTo.IdRef.ToTcmUri().GetItem<UserData>();
        }
        public static ComponentData GetComponent(this LinkToComponentData linkTo)
        {
            return linkTo.IdRef == TcmUri.Null ? null : linkTo.IdRef.ToTcmUri().GetItem<ComponentData>();
        }
        public static FolderData GetFolder(this LinkToFolderData linkTo)
        {
            return linkTo.IdRef == TcmUri.Null ? null : linkTo.IdRef.ToTcmUri().GetItem<FolderData>();
        }
       
        
    }
}
