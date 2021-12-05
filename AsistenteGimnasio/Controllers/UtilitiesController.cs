using AsistenteGimnasio.Models;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.AspNet.Identity;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace AsistenteGimnasio.Controllers
{

    public class UtilitiesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        private readonly UserManager<ApplicationUser> userManager;
        private static RegistroEjercicio re;



        // GET: Utilities
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Clientes()
        {
            
            if (User.IsInRole("Administrador")==false)
            {
                return RedirectToAction("Index", "Home");

            }
            
            
            //var users = db.Users.ToList();
            //users = null;
            //var Rolcliente = (from x in db.Roles
            //                  where x.Name == "Cliente"
            //                  select x.Users).ToList();
            //foreach (var item in Rolcliente)
            //{
            //        users.Add( (from x in db.Users
            //                 where x == item
            //                 select x).FirstOrDefault());
            //}
            ApplicationUser user = new ApplicationUser();
            var users = db.Users.ToList();
            return View(users.ToList());
        }
        public ActionResult ClientesActivos()
        {
           
            if (User.IsInRole("Administrador") == false)
            {
                return RedirectToAction("Index", "Home");

            }

            

            //var users = db.Users.ToList();
            //users = null;
            //var Rolcliente = (from x in db.Roles
            //                  where x.Name == "Cliente"
            //                  select x.Users).ToList();
            //foreach (var item in Rolcliente)
            //{
            //        users.Add( (from x in db.Users
            //                 where x == item
            //                 select x).FirstOrDefault());
            //}
            ApplicationUser user = new ApplicationUser();
            //var users = db.Users.ToList();
            var users = (from x in db.Users
                         where x.Activo == true
                         select x).ToList(); ;
            return View(users.ToList());
            
        }

        public ActionResult DetallesUsuario(string id)
        {
            /**
            if (User.IsInRole("Administrador") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            **/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetallesUsuario([Bind(Include = "Id,Fecha_Registro,Nombre,Apellido,Sexo,Fecha_de_Nacimiento,Direccion,pss2,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (User.IsInRole("Administrador") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                //applicationUser.Sexo = "Femenino";
                db.SaveChanges();
                return View(applicationUser);
            }
            
            return View(applicationUser);
        }

        public ActionResult ModificarUsuario(string Id, string Nombre, string Apellido, string Email, string Fecha_de_Nacimiento, string Sexo, string Fecha_Registro, string Direccion)
        {
            if (User.IsInRole("Administrador") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            ApplicationUser applicationUser = db.Users.Find(Id);

            applicationUser.Nombre = Nombre;
            applicationUser.Apellido = Apellido;
            applicationUser.Direccion = Direccion;
            applicationUser.Email = Email;
            applicationUser.Fecha_de_Nacimiento = Fecha_de_Nacimiento;
            applicationUser.Sexo = Sexo;
            db.SaveChanges();

            return RedirectToAction("DetallesUsuario/" + Id, "Utilities");
        }
        //--------------Notificaciones-------------------------------------------
        public ActionResult Notificaciones()
        {
            return View();
        }


        //---------------Estadisticas---------------------------------------------
        public ActionResult Estadisticas()
        {
            /**
            if (User.IsInRole("Administrador") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            **/
            return View();
        }
        //------------------------Graficas----------------------------------------
        //------------------------Circular----------------------------------------
        public ActionResult GetDataCircle()
        {
            Datos datos = new Datos();

            List<nombre_cantidad> listnc = new List<nombre_cantidad>();

            re = null;

            var ej = db.Ejercicios.ToList();



            foreach (var item in ej)
            {
                int rej = (from x in db.RegistroEjercicios
                           where x.EjercicioId == item.EjercicioId
                           select x).Count();

                nombre_cantidad nc = new nombre_cantidad();
                nc.cantidad = rej;
                nc.nombre = item.Nombre;
                listnc.Add(nc);


            }

            var dt = listnc.OrderByDescending(x => x.cantidad).Take(5).ToList();
            listnc.Clear();
            int i = 0;
            foreach (var item in dt)
            {
                if (i == 0)
                {
                    datos.Ejercicio1 = item.nombre;
                    datos.TEjercicio1 = item.cantidad;
                }
                if (i == 1)
                {
                    datos.Ejercicio2 = item.nombre;
                    datos.TEjercicio2 = item.cantidad;
                }
                if (i == 2)
                {
                    datos.Ejercicio3 = item.nombre;
                    datos.TEjercicio3 = item.cantidad;

                }
                if (i == 3)
                {
                    datos.Ejercicio4 = item.nombre;
                    datos.TEjercicio4 = item.cantidad;
                }
                if (i == 4)
                {
                    datos.Ejercicio5 = item.nombre;
                    datos.TEjercicio5 = item.cantidad;
                }

                i++;
            }
            return Json(datos, JsonRequestBehavior.AllowGet);


        }
        public class nombre_cantidad
        {
            public int cantidad { get; set; }
            public string nombre { get; set; }
        }
        public class Datos
        {

            public string Ejercicio1 { get; set; }
            public int TEjercicio1 { get; set; }
            public string Ejercicio2 { get; set; }
            public int TEjercicio2 { get; set; }
            public string Ejercicio3 { get; set; }
            public int TEjercicio3 { get; set; }
            public string Ejercicio4 { get; set; }
            public int TEjercicio4 { get; set; }
            public string Ejercicio5 { get; set; }
            public int TEjercicio5 { get; set; }
        }

        //------------------------Grafica Linea-----------------------

        public ActionResult GetDataLine(int periodo)
        {
            DatosSesiones datosSesiones = new DatosSesiones();
            int[] sesiones = new int[12];
            int[] Usuarios = new int[12];

            var ses = db.Sesiones.ToList();
            var usu = db.Users.ToList();
            int pos = 0;
            int pos2 = 0;
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

                foreach (var item in ses)
                {
                    DateTime fecha2 = DateTime.ParseExact(dm + "/" + dm + "/" + periodo, "dd/MM/yyyy", null);
                    DateTime fecha1 = DateTime.ParseExact(item.Fecha, "MM/dd/yyyy", null);

                    if (fecha1.Month == fecha2.Month && fecha1.Year == fecha2.Year)
                    {

                        sesiones[i - 1] += 1;

                    }

                }

                pos++;
            }
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

                foreach (var item in usu)
                {
                    DateTime fecha2 = DateTime.ParseExact(dm + "/" + dm + "/" + periodo, "dd/MM/yyyy", null);
                    DateTime fecha1 = DateTime.ParseExact(item.Fecha_Registro, "MM/dd/yyyy", null);
                    if (fecha1 <= fecha2)
                    {

                        Usuarios[i - 2] += 1;

                    }

                }

                pos++;
            }

            datosSesiones.sesiones = sesiones;
            datosSesiones.Usuarios = Usuarios;

            return Json(datosSesiones, JsonRequestBehavior.AllowGet);
        }
        public class DatosSesiones
        {
            public int[] sesiones { get; set; }
            public int[] Usuarios { get; set; }

        }
        public ActionResult ValidarNotificacion(int Id)
        {
            Notificacion notificacion = new Notificacion();
            if (Id != 0)
            {
                notificacion = (from x in db.notificaciones
                                where x.NotificacionId == Id
                                select x).FirstOrDefault();
                notificacion.Activa = false;
                db.SaveChanges();

            }

            if (notificacion != null)
            {

                if (notificacion.RedireccionUrl.Contains("/"))
                {
                    var urlfix = notificacion.RedireccionUrl.Split('/');

                    return RedirectToAction(urlfix[2], urlfix[1]);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }



        public ActionResult ImprimirCarnet(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Usuario = (from x in db.Users
                           where x.Id == Id
                           select x).FirstOrDefault();

            if (Usuario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            //ViewBag.txtQRCode = txtQRCode;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Usuario.Id, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //imgBarCode.Height = 150;
            //imgBarCode.Width = 150;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                    //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    List<DatosCarnet> carnet2 = new List<DatosCarnet>();


                    DatosCarnet Carnet = new DatosCarnet();
                    Carnet.QR = ms.ToArray();
                    Carnet.Nombre = Usuario.Nombre;
                    Carnet.Apellido = Usuario.Apellido;
                    carnet2.Add(Carnet);
                    ReportDocument rd = new ReportDocument();

                    rd.Load(Path.Combine(Server.MapPath("~/Reportes/CarnetV01.rpt")));
                    rd.SetDataSource(carnet2);
                    //rd.SetParameterValue("PathImagen", ms.ToString());

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();


                    rd.PrintToPrinter(1, true, 0, 0);

                    //Stream str = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                    //str.Seek(0, SeekOrigin.Begin);
                    //string name = String.Format("imp {0}", DateTime.Now);


                    return View();


                }
            }
        }


        public ActionResult ObtenerCarnet(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Usuario = (from x in db.Users
                           where x.Id == Id
                           select x).FirstOrDefault();

            if (Usuario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            //ViewBag.txtQRCode = txtQRCode;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Usuario.Id, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //imgBarCode.Height = 150;
            //imgBarCode.Width = 150;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                    //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    List<DatosCarnet> carnet2 = new List<DatosCarnet>();


                    DatosCarnet Carnet = new DatosCarnet();
                    Carnet.QR = ms.ToArray();
                    Carnet.Nombre = Usuario.Nombre;
                    Carnet.Apellido = Usuario.Apellido;
                    carnet2.Add(Carnet);
                    ReportDocument rd = new ReportDocument();

                    rd.Load(Path.Combine(Server.MapPath("~/Reportes/CarnetV01.rpt")));
                    rd.SetDataSource(carnet2);
                    //rd.SetParameterValue("PathImagen", ms.ToString());

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();


                    //rd.PrintToPrinter(1, true, 0, 0);

                    Stream str = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                    str.Seek(0, SeekOrigin.Begin);
                    string name = String.Format("imp {0}", DateTime.Now);


                    return File(str, "application/pdf", name);


                }
            }
        }
    }
}