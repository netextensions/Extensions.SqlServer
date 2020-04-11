using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetExtensions
{
    public static class SqlServerExtension
    {
        public static IServiceCollection AddSqlServerDb<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext, new()
        {
            var options = new DbContextOptionsBuilder<TContext>()
                .UseSqlServer(connectionString)
                .Options;
            using var context = CreateContext(options);
            context.Database.Migrate();

            services.AddSingleton(sp => options);
            return services;
        }

        private static TContext CreateContext<TContext>(DbContextOptions<TContext> options) where TContext : DbContext, new() => (TContext)Activator.CreateInstance(typeof(TContext), options);

    }
}
