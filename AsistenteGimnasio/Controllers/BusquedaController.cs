using AsistenteGimnasio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using static AsistenteGimnasio.Models.EntidadesTemporales;

namespace AsistenteGimnasio.Controllers
{
    public class BusquedaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static string busqueda = null;
        //public static EntidadesTemporales.BusquedaR busqueda = new EntidadesTemporales.BusquedaR();

        // GET: Busqueda
        public ActionResult Index(string Buscar)
        {
            var Ejercicios = (from x in db.Ejercicios
                              where x.Nombre.Contains(Buscar)
                              select x).ToList();
            //busqueda.busqueda = Busqueda;

            ViewData["Bus"] = Buscar;
            return View(Ejercicios);
        }
      
      
    }
}