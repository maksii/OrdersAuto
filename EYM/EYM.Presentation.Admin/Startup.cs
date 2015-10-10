using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EYM.Presentation.Admin.Startup))]
namespace EYM.Presentation.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
