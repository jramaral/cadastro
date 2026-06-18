using System;
using System.Web.Mvc;
using Cadastro.Aplicacao;
using Cadastro.Dominio;

namespace Cadastro.UI.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpPost]
        public ActionResult Logar(Usuario user)
        {
            var app = new UsuarioAplicacao();
            var usuario = app.ListarPorId(user.Email, user.Senha);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário ou senha não confere");
                return View();
            }

            Session["usuarioid"] = user.Email;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logar()
        {
            if (Session["usuarioid"] != null) return RedirectToAction("Index", "Home");
            var usuario = new Usuario();
            usuario.Email = "jose@teste.com";
            usuario.Senha = "123";
            return View(usuario);
        }

        public ActionResult CadastraUsuario()
        {
            var app = new UsuarioAplicacao();
            var cidade = app.EncheComoCidade();


            ViewBag.Cidade = new SelectList(cidade, "NomeCidade", "NomeCidade");

            return View();
        }

        [HttpPost]
        public ActionResult CadastraUsuario(Usuario usuario)
        {
            var app = new UsuarioAplicacao();
            usuario.DataCadastro = DateTime.Now;
            usuario.DataNascimento = Convert.ToDateTime("27/05/1974");
            app.NewUsuario(usuario);

            var cidade = app.EncheComoCidade();
            ViewBag.Cidade = new SelectList(cidade, "NomeCidade", "NomeCidade");

            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("usuarioid");

            return RedirectToAction("Logar", "Usuario");
        }
    }
}