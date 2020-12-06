using NSwag;
using NSwag.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeirdFlex.Common.Interfaces;

namespace WeirdFlex.Api.Extensions
{
    public static class NSwagExtensions
    {
        public static void AddFlexAuthentication(this OpenApiDocument document, IServiceIdentity serviceIdentity)
        {
            document.Components.SecuritySchemes.Add("openid", new OpenApiSecurityScheme
            {
                // swagger-ui doesn't support native OpenIdConnect yet
                Type = OpenApiSecuritySchemeType.OAuth2,
                Description = "Portal Authentication",
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = serviceIdentity.IdentityProvider + "/oauth2/v2.0/authorize",
                        TokenUrl = serviceIdentity.IdentityProvider + "/oauth2/v2.0/token",
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID Connect" },
                            { "profile", "User Profile" },
                            { "email", "E-Mail" }
                        }
                    }
                }
            });
            document.Security.Add(new OpenApiSecurityRequirement
            {
                { "openid", Array.Empty<string>() }
            });
        }

        public static void AddFlexAuthentication(this SwaggerUi3Settings settings, IServiceIdentity serviceIdentity)
        {
            settings.OAuth2Client = new OAuth2ClientSettings
            {
                AppName = serviceIdentity.ClientId,
                ClientId = serviceIdentity.ClientId,
                ClientSecret = string.Empty,
                UsePkceWithAuthorizationCodeGrant = true
            };
        }
    }
}
