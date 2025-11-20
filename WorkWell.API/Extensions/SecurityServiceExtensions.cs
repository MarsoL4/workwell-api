using Microsoft.AspNetCore.Authorization;
using WorkWell.API.Security;

namespace WorkWell.API.Extensions
{
    public static class SecurityServiceExtensions
    {
        public static IServiceCollection AddApiKeySecurity(this IServiceCollection services, IConfiguration config)
        {
            // Registra opções a partir do appsettings.json -> "ApiKeys" e "SuperApiKey"
            var apiKeysSection = config.GetSection("ApiKeys");
            var superApiKey = config.GetValue<string>("SuperApiKey");

            var keyDict = new Dictionary<string, string>
            {
                { "Admin", apiKeysSection.GetValue<string>("Admin") ?? "admin-key" },
                { "RH", apiKeysSection.GetValue<string>("RH") ?? "rh-key" },
                { "Psicologo", apiKeysSection.GetValue<string>("Psicologo") ?? "psicologo-key" },
                { "Funcionario", apiKeysSection.GetValue<string>("Funcionario") ?? "funcionario-key" },
            };

            services.Configure<ApiKeyOptions>(opts =>
            {
                opts.Keys = keyDict;
                opts.SuperKey = superApiKey ?? "super-key";
            });

            services.AddSingleton<IAuthorizationHandler, ApiKeyHandler>();

            // Políticas básicas (isoladas)
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiKey", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement());
                });

                foreach (var role in keyDict.Keys)
                {
                    options.AddPolicy($"ApiKey_{role}", policy =>
                    {
                        policy.Requirements.Add(new ApiKeyRequirement(role));
                    });
                }

                // Adicione explicitamente todas as combinações utilizadas nos controllers
                options.AddPolicy("ApiKey_Funcionario_RH_Admin", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Funcionario", "RH", "Admin"));
                });

                options.AddPolicy("ApiKey_Admin_RH", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Admin", "RH"));
                });

                options.AddPolicy("ApiKey_Funcionario_Psicologo_RH_Admin", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Funcionario", "Psicologo", "RH", "Admin"));
                });

                options.AddPolicy("ApiKey_Funcionario_Psicologo_RH", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Funcionario", "Psicologo", "RH"));
                });

                options.AddPolicy("ApiKey_Funcionario_Psicologo_RH_Admin", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Funcionario", "Psicologo", "RH", "Admin"));
                });

                options.AddPolicy("ApiKey_Admin_RH_Funcionario", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Admin", "RH", "Funcionario"));
                });

                // Se necessário, adicione outras combinações encontradas nos controllers.
            });

            return services;
        }
    }
}