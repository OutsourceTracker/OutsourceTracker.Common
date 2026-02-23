using System.Security.Claims;
using System.Text.Json;

namespace OutsourceTracker.Authorization;

public static class AuthorizationExtensions
{
    public static bool IsInRole(this ClaimsPrincipal principal, ApplicationRole role) => principal.HasRole(role.Permission);

    public static bool HasRole(this ClaimsPrincipal principal, string role) => principal.HasClaim(ApplicationRole.CLAIM_TYPE, value => string.Equals(value, role, StringComparison.OrdinalIgnoreCase));

    public static IEnumerable<ApplicationRole> GetUserRoles(this ClaimsPrincipal principal)
    {
        if (principal?.Identity?.IsAuthenticated != true)
            return Enumerable.Empty<ApplicationRole>();

        return ApplicationRole.Roles()
            .Where(principal.IsInRole);
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

        return principal.HasClaim(ApplicationRole.CLAIM_TYPE,
            value => value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }
}
