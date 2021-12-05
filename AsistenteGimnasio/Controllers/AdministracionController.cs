using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsistenteGimnasio.Controllers
{
    public class AdministracionController : Controller
    {
        // GET: Administracion
        public ActionResult Index()
        {
            return View();
            
        }
        public ActionResult LoginbyQR(string id)
        {
            ViewBag.Us = id;
            return View();
        }
    }
}