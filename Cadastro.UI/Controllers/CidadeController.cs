using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using Cadastro.Aplicacao;
using Cadastro.Dominio;
using Cadastro.UI.Models;
using Cadastro.UI.Validations;
using Newtonsoft.Json;

namespace Cadastro.UI.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        public ActionResult Index()
        {
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");
            var app = new CidadeAplicacao();
            var lista = app.ListarCidade();
            return View(lista);
        }

        public ActionResult CadastrarCidade()
        {
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");
            return View();
        }

        [HttpPost]
        public ActionResult ListarCidades(DataTablesDados model)
        {
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");


            var app = new CidadeAplicacao();

            var totalRegistros = app.TotalCidades();

            var lista = app.ListarCidadePaginado(model.start, model.length);

            //var lista = app.ListarCidade();

            var dados = new
            {
                model.draw,
                recordsTotal = totalRegistros,
                recordsFiltered = totalRegistros,
                data = lista.Select(x => new
                {
                    Codigo = x.CodigoCidade,
                    Cidade = x.NomeCidade,
                    Uf = x.UF,
                    x.Cep,
                    UrlEditar = Url.Action(nameof(AtualizaCidade), new { id = x.CodigoCidade }),
                    UrlRemover = x.CodigoCidade
                }).ToArray()
            };


            return Json(dados, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarCidade(Cidade cidade)
        {
            if (!await RecaptchaValidation.Validate(Request))
            {
                ModelState.AddModelError(string.Empty, "Você não confirmou se não é um robô");

                return View();
            }

            if (ModelState.IsValid)
            {
                var app = new CidadeAplicacao();
                app.Inserir(cidade);
                return RedirectToAction("Index", "Cidade");
            }

            return View();
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

        [HttpPost]
        public ActionResult RemoverCidade(int codigo)
        {
            try
            {
                var app = new CidadeAplicacao();
                app.RemoverCidade(codigo);
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

        public ActionResult AtualizaCidade(int id = 0)
        {
            if (Session["usuarioid"] == null) return RedirectToAction("Logar", "Usuario");
            var app = new CidadeAplicacao();

            var cidade = app.ListarPorId(id.ToString());

            if (cidade == null) return HttpNotFound();
            return View(cidade);
        }

        public static CaptchaResponse ValidateCaptcha(string response)
        {
            var secret = WebConfigurationManager.AppSettings["recaptchaPrivateKey"];

            var cliente = new WebClient();

            var jsonResult = cliente.DownloadString(
                string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret,
                    response));

            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult);
        }
    }
}