using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XKCDSubstitutions.Startup))]
namespace XKCDSubstitutions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
