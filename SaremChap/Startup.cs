using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaremChap.Startup))]
namespace SaremChap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
