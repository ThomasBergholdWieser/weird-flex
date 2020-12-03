using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WeirdFlex.Data.EF
{
    public class DesignTimeFlexContextFactory : IDesignTimeDbContextFactory<FlexContext>
    {
        /// <summary>
        /// Creates a new instance of a pas database context used for local development.
        ///
        /// This context will be used for "dotnet ef" commands.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>An instance of <see cref="PasDbContext" />.</returns>
        public FlexContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FlexContext>()
                .UseMySql("Server=localhost;Database=flexDb;User=root;Password=f2S-HtaPD_Lx;")
                .EnableSensitiveDataLogging();

            return new FlexContext(optionsBuilder.Options);
        }
    }
}
