using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using SelfHosted.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Web.Routing;


namespace SelfHosted
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new HttpSelfHostConfiguration("http://localhost:1337");

            cfg.MaxReceivedMessageSize = 16L * 1024 * 1024 * 1024;
            cfg.TransferMode = TransferMode.StreamedRequest;
            cfg.ReceiveTimeout = TimeSpan.FromMinutes(20);            

            cfg.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            cfg.Routes.MapHttpRoute(
                "Default", "{*res}",
                new { controller = "StaticFile", res = RouteParameter.Optional });

            var documentStore = new EmbeddableDocumentStore { DataDirectory = new FileInfo("db/").DirectoryName };
            documentStore.Initialize();
            cfg.Filters.Add(new RavenDbApiAttribute(documentStore));


            using (HttpSelfHostServer server = new HttpSelfHostServer(cfg))
            {
                Console.WriteLine("Initializing server.");
                server.OpenAsync().Wait();
                Console.WriteLine("Server ready at: " + cfg.BaseAddress);
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }        
    }
}
