using Owin;
using System.Web.Http;
using Microsoft.Owin;
using Unity.WebApi;

[assembly: OwinStartup(typeof(PollService.Frontend.Admin.Startup))]

namespace PollService.Frontend.Admin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(config =>
            {
                config.DependencyResolver = new UnityDependencyResolver(UnityConfiguration.GetContainer());
                ApiConfiguration.Install(config, app);
            });
        }
    }
}
