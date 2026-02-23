using System.Security.Claims;

namespace OutsourceTracker.Authorization;

public static class AuthorizationExtensions
{
    public static bool IsInRole(this ClaimsPrincipal claim, ApplicationRole role) => claim.IsInRole(role.Permission);

    public static IEnumerable<ApplicationRole> GetUserRoles(this ClaimsPrincipal claim) => ApplicationRole.GetUserRoles(claim);
}
