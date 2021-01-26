using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastro.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["usuarioid"] == null)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            return View();
        }
        




    }
}