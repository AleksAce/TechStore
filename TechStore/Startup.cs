using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechStore.Startup))]
namespace TechStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}