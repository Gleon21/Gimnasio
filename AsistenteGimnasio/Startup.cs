using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsistenteGimnasio.Startup))]
namespace AsistenteGimnasio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
