using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cadastro.Aplicacao;
using Cadastro.Dominio;

namespace Cadastro.UI.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        public ActionResult Index()
        {
            if (Session["usuarioid"] == null)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            var app = new CidadeAplicacao();
            var lista = app.ListarCidade();
            return View(lista);
        }

        public ActionResult CadastrarCidade()
        {

            if (Session["usuarioid"] == null)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            return View();
            

        }

        [HttpPost]
        public  ActionResult CadastrarCidade(Cidade cidade)
        {
            if(ModelState.IsValid)
            {
                var app = new CidadeAplicacao();
                app.Inserir(cidade);
                
            }
            return RedirectToAction("Index", "Cidade");

        }

        [HttpPost]
        public ActionResult AtualizaCidade(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                var app = new CidadeAplicacao();
                app.AtualizarCidade(cidade);

            }
            return RedirectToAction("Index", "Cidade");
        }

        public ActionResult AtualizaCidade(int id=0)
        {
            if (Session["usuarioid"] == null)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            var app = new CidadeAplicacao();

            var cidade = app.ListarPorId(id.ToString());

            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
        }

        
    }
}