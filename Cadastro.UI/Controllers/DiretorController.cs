using System;
using System.Linq;
using System.Web.Mvc;
using Cadastro.Aplicacao;
using Cadastro.UI.Models;

namespace Cadastro.UI.Controllers
{
    public class DiretorController : Controller
    {
        private readonly DiretorAplicacao _aplicacao;

        public DiretorController(DiretorAplicacao aplicacao)
        {
            _aplicacao = aplicacao;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListarDiretores(DataTablesDados model)
        {
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");

             var totalRegistros = _aplicacao.TotalDiretores();
             var lista = _aplicacao.ListarDiretoresPaginado(model.start, model.length);

            var dados = new
            {
                model.draw,
                recordsTotal = totalRegistros,
                recordsFiltered = totalRegistros,
                data = lista.Select(x => new
                {
                    Codigo = x.DiretorId,
                    Nome = x.Nome,
                    Email = x.Email,
                    DataCadastro = x.DataCadastro.ToShortDateString(),
                    Cidade = x.Cidade,
                    UrlEditar = x.DiretorId,
                    UrlRemover = x.DiretorId
                }).ToArray()
            };


            return Json(dados, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult RemoverDiretor(int codigo)
        {
            try
            {
              
                _aplicacao.RemoverDiretor(codigo);
                return Json(new { sucesso = true });
            }
            catch (Exception e)
            {
                // logar erro aqui se quiser (log4net, serilog, etc)
                return Json(new
                {
                    sucesso = false,
                    mensagem = "Erro ao remover a cidade."
                    // em dev você pode retornar ex.Message
                });
            }
        }

        public ActionResult CadastroDiretor()
        {
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");

            //var app = new AlunoAplicacao();
            //var aluno = new Aluno();
            //var cidade = app.EncheComoCidade();

            //aluno.DataCadastro = DateTime.Now;

            //ViewBag.DtCad = aluno.DataCadastro;

            //ViewBag.Cidade = new SelectList(cidade, "NomeCidade", "NomeCidade");

            return View();
        }
    }
}