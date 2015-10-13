using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EYM.Presentation.Public.Startup))]
namespace EYM.Presentation.Public
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
