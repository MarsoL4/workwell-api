using Microsoft.AspNetCore.Authorization;

namespace WorkWell.API.Security
{
    public class ApiKeyAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiKeyAuthorizeAttribute(params string[] roles)
        {
            if (roles is { Length: > 0 })
            {
                Policy = $"ApiKey_{string.Join("_", roles)}"; // UNDERSCORE!
            }
            else
            {
                Policy = "ApiKey";
            }
        }
    }
}