using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace WorkWell.API.Filters
{
    /// <summary>
    /// Adiciona 401 automaticamente em todos os endpoints protegidos por Authorize/ApiKeyAuthorize.
    /// </summary>
    public class UnauthorizedResponseOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Busca atributos em controller e método
            var hasAuthorize = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                .OfType<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>().Any() == true
                || context.MethodInfo.GetCustomAttributes(true)
                .OfType<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>().Any();

            // Inclui também os endpoints com [ApiKeyAuthorize]
            var hasApiKeyAuthorize = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                .Any(attr => attr.GetType().Name == "ApiKeyAuthorizeAttribute") == true
                || context.MethodInfo.GetCustomAttributes(true)
                .Any(attr => attr.GetType().Name == "ApiKeyAuthorizeAttribute");

            if (hasAuthorize || hasApiKeyAuthorize)
            {
                // Adiciona resposta 401 (se já não tiver)
                if (!operation.Responses.ContainsKey("401"))
                {
                    operation.Responses.Add("401", new OpenApiResponse
                    {
                        Description = "Não autorizado - forneça um header X-API-KEY válido ou permissão adequada"
                    });
                }
            }
        }
    }
}