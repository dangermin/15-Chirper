using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Chirper.API.Infrastructure
{
    public class ChirperAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // 1. Allow cors
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            // Create Cors user
            using (var authRepository = new AuthRepository())
            {
                var user = await authRepository.FindUser(
                    context.UserName,
                    context.Password
                );

                if (user == null)
                {
                    context.SetError("invalid_grant", "Username or password is incorrect");

                    return;
                }
                else
                {
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                   {
                       {
                           "username", user.ChirpName
                       }

                });
                    var token = new ClaimsIdentity(context.Options.AuthenticationType);
                    token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    token.AddClaim(new Claim(ClaimTypes.Role, "user"));
                    // Adds Auth Ticket for user
                    var ticket = new AuthenticationTicket(token, props);
                    context.Validated(ticket);
                }
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}