using Microsoft.Owin;

[assembly: OwinStartup(typeof(SocialUnion.Web.Startup))]

namespace  SocialUnion.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Cors;
    using System.Web.Http;

    using Microsoft.Owin;
    using Microsoft.Owin.Cors;

    using Owin;
    using TripExchange.Web.Infrastructure;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(new CorsOptions()
            {
                PolicyProvider = new CorsPolicyProvider()
                {
                    PolicyResolver = request =>
                    {
                        if (request.Path.StartsWithSegments(new PathString(TokenEndpointPath)))
                        {
                            return Task.FromResult(new CorsPolicy { AllowAnyOrigin = true });
                        }

                        return Task.FromResult<CorsPolicy>(null);
                    }
                }
            });

            ConfigureAuth(app);

            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            //Rest of code is removed for brevity

            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            //Rest of code is removed for brevity

        }
    }
}
