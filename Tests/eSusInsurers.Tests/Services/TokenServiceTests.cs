using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.RefreshAccessToken;
using eSusInsurers.Services.Implementations;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Xunit;
using Claim = System.Security.Claims.Claim;

namespace eSusInsurers.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly IUnitOfWork _fakeUnitOfWork;
        private readonly IConfiguration _fakeConfiguration;
        private readonly TokenService _tokenService;
        private readonly IDbContextTransaction _fakeTransaction;
        private const string SecretKey = "your-256-bit-secret-key-your-256-bit-secret-key-your-256-bit-secret-key-your-256-bit-secret";

        public TokenServiceTests()
        {
            _fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _fakeConfiguration = A.Fake<IConfiguration>();
            _fakeTransaction = A.Fake<IDbContextTransaction>();
            _tokenService = new TokenService(_fakeUnitOfWork, _fakeConfiguration);

            // Setup basic configuration with a properly sized secret key
            A.CallTo(() => _fakeConfiguration["JWT:Secret"]).Returns(SecretKey);
            A.CallTo(() => _fakeConfiguration["JWT:TokenValidityInMinutes"]).Returns("60");
            A.CallTo(() => _fakeConfiguration["JWT:RefreshTokenValidityInDays"]).Returns("7");
            A.CallTo(() => _fakeConfiguration["JWT:ValidAudience"]).Returns("test-audience");
            A.CallTo(() => _fakeConfiguration["JWT:ValidIssuer"]).Returns("test-issuer");

            A.CallTo(() => _fakeUnitOfWork.BeginTransactionAsync(A<CancellationToken>._))
                .Returns(_fakeTransaction);
        }

        [Fact]
        public void GenerateAccessToken_WithValidClaims_ReturnsValidToken()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "test@example.com"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            // Act
            var token = _tokenService.GenerateAccessToken(claims);

            // Assert
            token.Should().NotBeNullOrEmpty();
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);
            decodedToken.Claims.Should().Contain(c => c.Value == "test@example.com");
            decodedToken.Claims.Should().Contain(c => c.Value == "Admin");
        }

        [Fact]
        public void GenerateRefreshToken_ReturnsValidToken()
        {
            // Act
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Assert
            refreshToken.Should().NotBeNullOrEmpty();
            refreshToken.Length.Should().BeGreaterThan(32); // Base64 encoded 32 bytes
        }

        [Fact]
        public void GetPrincipalFromExpiredToken_WithValidToken_ReturnsPrincipal()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "test@example.com")
            };
            var token = _tokenService.GenerateAccessToken(claims);

            // Act
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);

            // Assert
            principal.Should().NotBeNull();
            principal.Identity.Name.Should().Be("test@example.com");
        }

        [Fact]
        public async Task RefreshToken_WithValidTokens_ReturnsNewTokens()
        {
            // Arrange
            var user = new User 
            { 
                EmailId = "test@example.com",
                RefreshToken = "valid-refresh-token",
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
            };

            var tokenApiModel = new TokenApiModel
            {
                AccessToken = _tokenService.GenerateAccessToken(new[] { new Claim(ClaimTypes.Name, user.EmailId) }),
                RefreshToken = user.RefreshToken
            };

            A.CallTo(() => _fakeUnitOfWork.UserRepository.GetByEmailIdAsync(user.EmailId, A<CancellationToken>._))
                .Returns(user);

            // Act
            var result = await _tokenService.RefreshToken(tokenApiModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AuthenticatedResponse>();
            var response = result as AuthenticatedResponse;
            response.Token.Should().NotBeNullOrEmpty();
            response.RefreshToken.Should().NotBeNullOrEmpty();
            
            A.CallTo(() => _fakeTransaction.CommitAsync(A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RefreshToken_WithExpiredRefreshToken_ThrowsBadRequestException()
        {
            // Arrange
            var user = new User 
            { 
                EmailId = "test@example.com",
                RefreshToken = "valid-refresh-token",
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(-1) // Expired
            };

            var tokenApiModel = new TokenApiModel
            {
                AccessToken = _tokenService.GenerateAccessToken(new[] { new Claim(ClaimTypes.Name, user.EmailId) }),
                RefreshToken = user.RefreshToken
            };

            A.CallTo(() => _fakeUnitOfWork.UserRepository.GetByEmailIdAsync(user.EmailId, A<CancellationToken>._))
                .Returns(user);

            // Act & Assert
            await _tokenService.Invoking(x => x.RefreshToken(tokenApiModel))
                .Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public async Task RevokeToken_WithValidUser_RevokesToken()
        {
            // Arrange
            var user = new User { EmailId = "test@example.com", RefreshToken = "token" };
            A.CallTo(() => _fakeUnitOfWork.UserRepository.GetByEmailIdAsync(user.EmailId, A<CancellationToken>._))
                .Returns(user);

            // Act
            var result = await _tokenService.RevokeToken(user.EmailId);

            // Assert
            result.Should().BeTrue();
            user.RefreshToken.Should().BeNull();
            A.CallTo(() => _fakeTransaction.CommitAsync(A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RevokeToken_WithInvalidUser_ThrowsBadRequestException()
        {
            // Arrange
            A.CallTo(() => _fakeUnitOfWork.UserRepository.GetByEmailIdAsync(A<string>._, A<CancellationToken>._))
                .Returns((User)null);

            // Act & Assert
            await _tokenService.Invoking(x => x.RevokeToken("invalid@example.com"))
                .Should().ThrowAsync<BadRequestException>();
        }
    }
}