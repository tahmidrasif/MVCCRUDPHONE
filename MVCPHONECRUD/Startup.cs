using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCPHONECRUD.Startup))]
namespace MVCPHONECRUD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
