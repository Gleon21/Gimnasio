using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AsistenteGimnasio.Models;
using Microsoft.AspNet.Identity;

namespace AsistenteGimnasio.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

  
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //------------------------Grafica Linea-----------------------

        public ActionResult GetDataLinePesos(int periodo)
        {
            DatosPesos datos = new DatosPesos();
            decimal[] Pesos = new decimal[11];

            string user = User.Identity.GetUserId();

            var UltimoPeso = (from x in db.Saluds
                                where x.UserId == user
                                select x).OrderByDescending(x => x.SaludId).FirstOrDefault();
            var obtenerPesos = (from x in db.Saluds
                                where x.UserId == user
                                select x);
            
            
            string dm;
            for (int i = 1; i < 13; i++)
            {
                if (i <= 9)
                {
                    dm = "0" + i;
                }
                else
                {
                    dm = i.ToString();
                }

                foreach (var item in obtenerPesos)
                {
                    DateTime fecha2 = DateTime.ParseExact(dm + "/" + dm + "/" + periodo, "dd/MM/yyyy", null);
                    DateTime fecha1 = DateTime.ParseExact(item.FechadeRegistro, "MM/dd/yyyy", null);

                    if (fecha1.Month == fecha2.Month && fecha1.Year == fecha2.Year)
                    {


                        Pesos[i - 2] += item.Peso;
                        
                       
                        

                    }

                }
                if (obtenerPesos.Count() < 12)
                {

                    
                    if (UltimoPeso != null )
                    {
                        
                        for (int ini = obtenerPesos.Count(); ini < 12; ini++)
                        {
                            Pesos[ini - 1] = UltimoPeso.Peso;
                        }
                        
                    }
                    
                }
                
            }




            datos.Pesos = Pesos;

            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public class DatosPesos
        {
            public decimal[] Pesos { get; set; }
           

        }



       
    }
}