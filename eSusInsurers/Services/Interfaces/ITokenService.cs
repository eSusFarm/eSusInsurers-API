using System.Security.Claims;
using eSusInsurers.Models.Users.RefreshAccessToken;

namespace eSusInsurers.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task<object> RefreshToken(TokenApiModel tokenApiModel, CancellationToken cancellationToken = default);
    Task<bool> RevokeToken(string userName, CancellationToken cancellationToken = default);
}