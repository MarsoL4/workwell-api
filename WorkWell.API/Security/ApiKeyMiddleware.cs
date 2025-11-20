using Microsoft.Extensions.Options;

namespace WorkWell.API.Security
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiKeyOptions _options;

        public ApiKeyMiddleware(RequestDelegate next, IOptions<ApiKeyOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api"))
            {
                // Não protege rotas não-API
                await _next(context);
                return;
            }

            string? apiKey = context.Request.Headers["X-API-KEY"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"statusCode\":401,\"message\":\"API Key is missing\"}");
                return;
            }

            if (apiKey == _options.SuperKey)
            {
                context.Items["ApiKeyRole"] = "Super";
            }
            else
            {
                var matched = _options.Keys.FirstOrDefault(kv => kv.Value == apiKey);
                if (!string.IsNullOrEmpty(matched.Key))
                {
                    context.Items["ApiKeyRole"] = matched.Key;
                }
                else
                {
                    context.Response.StatusCode = 403; // Forbidden
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"statusCode\":403,\"message\":\"Invalid API Key\"}");
                    return;
                }
            }

            await _next(context);
        }
    }
}