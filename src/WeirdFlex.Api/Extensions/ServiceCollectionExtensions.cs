using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WeirdFlex.Common.Interfaces;

namespace WeirdFlex.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFlexAuthentication(this IServiceCollection services, IServiceIdentity serviceIdentity)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Add("sub", ClaimTypes.NameIdentifier);
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                config.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        context.Response.Headers["Location"] = context.RedirectUri;
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }
                };
            })
            .AddOpenIdConnect(options =>
            {
                options.ClientId = serviceIdentity.ClientId;
                options.Authority = serviceIdentity.IdentityProvider + "/v2.0";
                options.ResponseType = "id_token";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddTransient<IPostConfigureOptions<OpenIdConnectOptions>, OpenIdConnectPostConfiguration>();
        }
    }

    public sealed class OpenIdConnectPostConfiguration : IPostConfigureOptions<OpenIdConnectOptions>
    {
        readonly IServiceScopeFactory _scopeFactory;
        readonly IConfiguration _configuration;

        public OpenIdConnectPostConfiguration(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        public void PostConfigure(string name, OpenIdConnectOptions options)
        {
            _configuration.GetSection(OpenIdConnectDefaults.AuthenticationScheme)
                .Bind(options);

            options.TokenValidationParameters.ValidateIssuer = false;

            // Defines whether access and refresh tokens should be stored in the 
            // Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties 
            // after a successful authorization.
            options.Events = new OpenIdConnectEvents
            {
                OnTokenValidated = async (ctx) =>
                {
                    using var scope = _scopeFactory.CreateScope();
                }
            };
        }
    }
}