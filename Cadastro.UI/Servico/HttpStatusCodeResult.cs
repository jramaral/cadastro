using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastro.UI.Servico
{
    public class HttpStatusCodeResult: ActionResult
    {
        public HttpStatusCodeResult(int statusCode) : this(statusCode, null) { }

        public HttpStatusCodeResult(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException("context");
            }
            context.HttpContext.Response.StatusCode = this.StatusCode;
            if(this.StatusDescription != null)
            {
                context.HttpContext.Response.StatusDescription = this.StatusDescription;
            }
        }

        // Properties
        public int StatusCode { get; private set; }
        public string StatusDescription { get; private set; }

    }
}