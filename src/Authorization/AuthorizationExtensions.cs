using System.Security.Claims;
using System.Text.Json;

namespace OutsourceTracker.Authorization;

public static class AuthorizationExtensions
{
    public static bool IsInRole(this ClaimsPrincipal principal, ApplicationRole role) => principal.IsInRole(role.Permission);

    public static IEnumerable<ApplicationRole> GetUserRoles(this ClaimsPrincipal principal)
    {
        if (principal?.Identity?.IsAuthenticated != true)
            return Enumerable.Empty<ApplicationRole>();

        return ApplicationRole.GetUserRoles(principal);
    }

    public static bool HasClaim(this ClaimsPrincipal principal, string claimType, Predicate<string> predicate)
    {
        ArgumentNullException.ThrowIfNull(principal);
        ArgumentNullException.ThrowIfNull(predicate);

        return principal.Claims
            .Where(c => string.Equals(c.Type, claimType, StringComparison.OrdinalIgnoreCase)
                     && !string.IsNullOrWhiteSpace(c.Value))
            .SelectMany(c =>
            {
                var value = c.Value!.Trim();

                if (value.StartsWith('[') && value.EndsWith(']'))
                {
                    try
                    {
                        var array = JsonSerializer.Deserialize<string[]>(value);
                        return array ?? Enumerable.Empty<string>();
                    }
                    catch (JsonException)
                    {
                        // Malformed JSON → treat as single value
                        return [value];
                    }
                }

                return [value];
            })
            .Any(c => predicate(c));
    }

    public static bool HasRolePrefix(this ClaimsPrincipal principal, string prefix)
    {
        if (string.IsNullOrWhiteSpace(prefix))
            return false;

        return principal.HasClaim(ClaimTypes.Role,
            value => value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }
}
