using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                return HttpNotFound();
            }
            else
            {
                Session["usuarioid"] = user.Email;
                
                return RedirectToAction("Index", "Home");
                
            }
        }

        public ActionResult Logar()
        {
            if (Session["usuarioid"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            var usuario = new Usuario();
            return View(usuario);
        }

        public ActionResult CadastraUsuario()
        {
            var app = new UsuarioAplicacao();
            var cidade = app.EncheComoCidade();
 

            ViewBag.Cidade = new SelectList(cidade,"NomeCidade","NomeCidade");

           return View();
        }

        [HttpPost]
        public ActionResult CadastraUsuario(Usuario usuario)
        {
            var app = new UsuarioAplicacao();
            usuario.DataCadastro = DateTime.Now;
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