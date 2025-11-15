using Microsoft.Extensions.DependencyInjection;
using WorkWell.Application.Services.EmpresaOrganizacao;

namespace WorkWell.Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEmpresaService, EmpresaService>();
            return services;
        }
    }
}