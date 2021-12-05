using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using AsistenteGimnasio.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using AsistenteGimnasio.Metodos;

namespace AsistenteGimnasio
{
    public class EnergymHub : Hub
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //------------Notificaciones---------------------

        public void notifi(string Titulo, string message)
        {

            Clients.All.notifiSend(Titulo, message);


        }

        //--------------------------------------------------
        //-----------------Redirigir------------------------
        public void Redirigir(string Direccion)
        {

            Clients.All.Redireccion(Direccion);


        }
        //--------------------------------------------------
       //---------Usuarios Activos--------------------------
        public static int contador = 0;
       
        
        public static ApplicationUser user = new ApplicationUser();
        public static List<ApplicationUser> usersActivos = new List<ApplicationUser>();
        public static List<ApplicationUser> usersActivosVerificacion = new List<ApplicationUser>();

        //public List<user> users = new List<user>();
        string userId = ""; 

        //public static string userIdActual = HttpContext.Current.User.Identity.GetUserId().ToString();
        public void ObtenerUser(string Id)
        {
            //string un = User.Identity.GetUserId();
            if (Id != userId)
            {
                userId = Id;
                user = (from x in db.Users
                        where x.Id == Id
                        select x).FirstOrDefault();
                user.Activo = true;
                db.SaveChanges();
                Clients.All.UsId(Id);
            }
       
        }
        public void ObtenerUsers(string Id)
        {
            user = (from x in db.Users
                    where x.Id == Id
                    select x).FirstOrDefault();
            user.Activo = true;
            db.SaveChanges();
            usersActivos.Add(user);

        }
        public void Desconectar(string Id)
        {
            if (Id != userId)
            {
                userId = Id;
                user = (from x in db.Users
                        where x.Id == Id
                        select x).FirstOrDefault();
                user.Activo = false;
                db.SaveChanges();
                //Clients.All.UsId(Id);
            }
        }


        //public override Task OnConnected()
        //{
        //    //var i = 
        //    contador++;
        
        //    Clients.All.usuariosActivos(contador);
        //    return base.OnConnected();
        //}


        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    //Desconectar(userId);
           
        //       user = (from x in db.Users
        //                where x.Id == userId
        //                select x).FirstOrDefault();
        //        user.Activo = false;
        //        db.SaveChanges();
        //        //Clients.All.UsId(Id);
            
        //    contador--;
        //    Clients.All.verificaractivos();
        //    Clients.All.usuariosActivos(/*userIdActual*/ contador);
        //    return base.OnDisconnected(stopCalled);
        //}
        public void ObtenerUsersver(string Id)
        {
            user = (from x in db.Users
                    where x.Id == Id
                    select x).FirstOrDefault();
            user.Activo = true;
            db.SaveChanges();
            usersActivosVerificacion.Add(user);

        }

        public void verificar(string Id)
        {
            var excepiones = usersActivos.Except(usersActivosVerificacion);
            foreach (var item in excepiones)
            {
                item.Activo = false;
                usersActivos.Remove(item);
                usersActivosVerificacion.Clear();
                
            }
            db.SaveChanges();
        }

        //--------------------------------------------------
    }
}