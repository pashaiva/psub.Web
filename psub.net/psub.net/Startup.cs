using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(psub.net.Startup))]
namespace psub.net
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
