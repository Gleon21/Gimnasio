using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AsistenteGimnasio.Models;

namespace AsistenteGimnasio.Controllers
{
    public class RecetasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Recetas
        public ActionResult Index(string id)
        {
            //var recetas = db.Recetas.Include(r => r.TiempoComida).Include(r => r.TipoReceta);
            var Receta = (from x in db.Recetas
                          where x.Nombre == id
                          select x).FirstOrDefault();
            if (Receta==null)
            {
                return HttpNotFound();
            }
            return View(Receta);
        }

        // GET: Recetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Recetas.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // GET: Recetas/Create
        public ActionResult Create()
        {
            ViewBag.TiempoComidaId = new SelectList(db.tiempoComidas, "TiempoComidaId", "Tiempo");
            ViewBag.TipoRecetaId = new SelectList(db.TipoRecetas, "TipoRecetaId", "Tipo");
            return View();
        }

        // POST: Recetas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecetaId,Nombre,TipoRecetaId,TiempoComidaId,Imagen,Ingredientes,Calorias,HidratosDeCrabono,Proteinas,grasa")] Receta receta)
        {
            if (ModelState.IsValid)
            {
                db.Recetas.Add(receta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TiempoComidaId = new SelectList(db.tiempoComidas, "TiempoComidaId", "Tiempo", receta.TiempoComidaId);
            ViewBag.TipoRecetaId = new SelectList(db.TipoRecetas, "TipoRecetaId", "Tipo", receta.TipoRecetaId);
            return View(receta);
        }

        // GET: Recetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Recetas.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            ViewBag.TiempoComidaId = new SelectList(db.tiempoComidas, "TiempoComidaId", "Tiempo", receta.TiempoComidaId);
            ViewBag.TipoRecetaId = new SelectList(db.TipoRecetas, "TipoRecetaId", "Tipo", receta.TipoRecetaId);
            return View(receta);
        }

        // POST: Recetas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecetaId,Nombre,TipoRecetaId,TiempoComidaId,Imagen,Ingredientes,Calorias,HidratosDeCrabono,Proteinas,grasa")] Receta receta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TiempoComidaId = new SelectList(db.tiempoComidas, "TiempoComidaId", "Tiempo", receta.TiempoComidaId);
            ViewBag.TipoRecetaId = new SelectList(db.TipoRecetas, "TipoRecetaId", "Tipo", receta.TipoRecetaId);
            return View(receta);
        }

        // GET: Recetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Recetas.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // POST: Recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receta receta = db.Recetas.Find(id);
            db.Recetas.Remove(receta);
            db.SaveChanges();
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
