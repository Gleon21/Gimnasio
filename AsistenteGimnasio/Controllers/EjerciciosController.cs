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
using System.IO;
using WMPLib;

namespace AsistenteGimnasio.Controllers
{
    public class EjerciciosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private static int idcache;
        private static int idcache_archivos;

        private static int idcache_editar;
        private static RegistroEjercicio re;


        // GET: Ejercicios

        [HttpGet]
        public ActionResult Index(string categoria)
        {
           
            var ejercicios = db.Ejercicios.Include(e => e.Categoria);
            ejercicios = null;
            var cat = from x in db.Categorias
                      where x.Nombre == categoria
                      select x;
            if (cat == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            foreach (var categoriaselect in cat)
            {
                ejercicios = (from x in db.Ejercicios
                              where x.CategoriaId == categoriaselect.CategoriaId && x.hidden == false
                              select x).Include(e => e.Categoria);
            }

            if (ejercicios == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            return View(ejercicios.ToList());
        }
        [HttpGet]
        public ActionResult Listado()
        {

            var ejercicios = db.Ejercicios.Include(e => e.Categoria);


            return View(ejercicios.ToList());
        }
     
        // subir Imagen


        //public ActionResult subir()
        //{

        //    return View();
        //}



        //[HttpPost]
        //public void Subirimg(HttpPostedFileBase file)
        //{
        //    if (file == null) return;

        //    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();

        //    file.SaveAs(Server.MapPath("~/Resources/" + archivo));
        //}


        // GET: Ejercicios/Details/5
        public ActionResult Editar(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ejercicio ejercicio = db.Ejercicios.Find(id);
            if (ejercicio == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nombre", ejercicio.CategoriaId);
            return View(ejercicio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "EjercicioId,Nombre,CategoriaId,Descripcion,Calorias")] Ejercicio ejercicio)
        {
            if (ModelState.IsValid)
            {
                idcache_editar = ejercicio.EjercicioId;
                //db.Entry(ejercicio).State = EntityState.Modified;
                var ej = (from x in db.Ejercicios
                          where x.EjercicioId == idcache_editar
                          select x).FirstOrDefault();
                ej.Nombre = ejercicio.Nombre;
                ej.Descripcion = ejercicio.Descripcion;
                ej.Calorias = ejercicio.Calorias;
                ej.CategoriaId = ejercicio.CategoriaId;
                db.SaveChanges();
                return RedirectToAction("Editar/" + idcache_editar, "ejercicios");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nombre", ejercicio.CategoriaId);
            return RedirectToAction("Details/" + idcache_editar, "Ejercicios");
        }
        //seleccionar uno
        public ActionResult ViewEjercicio(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ViewEjercicio/1");
            }
            Ejercicio ejercicio = db.Ejercicios.Find(id);
            if (ejercicio == null)
            {
                return RedirectToAction("ViewEjercicio/1");
            }
            return View(ejercicio);
        }
        public ActionResult Ejercicio(int? id)
        {
            idcache = Convert.ToInt32(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ejercicio ejercicio = db.Ejercicios.Find(id);
            if (ejercicio == null)
            {
                return HttpNotFound();
            }
            re = new RegistroEjercicio();

            if (re != null)
            {
                re.Fecha = DateTime.Now.ToString("dd/MM/yyyy");
                re.HoraInicio = DateTime.Now.ToString("HH:mm:ss");
                re.EjercicioId = Convert.ToInt32(id);

            }

            return View(ejercicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ejercicio(int? id, [Bind(Include = "EjercicioId,Nombre,CategoriaId")] Ejercicio ejercicio)
        {
            if (re != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    re.UserId = User.Identity.GetUserId();
                }
                re.HoraFin = DateTime.Now.ToString("HH:mm:ss");
                DateTime h1 = DateTime.ParseExact(re.HoraInicio, "HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime h2 = DateTime.ParseExact(re.HoraFin, "HH:mm:ss", CultureInfo.InvariantCulture);


                re.Tiempo = Convert.ToString(h2.Subtract(h1));
                db.RegistroEjercicios.Add(re);
                db.SaveChanges();
            }
            return RedirectToAction("Preview/" + idcache);
        }

        public ActionResult Preview(int? id)
        {
            idcache = Convert.ToInt32(id);


            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Ejercicio ejercicio = db.Ejercicios.Find(id);
            if (ejercicio == null)
            {
                return HttpNotFound();
            }
            return View(ejercicio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Preview([Bind(Include = "EjercicioId,Nombre,CategoriaId")] Ejercicio ejercicio)
        {
            if (idcache == 0)
            {
                return RedirectToAction("Index");
            }
            Ejercicio ejercicio2 = db.Ejercicios.Find(idcache);
            if (ejercicio2 == null)
            {
                return HttpNotFound();
            }

            //return View(ejercicio2);
            return RedirectToAction("Ejercicio/" + ejercicio2.EjercicioId);



        }

        //Obtener Tiempo de duracion del video Subido
        public string time(string file)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayerClass();
            IWMPMedia mediainfo = wmp.newMedia(file);
            string[] x = mediainfo.duration.ToString().Split('.');
            return CalcularTiempo(Convert.ToInt32(x[0]));
        }
        private String CalcularTiempo(Int32 tsegundos)
        {
            Int32 horas = (tsegundos / 3600);
            Int32 minutos = ((tsegundos - horas * 3600) / 60);
            Int32 segundos = tsegundos - (horas * 3600 + minutos * 60);
            string horas2dig, minutos2dig, segundos2dig;
            if (horas < 10)
            {
                horas2dig = "0" + horas.ToString();
            }
            else
            {
                horas2dig = horas.ToString();
            }
            if (minutos < 10)
            {
                minutos2dig = "0" + minutos.ToString();
            }
            else
            {
                minutos2dig = minutos.ToString();
            }
            if (segundos < 10)
            {
                segundos2dig = "0" + segundos.ToString();
            }
            else
            {
                segundos2dig = segundos.ToString();
            }
            return horas2dig + ":" + minutos2dig + ":" + segundos2dig;
        }

        //Subir Archivos(Imagen y Video)

        public ActionResult subirArchivos(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            idcache_archivos = Convert.ToInt32(id);
            ViewBag.idej= Convert.ToInt32(id);
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult subirArchivosEJ(HttpPostedFileBase Poster, HttpPostedFileBase Video)
        {
            var rutaarchivos = db.Settings.FirstOrDefault();
            if (idcache_archivos > 0)
            {
                var ej = (from x in db.Ejercicios
                          where x.EjercicioId == idcache_archivos
                          select x).FirstOrDefault();

                if (ej != null)
                {
                    if (Video != null)
                    {
                        string archivo = (Video.FileName).ToLower(); 
                        if (System.IO.File.Exists(Server.MapPath("~/Resources/Videos/" + archivo)))
                        {
                            var archivofor = archivo.Split('.');
                            string archivoNuevo = archivofor[0] + "(1)." + archivofor[1];


                            Video.SaveAs(/*"~/Resources/Videos/"*/ /*@rutaarchivos.RutaVideos+"/"+ archivo*/Server.MapPath("~/Resources/Videos/" + archivoNuevo ));

                            ej.EnlaceVideo = archivoNuevo;
                        }
                        else
                        {
                            Video.SaveAs(/*"~/Resources/Videos/"*/ /*@rutaarchivos.RutaVideos+"/"+ archivo*/Server.MapPath("~/Resources/Videos/" + archivo));

                            ej.EnlaceVideo = archivo;
                        }
                       
                        ej.Duracion = time(/*@rutaarchivos.RutaVideos + "/" + archivo*/Server.MapPath("~/Resources/Videos/" + ej.EnlaceVideo));
                    }
                    if (Poster != null)
                    {
                        
                        string archivo = (Poster.FileName).ToLower(); ;

                       


                        if (System.IO.File.Exists(Server.MapPath("~/Resources/Imagenes/" + archivo)))
                        {
                            var archivofor = archivo.Split('.');
                            string archivoNuevo = archivofor[0] + "(1)." + archivofor[1];


                            Poster.SaveAs(Server.MapPath("~/Resources/Imagenes/" + archivoNuevo));
                            ej.Imagen = archivoNuevo;
                        }
                        else
                        {
                            Poster.SaveAs(Server.MapPath("~/Resources/Imagenes/" + archivo));
                            ej.Imagen = archivo;
                        }
                       
                    }
                    db.SaveChanges();
                }


                return RedirectToAction("Listado");
            }

            return RedirectToAction("Listado");

        }
        public ActionResult EditarArchivos(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            idcache_editar = Convert.ToInt32(id);
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditarArchivosEJ(HttpPostedFileBase Poster, HttpPostedFileBase Video)
        {
            var rutaarchivos = db.Settings.FirstOrDefault();
            if (idcache_editar > 0)
            {
                var ej = (from x in db.Ejercicios
                          where x.EjercicioId == idcache_editar
                          select x).FirstOrDefault();

                if (ej != null)
                {
                    if (Video != null)
                    {
                        string archivo = (Video.FileName).ToLower(); ;

                        if (System.IO.File.Exists(Server.MapPath("~/Resources/Videos/" + archivo)))
                        {
                            var archivofor = archivo.Split('.');
                            string archivoNuevo = archivofor[0] + "(1)." + archivofor[1];

                            Video.SaveAs(/*"~/Resources/Videos/"*/ /*@rutaarchivos.RutaVideos+"/"+ archivo*/Server.MapPath("~/Resources/Videos/" + archivoNuevo));

                            ej.EnlaceVideo = archivo + "(1)";
                        }
                        else
                        {
                            Video.SaveAs(/*"~/Resources/Videos/"*/ /*@rutaarchivos.RutaVideos+"/"+ archivo*/Server.MapPath("~/Resources/Videos/" + archivo));

                            ej.EnlaceVideo = archivo;
                        }

                        ej.Duracion = time(/*@rutaarchivos.RutaVideos + "/" + archivo*/Server.MapPath("~/Resources/Videos/" + ej.EnlaceVideo));
                    }
                    if (Poster != null)
                    {

                        string archivo = (Poster.FileName).ToLower(); ;
                        if (System.IO.File.Exists(Server.MapPath("~/Resources/Imagenes/" + archivo)))
                        {
                            var archivofor = archivo.Split('.');
                            string archivoNuevo = archivofor[0] + "(1)." + archivofor[1];

                            Poster.SaveAs(Server.MapPath("~/Resources/Imagenes/" + archivoNuevo));
                            ej.Imagen = archivoNuevo;
                        }
                        else
                        {
                            Poster.SaveAs(Server.MapPath("~/Resources/Imagenes/" + archivo));
                            ej.Imagen = archivo;
                        }

                    }
                    db.SaveChanges();
                }


                return RedirectToAction("Editar/"+idcache_editar,"Ejercicios");
            }

            return RedirectToAction("Editar/" + idcache_editar, "Ejercicios");

        }
        // Registrar tiempo del cronometro
        public ActionResult RegistrarTiempo(string Tiempo, string HoraIni)
        {
            if (User.Identity.IsAuthenticated)
            {
               

                RegistroEjercicio rej = new RegistroEjercicio();
                rej.EjercicioId = 61;
                rej.Tiempo = Tiempo;
                rej.Fecha = DateTime.Now.ToString("dd/MM/yyyy");
                rej.HoraInicio = HoraIni;
                rej.UserId = User.Identity.GetUserId();
                db.RegistroEjercicios.Add(rej);
                db.SaveChanges();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            

            return RedirectToAction("Historial","Account");
        }


        // GET: Ejercicios/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nombre");
            return View();
        }

        // POST: Ejercicios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EjercicioId,Nombre,EnlaceVideo,Imagen,Duracion,Calorias,Descripcion,CategoriaId")] Ejercicio ejercicio)
        {
            if (ModelState.IsValid)
            {
                db.Ejercicios.Add(ejercicio);
                db.SaveChanges();
                idcache_archivos = ejercicio.EjercicioId;
                if (ejercicio.EnlaceVideo == null || ejercicio.Imagen==null)
                {
                    return RedirectToAction("SubirArchivos" + "/" + idcache_archivos, "Ejercicios");
                }
                return RedirectToAction("Listado","Ejercicios");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nombre", ejercicio.CategoriaId);
            return View(ejercicio);
        }

        // GET: Ejercicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ejercicio ejercicio = db.Ejercicios.Find(id);
            if (ejercicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nombre", ejercicio.CategoriaId);
            return View(ejercicio);
        }

        // POST: Ejercicios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EjercicioId,Nombre,CategoriaId")] Ejercicio ejercicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ejercicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nombre", ejercicio.CategoriaId);
            return View(ejercicio);
        }

        // GET: Ejercicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ejercicio ejercicio = db.Ejercicios.Find(id);
            if (ejercicio == null)
            {
                return HttpNotFound();
            }
            return View(ejercicio);
        }

        // POST: Ejercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ejercicio ejercicio = db.Ejercicios.Find(id);
            db.Ejercicios.Remove(ejercicio);
            db.SaveChanges();
            return RedirectToAction("Listado");
        }

        public ActionResult Timer()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Ejercicio ejercicio = db.Ejercicios.Find(id);
            //if (ejercicio == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: Ejercicios/Delete/5
        [HttpPost, ActionName("Timer")]
        [ValidateAntiForgeryToken]
        public ActionResult Timer(int id)
        {
            //Ejercicio ejercicio = db.Ejercicios.Find(id);
            //db.Ejercicios.Remove(ejercicio);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}
