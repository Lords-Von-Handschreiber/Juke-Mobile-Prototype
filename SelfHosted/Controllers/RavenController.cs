using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHosted.Controllers
{
    public abstract class RavenController : ApiController
    {
        public IDocumentSession Db { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Db != null)
                {
                    using (Db)
                    {
                        Db.Dispose();
                        Db = null;
                    }
                }
            }

            base.Dispose(disposing);
        }

    }

}
