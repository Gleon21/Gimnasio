using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsistenteGimnasio.Models
{
    
        public class BusquedaR
        {
            public string BusquedarId { get; set; }
            public string busqueda { get; set; }
        }

        public class DatosCarnet
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public byte[] QR { get; set; }
            
        }
    
}