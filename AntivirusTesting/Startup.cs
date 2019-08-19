using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AntivirusTesting.Startup))]
namespace AntivirusTesting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
