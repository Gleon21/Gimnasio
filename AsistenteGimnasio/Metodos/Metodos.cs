using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using AsistenteGimnasio.Models;
using System.Security.Claims;

namespace AsistenteGimnasio.Metodos
{
    public class Metodos
    {
        public decimal ObtenerHoras(string Tiempo)
        {
            var tiempo = Tiempo.Split(':');
            decimal horas = Convert.ToDecimal(tiempo[0]);
            decimal minutos = Convert.ToDecimal(tiempo[1]) / 60;
            decimal segundos =Convert.ToDecimal(tiempo[2]) * Convert.ToDecimal(0.000277778);

            return horas+minutos+segundos; 
        }
      
    }
 
}