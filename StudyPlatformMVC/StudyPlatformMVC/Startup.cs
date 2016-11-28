using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudyPlatformMVC.Startup))]
namespace StudyPlatformMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
