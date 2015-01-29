using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImageEncryptionWebSample.Startup))]
namespace ImageEncryptionWebSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
