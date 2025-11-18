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
                // Roles + suas chaves
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

            // Policy para cada role
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
            });

            return services;
        }
    }
}