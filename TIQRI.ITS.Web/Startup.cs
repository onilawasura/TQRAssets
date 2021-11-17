using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TIQRI.ITS.Web.Startup))]

namespace TIQRI.ITS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
