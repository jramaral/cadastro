using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Cadastro.UI.Servico
{
    public abstract class FileResult : ActionResult
    {
        private string _fileDownloadName;

        protected FileResult(string contentType)
        {
            if(string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentException();
            }

            this.ContentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;
            response.ContentType = this.ContentType;

            if(!string.IsNullOrEmpty(this.FileDownloadName))
            {
                var headerValue = ContentDispositionHeaderValue.Parse(this.FileDownloadName);

                context.HttpContext.Response.AddHeader("Content-Disposition", headerValue.ToString());  

            }
            WriteFile(response);
        }

        protected abstract void WriteFile(HttpResponseBase response);

        public string ContentType { get; private set; }
        public string FileDownloadName
        {
            get { return (this._fileDownloadName ?? string.Empty); }
            set { this._fileDownloadName = value; }
        }
    }
}