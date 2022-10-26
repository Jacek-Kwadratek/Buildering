using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Buildering.Startup))]
namespace Buildering
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
