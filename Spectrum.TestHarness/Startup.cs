using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Spectrum.TestHarness.Startup))]
namespace Spectrum.TestHarness
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
