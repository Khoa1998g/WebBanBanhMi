using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanBanhMi.Startup))]
namespace WebBanBanhMi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
