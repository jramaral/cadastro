using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cadastro.Aplicacao;
using Cadastro.Dominio;

namespace Cadastro.UI.Controllers
{
    public class AlunosController : Controller
    {
        // GET: Alunos
        public ActionResult Index()
        {
            if(Session["usuarioid"] ==null)
            {
                return RedirectToAction("Logar", "Usuario");
            }


            var app = new AlunoAplicacao();
            var lista = app.ListarAluno();
            return View(lista);
            
        }

        [HttpPost]
        public ActionResult CadastroAluno(Aluno aluno)
        {

            if (ModelState.IsValid)
            {
                var app = new AlunoAplicacao();
                aluno.DataCadastro = DateTime.Now;
                app.NewAluno(aluno);

                var cidade = app.EncheComoCidade();
                ViewBag.Cidade = new SelectList(cidade, "NomeCidade", "NomeCidade");

            }
            return RedirectToAction("Index", "Alunos");

        }

        public ActionResult EditarAluno(int id = 0)
        {
            if (Session["usuarioid"] == null)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            var app = new AlunoAplicacao();

            var cidade = app.EncheComoCidade();
            ViewBag.Cidade = new SelectList(cidade, "NomeCidade", "NomeCidade");

            var aluno = app.ListarPorId(id.ToString());
            
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        public ActionResult EditarAluno(Aluno aluno)
        {
            var app = new AlunoAplicacao();
           app.AtualizarAluno(aluno);

            var cidade = app.EncheComoCidade();
            ViewBag.Cidade = new SelectList(cidade, "NomeCidade", "NomeCidade");

            return RedirectToAction("Index");
        }

        public ActionResult CadastroAluno()
        {

            if (Session["usuarioid"] == null)
            {
                return RedirectToAction("Logar", "Usuario");
            }

            var app = new AlunoAplicacao();
            var aluno =new Aluno();
            var cidade = app.EncheComoCidade();

            aluno.DataCadastro = DateTime.Now;

            ViewBag.DtCad = aluno.DataCadastro;

            ViewBag.Cidade = new SelectList(cidade, "NomeCidade", "NomeCidade");

            return View();
        }

    }
}