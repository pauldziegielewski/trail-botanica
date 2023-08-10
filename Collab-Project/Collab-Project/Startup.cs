using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Collab_Project.Startup))]
namespace Collab_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
