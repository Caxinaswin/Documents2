using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientMvcProdutos.Startup))]
namespace ClientMvcProdutos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
