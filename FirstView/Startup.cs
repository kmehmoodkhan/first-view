using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstView.Startup))]
namespace FirstView
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
