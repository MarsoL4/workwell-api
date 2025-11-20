using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace WorkWell.API.Security
{
    public class ApiKeyAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiKeyAuthorizeAttribute(params string[] roles)
        {
            if (roles is { Length: > 0 })
            {
                Policy = string.Join(",", roles.Select(r => $"ApiKey_{r}"));
            }
            else
            {
                Policy = "ApiKey";
            }
        }
    }
}