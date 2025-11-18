using Microsoft.OpenApi.Models;

namespace WorkWell.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddWorkWellSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WorkWell API",
                    Version = "v1",
                    Description = "API para gestão de bem-estar corporativo e apoio psicológico.",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe WorkWell",
                    }
                });

                // Adiciona parametrização para API Key
                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Chave de API para autenticação. Use o valor para o header: X-API-KEY",
                    Name = "X-API-KEY",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKeyScheme"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new string[] {}
                    }
                });

                // Garante análise de comentários XML para summaries (ajustar nome do XML conforme csproj)
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "WorkWell.API.xml");
                if (File.Exists(xmlPath))
                    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });

            return services;
        }
    }
}