using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DecidirExample.Startup))]
namespace DecidirExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
