using Raven.Client;
using SelfHosted.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SelfHosted.Helper
{
    public class RavenDbApiAttribute : ActionFilterAttribute
    {
        readonly IDocumentStore documentStore;

        public RavenDbApiAttribute(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");
            this.documentStore = documentStore;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as RavenController;
            if (controller == null)
                return;

            controller.DocumentSession = documentStore.OpenSession();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as RavenController;
            if (controller == null)
                return;

            controller.DocumentSession.Dispose();
        }
    }

}
