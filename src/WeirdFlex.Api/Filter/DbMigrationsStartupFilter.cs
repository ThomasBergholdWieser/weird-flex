using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeirdFlex.Data.EF;

namespace WeirdFlex.Api.Filter
{
    /// <summary>
    /// DbMigrations Startup Filter.
    /// </summary>
    public class DbMigrationsStartupFilter : IStartupFilter
    {
        readonly ILogger _logger;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IWebHostEnvironment _env;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbMigrationsStartupFilter" /> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="serviceScopeFactory">IServiceScopeFactory.</param>
        /// <param name="env">IHostingEnvironment.</param>
        public DbMigrationsStartupFilter(ILogger<DbMigrationsStartupFilter> logger, IServiceScopeFactory serviceScopeFactory, IWebHostEnvironment env)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _env = env;
        }

        /// <summary>
        /// Configure.
        /// </summary>
        /// <param name="next">IApplicationBuilder.</param>
        /// <returns>Application Builder.</returns>
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            _logger.LogInformation("DbMigrationsStartupFilter for " + _env.EnvironmentName);

            using var scope = _serviceScopeFactory.CreateScope();
            using var context = scope.ServiceProvider
                .GetRequiredService<FlexContext>();
            context.Database.Migrate();

            _logger.LogInformation("Migrations processed.");

            return next;
        }
    }

}
