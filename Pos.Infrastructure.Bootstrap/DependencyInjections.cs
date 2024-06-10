using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pos.Application.Common.Interfaces;
using Pos.Infrastructure.Persistence.Sql.SQLContext;

namespace Pos.Infrastructure.Bootstrap
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            // Adding inMemory database support to use in unit testing
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<PosDbContext>(options =>
                    options.UseInMemoryDatabase("PosDB"));
            }
            else
            {
                services.AddDbContext<PosDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Pos"), o =>
                    {
                        o.EnableRetryOnFailure(3);
                        o.CommandTimeout(180);
                    });
                    //options.LogTo(Console.WriteLine);
                    options.EnableSensitiveDataLogging();
                });
            }

            services.AddTransient<System.Data.SqlClient.SqlConnection>(_ => new System.Data.SqlClient.SqlConnection(configuration.GetConnectionString("Pos")));
            services.AddScoped<IPosDbContext>(provider => provider.GetRequiredService<PosDbContext>());

            return services;
        }
    }
}
