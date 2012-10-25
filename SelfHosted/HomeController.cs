using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SelfHosted
{
    public class HomeController : ApiController
    {
        public HttpResponseMessage Get(string res = "index.html")
        {
            var file = new FileInfo("res/" + res);
            if (!file.Exists)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var stream = file.OpenRead();

            var content = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream)
            };
            content.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return content;

        }
    }
}