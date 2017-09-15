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
                return ((FullVersionInfo)objectData.VersionInfo).Version;

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

        public static void Publish(this IdentifiableObjectData objectData, string[] purpose, PublishPriority prio = PublishPriority.Low, DateTime? StartAt = null)
        {
            var pubInst = new PublishInstructionData()
            {
                ResolveInstruction = new ResolveInstructionData()
                {
                    IncludeChildPublications = false,
                    Purpose = ResolvePurpose.Publish,
                    StructureResolveOption = StructureResolveOption.OnlyItems
                },
                RenderInstruction = new RenderInstructionData(),
                MaximumNumberOfRenderFailures = 100,
                RollbackOnFailure = false
            };
            if (StartAt.HasValue)
                pubInst.StartAt = StartAt;

            Wrapper.Instance.Publish(new string[] { objectData.Id.ToString() }, pubInst, purpose, PublishPriority.Low, new ReadOptions());
        }

        public static void Publish(this IdentifiableObjectData objectData, string purpose, PublishPriority prio = PublishPriority.Low, DateTime? StartAt = null)
        {
            objectData.Publish(new[] { purpose }, prio, StartAt);
        }

        public static void UnPublish(this IdentifiableObjectData objectData, string[] purpose, PublishPriority prio = PublishPriority.Low, DateTime? StartAt = null)
        {
            var unPubInst = new UnPublishInstructionData()
            {
                ResolveInstruction = new ResolveInstructionData()
                {
                    IncludeChildPublications = false,
                    Purpose = ResolvePurpose.UnPublish,
                    StructureResolveOption = StructureResolveOption.OnlyItems
                },

                RollbackOnFailure = false
            };

            if (StartAt.HasValue)
                unPubInst.StartAt = StartAt;

            Wrapper.Instance.UnPublish(new string[] { objectData.Id.ToString() }, unPubInst, purpose, PublishPriority.Low, new ReadOptions());
        }

        public static void UnPublish(this IdentifiableObjectData objectData, string purpose, PublishPriority prio = PublishPriority.Low, DateTime? StartAt = null)
        {
            objectData.UnPublish(new[] { purpose }, prio, StartAt);
        }
    }
}