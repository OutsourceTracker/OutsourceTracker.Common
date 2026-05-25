using System.Security.Claims;

namespace OutsourceTracker.Authentication;

public interface ITokenService
{
    Task SetTokenAsync(string token, string? storeKey = null);

    Task<string> GetTokenAsync(string? storeKey = null);

    Task ClearTokenAsync(string? storeKey = null);
    
    bool IsTokenExpired(string token);

    Task<ClaimsPrincipal?> ValidateTokenAsync(string token);

    IEnumerable<Claim> GetClaims(string token);
}
