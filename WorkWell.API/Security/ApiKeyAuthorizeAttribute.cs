using Microsoft.AspNetCore.Authorization;

namespace WorkWell.API.Security
{
    public class ApiKeyAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiKeyAuthorizeAttribute(params string[] roles)
        {
            Policy = roles is { Length: > 0 }
                ? $"ApiKey_{string.Join("_", roles)}"
                : "ApiKey";
        }
    }
}