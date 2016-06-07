using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrowdTouring.Startup))]
namespace CrowdTouring
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
