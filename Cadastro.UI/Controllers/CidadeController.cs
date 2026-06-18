using Cadastro.Aplicacao;
using Cadastro.Dominio;
using Cadastro.UI.Models;
using Cadastro.UI.Validations;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

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
        [HttpPost]
        public FileResult GerarPdf(int id)
        {

            using (var memoryStream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.SetMargins(20, 20, 20, 20);

                string caminhoLogo = Server.MapPath("~/Content/img/logo_empresa.png");

                var fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                var fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                Table cabecalho = new Table(new UnitValue[]
                    {
                        UnitValue.CreatePointValue(90),
                        UnitValue.CreatePercentValue(100)
                    });
                cabecalho.UseAllAvailableWidth();

                //Logo
                ImageData imageData = ImageDataFactory.Create(caminhoLogo);

                Image logo = new Image(imageData);
                logo.ScaleToFit(140, 60);

                Cell celulaLogo = new Cell()
                    .Add(logo)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                    .SetTextAlignment(TextAlignment.CENTER);

                cabecalho.AddCell(celulaLogo);

                //dados da empresa
                Table dadosEmpresa = new Table(1);
                dadosEmpresa.UseAllAvailableWidth();

                dadosEmpresa.AddCell(new Cell()
                    .Add(new Paragraph("EMPRESA XYZ LTDA").SetFont(fontBold))
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(Border.NO_BORDER));


                dadosEmpresa.AddCell(new Cell()
                       .Add(new Paragraph($"Data de emissão: {DateTime.Now:dd/MM/yyyy HH:mm}"))
                       .SetTextAlignment(TextAlignment.CENTER)
                       .SetBorder(Border.NO_BORDER));

                Cell celulaEmpresa = new Cell()
                    .Add(dadosEmpresa)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                cabecalho.AddCell(celulaEmpresa);

                document.Add(cabecalho);

                document.Close();

                byte[] bytes = memoryStream.ToArray();

                return File(bytes, "application/pdf", "RelatorioCidades.pdf");

            }

        }
    }
}