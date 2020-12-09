using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeirdFlex.Api.Behaviours;
using WeirdFlex.Api.Extensions;
using WeirdFlex.Api.Filter;
using WeirdFlex.Business;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Common.Interfaces;
using WeirdFlex.Data.EF;

namespace WeirdFlex.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        static ServiceIdentity? ServiceIdentity;
        static ServiceIdentity GetServiceIdentity(IConfiguration configuration)
        {
            if (ServiceIdentity == null)
            {
                var optionsSection = configuration.GetSection(nameof(ServiceIdentity));
                var appOptions = new ServiceIdentity();
                optionsSection.Bind(appOptions);
                ServiceIdentity = appOptions;
            }

            return ServiceIdentity;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            var serviceIdentity = GetServiceIdentity(Configuration);
            services.AddSingleton<IServiceIdentity>(serviceIdentity);

            services.AddFlexAuthentication(serviceIdentity);
            services.AddScoped<IUserContext, UserContext>();

            // Policies
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    {
                        builder.WithOrigins(
                            "https://localhost:7501")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddMemoryCache();
            services.AddRouting();
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddRazorPages();

            // register infrastructure
            services.AddMediatR(typeof(WeirdFlexBusinessAssemblyMarker));
            services.AddAutoMapper(typeof(WeirdFlexBusinessAssemblyMarker));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // register business services
            services.AddScoped<IRequestDispatcher, RequestDispatcher>();

            // register entity framework
            services.AddDbContext<FlexContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString(nameof(FlexContext))));
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IStartupFilter, DbMigrationsStartupFilter>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeirdFlex.Api", Version = "v1" });
            });

            services.AddOpenApiDocument(
                config =>
                {
                    config.PostProcess = document =>
                    {
                        document.Info.Title = "Label Management API";
                        document.Info.Version = ApiVersion.InformationalVersion;
                        document.Info.Contact = new NSwag.OpenApiContact { Name = "Tieto Corporation", Email = string.Empty, Url = "https://www.tieto.com" };
                        document.Info.License = new NSwag.OpenApiLicense { Name = "Copyright (c) Tieto Corporation. All rights reserved", Url = "https://www.tieto.com" };
                        document.AddFlexAuthentication(serviceIdentity);
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseOpenApi();
            app.UseSwaggerUi3(c =>
            {
                c.EnableTryItOut = true;
                c.AddFlexAuthentication(GetServiceIdentity(Configuration));
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
