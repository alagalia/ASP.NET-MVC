using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventsApp.Startup))]
namespace EventsApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
