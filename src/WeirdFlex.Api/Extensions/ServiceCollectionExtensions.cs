﻿using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using WeirdFlex.Business.Notifications;
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

            //services.AddTransient<IPostConfigureOptions<OpenIdConnectOptions>, OpenIdConnectPostConfiguration>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(KnownPolicies.Flexer, new AuthorizationPolicyBuilder(OpenIdConnectDefaults.AuthenticationScheme)
                    .RequireAssertion(context => context.User.HasClaim(x => x.Value == "Flexer"))
                    .RequireAuthenticatedUser()
                    .Build());
            });
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
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var httpRequestScope = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
                    
                    var user = httpRequestScope.HttpContext?.User;
                    if (user == null)
                        return;

                    var displayName = user.GetDisplayName();
                    if (displayName == null)
                        return;

                    var uid = user.GetUserUId();
                    if (uid == null)
                        return;

                    await mediator.Publish(new UserAuthenticatedNotification(displayName, uid));
                }
            };
        }
    }
}