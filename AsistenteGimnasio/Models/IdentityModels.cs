using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AsistenteGimnasio.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;

        }
        public string Fecha_Registro { get; set; }
        public string Nombre { get; set; }
        public  string Apellido { get; set; }
        public string Sexo { get; set; }
        public string Fecha_de_Nacimiento { get; set; }
        public string Direccion { get; set; }
        public string pss2 { get; set; }
        public bool Activo { get; set; }

 

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AsistGimBD", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<RegistroEjercicio> RegistroEjercicios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Sesiones> Sesiones { get; set; }
        public DbSet<Salud> Saluds { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Notificacion> notificaciones { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<TipoReceta> TipoRecetas { get; set; }
        public DbSet<TiempoComida> tiempoComidas { get; set; }
        public DbSet<IMC> IMCs { get; set; }
        public DbSet<ApplicationUserRol> applicationUserRols { get; set; }    

        //public System.Data.Entity.DbSet<AsistenteGimnasio.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<AsistenteGimnasio.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<AsistenteGimnasio.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}