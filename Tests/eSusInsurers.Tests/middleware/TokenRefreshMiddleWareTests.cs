using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Middleware;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Middleware
{
    public class TokenRefreshMiddlewareTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly TokenRefreshMiddleware _middleware;

        public TokenRefreshMiddlewareTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _middleware = new TokenRefreshMiddleware(_mockUserService.Object, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task InvokeAsync_WithValidTokenNeedingRefresh_ShouldRefreshToken()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var token = GenerateToken(DateTime.UtcNow.AddMinutes(30));
            context.Request.Headers["Authorization"] = $"Bearer {token}";

            // Act
            await _middleware.InvokeAsync(context, (innerHttpContext) => Task.CompletedTask);

            // Assert
            Assert.True(context.Response.Headers.ContainsKey("X-Token-Refreshed"));
            Assert.Equal("True", context.Response.Headers["X-Token-Refreshed"]);
        }

        [Fact]
        public async Task InvokeAsync_WithValidTokenNotNeedingRefresh_ShouldNotRefreshToken()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var token = GenerateToken(DateTime.UtcNow.AddHours(2));
            context.Request.Headers["Authorization"] = $"Bearer {token}";

            // Act
            await _middleware.InvokeAsync(context, (innerHttpContext) => Task.CompletedTask);

            // Assert
            Assert.False(context.Response.Headers.ContainsKey("X-Token-Refreshed"));
        }

        [Fact]
        public async Task InvokeAsync_WithExpiredToken_ShouldReturnUnauthorized()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var token = GenerateToken(DateTime.UtcNow.AddMinutes(-5));
            context.Request.Headers["Authorization"] = $"Bearer {token}";

            // Act
            await _middleware.InvokeAsync(context, (innerHttpContext) => Task.CompletedTask);

            // Assert
            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_WithNoToken_ShouldNotRefreshToken()
        {
            // Arrange
            var context = new DefaultHttpContext();

            // Act
            await _middleware.InvokeAsync(context, (innerHttpContext) => Task.CompletedTask);

            // Assert
            Assert.False(context.Response.Headers.ContainsKey("X-Token-Refreshed"));
        }

        private string GenerateToken(DateTime expirationTime)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new byte[32];
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", "1") }),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

