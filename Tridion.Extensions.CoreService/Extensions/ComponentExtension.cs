using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;
using Tridion.Extensions.CoreService.Extensions;
using Tridion.Extensions.CoreService.Utils;

namespace Tridion.Extensions.CoreService.Extensions
{
    public static class ComponentExtension
    {
        public static int? GetVersion(this ComponentData component) 
        {
            IdentifiableObjectData objectData = component;
            return objectData.GetVersion(); 
        }

        public static LinkToUserData GetCheckedOutUser(this ComponentData component)
        {
            IdentifiableObjectData objectData = component; 
            return objectData.GetCheckedOutUser();
        }

        public static XElement GetFieldAsXElement(this ComponentData componentData,string fieldName,ContentType ctype)
        {
            var element = XElement.Parse(GetContent(componentData,ctype));
            return element.Descendants().FirstOrDefault(x => x.Name.LocalName == fieldName);
        }

        public static void SetFieldFromXElement(this ComponentData componentData, string fieldName, ContentType ctype, XElement content)
        {
            var element = XElement.Parse(componentData.GetContent(ctype));

            var elementToReplace = element.Descendants().FirstOrDefault(x => x.Name.LocalName == fieldName);
            if (null != elementToReplace)
            {
                elementToReplace.ReplaceWith(content);
            }
            componentData.SetContent(ctype,element.ToXmlElement().OuterXml);
        }

        public static string GetFieldAsString(this ComponentData componentData, string fieldName,ContentType ctype)
        {
            var element = XElement.Parse(GetContent(componentData,ctype));

            var e = element.Descendants().FirstOrDefault(x => x.Name.LocalName == fieldName);
            if (null != e)
                return e.Value;
            else
                return String.Empty;
        }

        public static void SetFieldFromString(this ComponentData componentData,string fieldName,ContentType ctype, string content)
        {
            var element = XElement.Parse(GetContent(componentData, ctype));
            var elementToReplace = element.Descendants().FirstOrDefault(x => x.Name.LocalName == fieldName);
            if (null != elementToReplace)
            {
                elementToReplace.SetValue(content);
            }
            componentData.SetContent(ctype,element.ToXmlElement().OuterXml);
        }

        /*public static void SetFieldFromComponentLink(this ComponentData componentData,string fieldName, ContentType ctype, string componentId)
        {
            SetFieldFromComponentLinks(componentData,fieldName,ctype,new List<string>{componentId});
        }
        public static void SetFieldFromComponentLinks(this ComponentData componentData, string fieldName, ContentType ctype, List<string> componentIds)
        {
            XElement element = XElement.Parse(componentData.GetContent(ctype));
            var e = element.Descendants().Where(x => x.Name.LocalName == fieldName);

            var links = new List<string>();

            foreach (var link in e)
            {
                var href = link.Attributes().FirstOrDefault(x => x.Name.LocalName == "href");
                if (null != href)
                    links.Add(href.Value);
            }
        }*/
       
        public static string GetFieldAsComponentLink(this ComponentData componentData, string fieldName,ContentType ctype, string content)
        {
            return GetFieldAsComponentLinks(componentData, fieldName, ctype)[0];
        }

        public static List<string> GetFieldAsComponentLinks(this ComponentData componentData, string fieldName,ContentType ctype)
        {
            XElement element = XElement.Parse(componentData.GetContent(ctype));
            var e = element.Descendants().Where(x => x.Name.LocalName == fieldName);

            var links = new List<string>();

            foreach (var link in e)
            {
                var href = link.Attributes().FirstOrDefault(x => x.Name.LocalName == "href");
                if(null != href)
                    links.Add(href.Value);
            }
            return links;
        }

        public static ComponentData GetFieldAsComponent(this ComponentData componentData, string fieldName,ContentType ctype)
        {
            return GetFieldAsComponents(componentData, fieldName, ctype)[0];
        }

        public static List<ComponentData> GetFieldAsComponents(this ComponentData componentData, string fieldName, ContentType ctype)
        {
            var links = GetFieldAsComponentLinks(componentData, fieldName, ctype);
            var components = new List<ComponentData>();
            if (links.Count == 0) return components;
            components.AddRange(links.Select(link => link.ToTcmUri().GetItem<ComponentData>()));
            return components;
        }

        public static string GetContent(this ComponentData componentData, ContentType ctype)
        {
            if (ctype == ContentType.Content) return componentData.Content;
            else return componentData.Metadata;
        }

        public static void SetContent(this ComponentData componentData, ContentType ctype, string content)
        {
            if (ctype == ContentType.Content) componentData.Content = content;
            else componentData.Metadata = content;
        }

        public static bool? Save(this ComponentData componentData)
        {
            //check if item exist; call update from identifalobjectdata otherwise Save
            return componentData.Update();
        }
        
        public static List<IdentifiableObjectData> GetAllComponentsBasedOnSchema(this ComponentData componentData,int resultLimit=-1)
        {
            
            List<IdentifiableObjectData> itemsToReturn = new List<IdentifiableObjectData>();

            var searchQuery = new SearchQueryData();

            searchQuery.BasedOnSchemas = new[] { new BasedOnSchemaData() { Schema = new LinkToSchemaData() { IdRef = componentData.Schema.IdRef } } };
            searchQuery.ItemTypes = new[]  { ItemType.Component };
            searchQuery.SearchInSubtree = true;
            searchQuery.BaseColumns = ListBaseColumns.IdAndTitle;
            
            if (resultLimit != -1) searchQuery.ResultLimit = resultLimit;

            var items = Wrapper.Instance.GetSearchResults(searchQuery);

            itemsToReturn.AddRange(items);
            
            return itemsToReturn;

        }
    
        /*
         * TODO: Finish this method!
        public static List<IdentifiableObjectData> GetUsesItems(this ComponentData componentData)
        {
            UsedItemsFilterData filter = new UsedItemsFilterData() { InRepository = { IdRef = string.Format("tcm:0-{0}-1", componentData.Id.ToTcmUri().PublicationId }, IncludeBlueprintParentItem = false, ItemTypes = new[] { ItemType.MultimediaType, ItemType.Component } };
        }
        */
    }
}