using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Phonebook.Web.Startup))]

namespace Phonebook.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}