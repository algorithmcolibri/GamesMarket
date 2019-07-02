using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GamesMarket.Startup))]
namespace GamesMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
