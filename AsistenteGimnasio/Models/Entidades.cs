using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsistenteGimnasio.Models
{
    public class Ejercicio
    {
        public int EjercicioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
        public string EnlaceVideo { get; set; }
        public string Imagen { get; set; }
        public string Duracion { get; set; }
        public int Calorias { get; set; }
        public bool hidden { get; set; }
        
    }
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
    }
    public class Salud
    {
        public int SaludId { get; set; }
        public decimal Peso { get; set; }
        public decimal Estatura { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string FechadeRegistro { get; set; }
        public decimal IMC { get; set; }


    }
    public class IMC
    {
        public int IMCId { get; set; }
        public decimal Maximo { get; set; }
        public decimal Minimo { get; set; }

    }
    public class RegistroEjercicio
    {
        public int RegistroEjercicioId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Ejercicio Ejercicio { get; set; }
        public int EjercicioId { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Tiempo { get; set; }
        public string Fecha { get; set; }

    }
    public class Receta
    {
        public int RecetaId { get; set; }
        public string Nombre { get; set; }
        public TipoReceta TipoReceta { get; set; }
        public int TipoRecetaId { get; set; }
        public TiempoComida TiempoComida { get; set; }
        public int TiempoComidaId { get; set; }
        public string Imagen { get; set; }

        public string Procedimiento { get; set; }
        public string Ingredientes { get; set; }

        public string Calorias { get; set; }
        public string HidratosDeCrabono { get; set; }
        public string Proteinas { get; set; }
        public string grasa { get; set; }


    }
    public class TipoReceta
    {
        public int TipoRecetaId { get; set; }
        public string Tipo { get; set; }
        
    }
    public class TiempoComida
    {
        public int TiempoComidaId { get; set; }
        public string Tiempo { get; set; }
    }
    public class Sesiones
    {
        public int SesionesId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
    }
    public class Settings
    {
        public int SettingsId { get; set; }
        public string RutaImagenes { get; set; }
        public string RutaVideos { get; set; }
    }

    public class Notificacion
    {
        public int NotificacionId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Fecha { get; set; }
        public bool Activa { get; set; }
        public string Titulo { get; set; }
        public string RedireccionUrl { get; set; }


    }
}