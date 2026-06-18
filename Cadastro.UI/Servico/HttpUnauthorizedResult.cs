using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadastro.UI.Servico
{
    public class HttpUnauthorizedResult : HttpStatusCodeResult
    {
        private const int UnauthorizedStatusCode = 401;
        public HttpUnauthorizedResult() : this(null) { }

        public HttpUnauthorizedResult(string statusDescription) : base(UnauthorizedStatusCode, statusDescription) { }

    }
}