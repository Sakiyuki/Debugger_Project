using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Debugger_Project.Startup))]
namespace Debugger_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
