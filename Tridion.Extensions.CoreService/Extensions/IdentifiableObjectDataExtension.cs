using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tridion.ContentManager.CoreService.Client;

namespace Tridion.Extensions.CoreService.Extensions
{
    internal static class IdentifiableObjectDataExtension
    {
        public static int? GetVersion(this IdentifiableObjectData objectData)
        {   
            if (!objectData.GetType().Name.Equals("FolderData") &&
                !objectData.GetType().Name.Equals("StructureGroupData") &&
                !objectData.GetType().Name.Equals("PublicationData"))
                return ((FullVersionInfo) objectData.VersionInfo).Version;
            
            return null;
        }

        public static LinkToUserData GetCheckedOutUser(this IdentifiableObjectData objectData)
        {
            if (!objectData.GetType().Name.Equals("FolderData") &&
                !objectData.GetType().Name.Equals("StructureGroupData") &&
                !objectData.GetType().Name.Equals("PublicationData"))
                return ((FullVersionInfo)objectData.VersionInfo).CheckOutUser;
           
            return null;
        }

        public static bool? Update(this IdentifiableObjectData objectData)
        {
            try
            {
                Wrapper.Instance.Update(objectData, new ReadOptions());
                return true;
            }
            catch (Exception e)
            {
                throw e; 
            }
        }

        //Todo implement 
        //public static void Publish(this IdentifiableObjectData objectData)
        //{
        //    var pubInst = new PublishInstructionData()
        //    {
                
        //        ResolveInstruction = new ResolveInstructionData() 
        //            { IncludeChildPublications = false, 
        //                Purpose = ResolvePurpose.Publish, 
        //                StructureResolveOption = StructureResolveOption.OnlyItems 
        //            },
        //        RenderInstruction = new RenderInstructionData(),
        //        MaximumNumberOfRenderFailures = 100,
        //        RollbackOnFailure = false
        //    };
        //   Wrapper.Instance.Publish(new string[] { objectData.Id.ToString() }, pubInst, new string[] { "tcm:0-36-65537" }, PublishPriority.Low, new ReadOptions());

        //}
    }
}
