using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHosted.Controller
{
    public abstract class RavenController : ApiController
    {
        public IDocumentSession DocumentSession { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DocumentSession != null)
                {
                    using (DocumentSession)
                    {
                        DocumentSession.Dispose();
                        DocumentSession = null;
                    }
                }
            }

            base.Dispose(disposing);
        }

    }

}
