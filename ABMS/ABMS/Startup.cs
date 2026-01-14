using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ABMS.Startup))]
namespace ABMS
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
