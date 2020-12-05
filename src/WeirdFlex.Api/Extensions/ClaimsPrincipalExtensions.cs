
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetUserUId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.TryResolveNameClaim(ClaimTypes.NameIdentifier);
        }

        public static string? GetDisplayName(this ClaimsPrincipal claimsPrincipal)
        {
            var surnameClaim = claimsPrincipal.FindFirst(ClaimTypes.Surname);
            var givennameClaim = claimsPrincipal.FindFirst(ClaimTypes.GivenName);

            if (surnameClaim != null && givennameClaim != null)
            {
                return surnameClaim.Value + " " + givennameClaim.Value;
            }

            return claimsPrincipal.TryResolveNameClaim(ClaimTypes.Name, "name", "username");
        }

        public static string? GetAccountName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.TryResolveNameClaim(ClaimTypes.WindowsAccountName, "preferred_username", ClaimTypes.Name, "name", "email");
        }

        private static string? TryResolveNameClaim(this ClaimsPrincipal claimsPrincipal, params string[] keys)
        {
            string? name = null;
            foreach (var key in keys)
            {
                name = claimsPrincipal.FindFirst(key)?.Value;
                if (!string.IsNullOrEmpty(name))
                    break;
            }
            return name;
        }

        public static bool IsServiceUser(this ClaimsPrincipal claimsPrincipal)
        {
            return !claimsPrincipal.HasClaim(x => x.Type == ClaimTypes.NameIdentifier) && claimsPrincipal.HasClaim(x => x.Type == "client_id");
        }
    }
}