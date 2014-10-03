<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference Relative="..\obj\Debug\Tridion.Extensions.CoreService.dll">G:\CloudStorage\SkyDrive\Dev\Sources\Tridion.Extensions.CoreService\Tridion.Extensions.CoreService\Tridion.Extensions.CoreService\obj\Debug\Tridion.Extensions.CoreService.dll</Reference>
  <Namespace>Tridion.Extensions.CoreService</Namespace>
  <Namespace>Tridion.Extensions.CoreService.CoreService2011</Namespace>
  <Namespace>Tridion.Extensions.CoreService.Events</Namespace>
  <Namespace>Tridion.Extensions.CoreService.Extensions</Namespace>
  <Namespace>Tridion.Extensions.CoreService.Utils</Namespace>
</Query>

void Main()
{
	var client = Wrapper.GetCoreServiceInstance("t2013","Administrator","Tr1d10n2013", string.Empty);
	Console.WriteLine(client.GetApiVersion());
	//client.Impersonate("Siavash Shibani");
	
	client.GetSystemWideListXml(new PublicationsFilterData()).Dump();
	//"tcm:2-2007-512".ToTcmUri().GetItem<CategoryData>().GetRootChildern().Dump();
	"tcm:1003-2090-1024".ToTcmUri().GetItem<KeywordData>().GetClassifiedItems(ItemType.Folder).Dump();
	//var webdav = @"/webdav/200 Design/Building Blocks/System/Razor Templates/Component Design/News Item.cshtml";
	
	  var filter = new OrganizationalItemItemsFilterData
      {
          ItemTypes = new ItemType[] { ItemType.Component },
          BasedOnSchemas = new LinkToSchemaData[] { new LinkToSchemaData { IdRef = "tcm:2-1047-8" } }
      };
	  
	  client.GetListXml("tcm:1003-2008-2", filter).Dump();

	"tcm:1003-2096".ToTcmUri().GetItem<ComponentData>().getu.GetFieldAsString("Value", ContentType.Content).Dump();
	//webdav.GetItem().Dump();
	//TcmUri tcm = new TcmUri("tcm:2-46");
	
}

// Define other methods and classes here