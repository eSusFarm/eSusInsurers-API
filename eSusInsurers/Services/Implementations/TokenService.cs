using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.RefreshAccessToken;
using eSusInsurers.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace eSusInsurers.Services.Implementations;

public class TokenService : ITokenService
{
    #region Constructor

    public TokenService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    #endregion

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out var tokenValidityInMinutes);

        var tokeOptions = new JwtSecurityToken(
            _configuration["JWT:ValidAudience"],
            _configuration["JWT:ValidIssuer"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
            ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public async Task<object> RefreshToken(TokenApiModel tokenApiModel, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(tokenApiModel, nameof(tokenApiModel));

        ArgumentNullException.ThrowIfNull(tokenApiModel.RefreshToken, nameof(tokenApiModel.RefreshToken));

        ArgumentNullException.ThrowIfNull(tokenApiModel.AccessToken, nameof(tokenApiModel.AccessToken));

        var accessToken = tokenApiModel.AccessToken;

        var refreshToken = tokenApiModel.RefreshToken;

        var principal = GetPrincipalFromExpiredToken(accessToken);

        var userName = principal.Identity.Name; //this is mapped to the Name claim by default

        var user = await _unitOfWork.UserRepository.GetByEmailIdAsync(userName, cancellationToken);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new BadRequestException("Invalid client request");

        var newAccessToken = GenerateAccessToken(principal.Claims);

        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out var refreshTokenValidityInDays);

        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);

        var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }

        return new AuthenticatedResponse
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public async Task<bool> RevokeToken(string userName, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailIdAsync(userName, cancellationToken);

        if (user is null)
            throw new BadRequestException("Invalid user.");

        user.RefreshToken = null;

        var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }

        return true;
    }

    #region Fields

    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    #endregion
}