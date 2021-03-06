﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Chirper.API.Infrastructure;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(Chirper.API.Startup))]
namespace Chirper.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            ConfigureOAuth(app);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            config.Formatters.JsonFormatter
            .SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            // Setup Authenication
            var authenticationOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(authenticationOptions);

            //Setup Authorization
            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,  /* In production this should be set to false */
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new ChirperAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(authorizationOptions);
        }
    }
}