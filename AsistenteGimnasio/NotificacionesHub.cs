using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AsistenteGimnasio
{
    public class NotificacionesHub : Hub
    {
        public static int contador = 0;

        public void notifi(string Titulo, string message)
        {

            Clients.All.notifiSend(Titulo, message);


        }     

        public override Task OnConnected()
        {
            contador++;
            Clients.All.usuariosActivos(contador);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            contador--;
            Clients.All.usuariosActivos(contador);
            return base.OnDisconnected(stopCalled);
        }
    }
}