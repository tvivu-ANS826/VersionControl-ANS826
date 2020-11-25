using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Testes.StartupOwin))]

namespace Testes
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
