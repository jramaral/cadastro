using System;
using System.Linq;
using System.Web.Mvc;
using Cadastro.Aplicacao;
using Cadastro.Dominio;
using Cadastro.UI.Models;

namespace Cadastro.UI.Controllers
{
    public class AlunosController : Controller
    {
        // GET: Alunos
        public ActionResult Index()
        {
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");


            var app = new AlunoAplicacao();
            var lista = app.ListarAluno();
            return View(lista);
        }

        // public ActionResult Lista()
        // {
        //     if(Session["usuarioid"] ==null)
        //     {
        //         return RedirectToAction("Logar", "Usuario");
        //     }
        //
        //
        //     var app = new AlunoAplicacao();
        //     var lista = app.ListarAluno();
        //     return View(lista);
        //     
        // }

        [HttpPost]
        public ActionResult ListarAlunos(DataTablesDados model)
        {
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");


            var app = new AlunoAplicacao();
            var lista = app.ListarAluno();

            var dados = new
            {
                model.draw,
                recordsTotal = 2,
                recordsFiltered = 10,
                data = lista.Select(x => new
                {
                    x.Codigo,
                    x.Nome,
                    CPF = x.Cpf,
                    Sexo = x.Sexo.ToString(),
                    x.Cidade,
                    UrlEditar = Url.Action(nameof(EditarAluno), new { id = x.Codigo })
                }).ToArray()
            };


            return Json(dados, JsonRequestBehavior.AllowGet);
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
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");
            var app = new AlunoAplicacao();

            var cidade = app.EncheComoCidade();
            ViewBag.Cidade = new SelectList(cidade, "NomeCidade", "NomeCidade");

            var aluno = app.ListarPorId(id.ToString());

            if (aluno == null) return HttpNotFound();
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
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");

            var app = new AlunoAplicacao();
            var aluno = new Aluno();
            var cidade = app.EncheComoCidade();

            aluno.DataCadastro = DateTime.Now;

            ViewBag.DtCad = aluno.DataCadastro;

            ViewBag.Cidade = new SelectList(cidade, "NomeCidade", "NomeCidade");

            return View();
        }
    }
}