using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SelfHosted
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new HttpSelfHostConfiguration("http://localhost:1337");

            cfg.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            cfg.Routes.MapHttpRoute(
                "Default", "{*res}",
                new { controller = "StaticFile", res = RouteParameter.Optional });

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
