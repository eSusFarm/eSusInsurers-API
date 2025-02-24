using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using eSusInsurers.Controllers;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.RefreshAccessToken;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Controllers
{
    public class TokenControllerTests
    {
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly TokenController _controller;

        public TokenControllerTests()
        {
            _mockTokenService = new Mock<ITokenService>();
            _controller = new TokenController(_mockTokenService.Object);
        }

        [Fact]
        public async Task Refresh_ValidToken_ReturnsOkResult()
        {
            // Arrange
            var tokenApiModel = new TokenApiModel { AccessToken = "validAccessToken", RefreshToken = "validRefreshToken" };
            var expectedResponse = new AuthenticatedResponse { Token = "newAccessToken", RefreshToken = "newRefreshToken" };
            _mockTokenService.Setup(s => s.RefreshToken(tokenApiModel, It.IsAny<CancellationToken>()))
                              .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Refresh(tokenApiModel);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
            _mockTokenService.Verify(s => s.RefreshToken(tokenApiModel, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Refresh_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var tokenApiModel = new TokenApiModel { AccessToken = "invalidAccessToken", RefreshToken = "invalidRefreshToken" };
            _mockTokenService.Setup(s => s.RefreshToken(tokenApiModel, It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new Exception("Invalid token"));

            // Act
            var result = await _controller.Refresh(tokenApiModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid token", (badRequestResult.Value as dynamic).ErrorMessage);
        }

        [Fact]
        public async Task Revoke_ValidToken_ReturnsNoContent()
        {
            // Arrange
            var username = "testUser";
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            _mockTokenService.Setup(s => s.RevokeToken(username, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Revoke();

            // Assert
            var noContentResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
            _mockTokenService.Verify(s => s.RevokeToken(username, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Revoke_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var username = "testUser";
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            _mockTokenService.Setup(s => s.RevokeToken(username, It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new Exception("Revoke failed"));

            // Act
            var result = await _controller.Revoke();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Revoke failed", (badRequestResult.Value as dynamic).ErrorMessage);
        }
    }
}

