using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cadastro.Aplicacao;
using Cadastro.Dominio;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;


namespace Cadastro.UI.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReportAluno()
        {
            var app = new AlunoAplicacao();
            var lista = app.ListarAluno();
            return View(lista);
            
        }
        public ActionResult ReportAlunoCidade()
        {

            return View();
        }
        public ActionResult ReportCidade()
        {
            var app = new CidadeAplicacao();
            var lista = app.ListarCidade();
            return View(lista);
           
        }
    }
}