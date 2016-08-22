using System.ServiceModel;
using System.Xml;
using Tridion.ContentManager.CoreService.Client;

namespace Tridion.Extensions.CoreService
{
    public static class Wrapper
    {
        public static SessionAwareCoreServiceClient Instance { get; set; }

        public static SessionAwareCoreServiceClient GetCoreServiceInstance(string hostName, string username, string password, string domain, CoreServiceInstance version)
        {
            var netTcpBinding = new NetTcpBinding
            {
                MaxReceivedMessageSize = 2147483647,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxStringContentLength = 2147483647,
                    MaxArrayLength = 2147483647
                }
            };

            var remoteAddress =
                new EndpointAddress(
                    string.Format("net.tcp://{0}:2660/CoreService/{1}/netTcp", hostName, (int)version));

            var coreServiceClient = new SessionAwareCoreServiceClient(netTcpBinding, remoteAddress);

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                coreServiceClient.ClientCredentials.Windows.ClientCredential.UserName = username;
                coreServiceClient.ClientCredentials.Windows.ClientCredential.Password = password;
            }

            if (!string.IsNullOrEmpty(domain))
                coreServiceClient.ClientCredentials.Windows.ClientCredential.Domain = domain;

            //coreServiceClient.Impersonate("Siavash Shibani");

            Instance = coreServiceClient;

            return Instance;
        }

        public static SessionAwareCoreServiceClient GetCoreServiceWsHttpInstance(string hostName, string username, string password, string domain, CoreServiceInstance version)
        {
            var httpBinding = new WSHttpBinding
            {
                MaxReceivedMessageSize = 2147483647,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxStringContentLength = 2147483647,
                    MaxArrayLength = 2147483647
                }
            };

            var remoteAddress =
                new EndpointAddress(
                    string.Format("http://{0}/webservices/CoreService{1}.svc/wsHttp", hostName, (int)version));

            var coreServiceClient = new SessionAwareCoreServiceClient(httpBinding, remoteAddress);

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                coreServiceClient.ClientCredentials.Windows.ClientCredential.UserName = username;
                coreServiceClient.ClientCredentials.Windows.ClientCredential.Password = password;
            }

            if (!string.IsNullOrEmpty(domain))
                coreServiceClient.ClientCredentials.Windows.ClientCredential.Domain = domain;

            //coreServiceClient.Impersonate("Siavash Shibani");

            Instance = coreServiceClient;

            return Instance;
        }
    }

    public enum CoreServiceInstance
    {
        Tridion2011 = 2011,
        Tridion2013 = 2013,
        SdlWeb8 = 201501
    }
}