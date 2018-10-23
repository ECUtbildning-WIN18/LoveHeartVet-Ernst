using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LH_Vet._0._1.Startup))]
namespace LH_Vet._0._1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
