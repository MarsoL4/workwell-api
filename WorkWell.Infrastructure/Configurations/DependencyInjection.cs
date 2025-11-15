using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Configurations
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Usa Oracle. O pacote Oracle.EntityFrameworkCore deve estar instalado.
            services.AddDbContext<WorkWellDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString("OracleConnection")));

            return services;
        }
    }
}