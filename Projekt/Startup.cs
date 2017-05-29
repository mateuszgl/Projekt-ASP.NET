using Microsoft.Owin;
using Owin;
using Projekt.DAL;

[assembly: OwinStartupAttribute(typeof(Projekt.Startup))]
namespace Projekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
               //ConfigureAuth(app);
        }
    }
}
