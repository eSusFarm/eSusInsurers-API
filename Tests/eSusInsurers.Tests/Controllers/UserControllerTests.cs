using eSusInsurers.Controllers;
using eSusInsurers.Models;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.Users.ChangePassword;
using eSusInsurers.Models.Users.GetUsers;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.UpdateUser;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Controllers;

public class UserControllerTests
{
    private readonly UserController _controller;
    private readonly Mock<IUserService> _userService;

    public UserControllerTests()
    {
        _userService = new Mock<IUserService>();
        _controller = new UserController(_userService.Object);
    }

    [Fact]
    public async Task GetUsers_WhenSuccessful_ShouldReturnUsers()
    {
        // Arrange
        var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
        var expectedResult = new PagedResult<UserModel>
        {
            CurrentPage = 1,
            PageSize = 10,
            TotalPages = 1,
            TotalRecordCount = 1,
            Records = new List<UserModel> { new UserModel
            {
                UserId = 1,
                ContactNumber = 579686060,
            } }
        };
        _userService.Setup(s=>s.GetUsers(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);
        var result = await _controller.GetUsers(pagingOptions);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<PagedResult<UserModel>>(okResult.Value);
        Assert.Equal(expectedResult.CurrentPage, returnedResponse.CurrentPage);
        Assert.Equal(expectedResult.PageSize, returnedResponse.PageSize);
        Assert.Equal(expectedResult.TotalPages, returnedResponse.TotalPages);
        Assert.Equal(expectedResult.TotalRecordCount, returnedResponse.TotalRecordCount);
        Assert.Equal(expectedResult.Records.Count, returnedResponse.Records.Count);
        Assert.Equal(expectedResult.Records[0].UserId, returnedResponse.Records[0].UserId);
        Assert.Equal(expectedResult.Records[0].ContactNumber, returnedResponse.Records[0].ContactNumber);
    }

    [Fact]
    public async Task GetUsers_ServiceThrowsException_ShouldReturnBadRequest()
    {
        var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
        var expectedException = new Exception("Test error");
        _userService.Setup(s=>s.GetUsers(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = await _controller.GetUsers(pagingOptions);
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }

    [Fact]
    public async Task GetUserById_WhenSuccessful_ShouldReturnAUser()
    {
        var userId = 1;
        var expectedResult = new UserModel
        {
            UserId = userId,
            ContactNumber = 579686060,
            IsActive = true
        };
        _userService.Setup(s=>s.GetUserById(1, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);
        var actionResult = await _controller.GetUserById(userId);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedResponse = Assert.IsType<UserModel>(okResult.Value);
        Assert.Equal(expectedResult.UserId, returnedResponse.UserId);
        Assert.Equal(expectedResult.ContactNumber, returnedResponse.ContactNumber);
        Assert.Equal(expectedResult.IsActive, returnedResponse.IsActive);
    }
    
    [Fact]
    public async Task GetUserById_ServiceThrowsException_ShouldReturnBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s=>s.GetUserById(1, It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = await _controller.GetUserById(1);
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }

    [Fact]
    public async Task RegisterUser_WhenSuccessful_ShouldReturnStatus201()
    {
        _userService.Setup(s => s.Register(GetUserRegisterRequest(), It.IsAny<CancellationToken>()));
        var result = _controller.Register(GetUserRegisterRequest());
        var okResult = Assert.IsType<ObjectResult>(result.Result);
    }

    
    [Fact]
    public async Task RegisterUser_WhenExceptionThrown_ShouldReturnStatus400BadRequest()
    {
        var expectedException = new Exception("Test error");
        var request = GetUserRegisterRequest();
        _userService.Setup(s => s.Register(It.IsAny<UserRegisterRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(expectedException);
        var result = await _controller.Register(request);
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task UpdateUser_UpatesUserSuccessfully()
    {
        _userService.Setup(s =>
            s.UpdateUser(It.IsAny<int>(), It.IsAny<UpdateUserRequestModel>(), It.IsAny<CancellationToken>()));
        var result = _controller.UpdateUser(1, new UpdateUserRequestModel());
        var okResult = Assert.IsType<NoContentResult>(result.Result);
    }
    
    [Fact]
    public async Task UpdateUser_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.UpdateUser(It.IsAny<int>(), It.IsAny<UpdateUserRequestModel>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.UpdateUser(1, new UpdateUserRequestModel());
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task UpdateUserProfile_UpatesUserProfileSuccessfully()
    {
        _userService.Setup(s =>
            s.UpdateUserProfile(It.IsAny<int>(), It.IsAny<UpdateUserProfileRequestModel>(), It.IsAny<CancellationToken>()));
        var result = _controller.UpdateUserProfile(1, new UpdateUserProfileRequestModel());
        var okResult = Assert.IsType<NoContentResult>(result.Result);
    }
    
    [Fact]
    public async Task UpdateUserProfile_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.UpdateUserProfile(It.IsAny<int>(), It.IsAny<UpdateUserProfileRequestModel>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.UpdateUserProfile(1, new UpdateUserProfileRequestModel());
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task ActivateUser_ActivatesUserSuccessfully()
    {
        _userService.Setup(s =>
            s.ActivateUser(It.IsAny<int>(), It.IsAny<CancellationToken>()));
        var result = _controller.ActivateUser(1);
        var okResult = Assert.IsType<NoContentResult>(result.Result);
    }
    
    [Fact]
    public async Task ActivateUser_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.ActivateUser(It.IsAny<int>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.ActivateUser(1);
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task DeleteUser_ActivatesUserSuccessfully()
    {
        _userService.Setup(s =>
            s.DeleteUser(It.IsAny<int>(), It.IsAny<CancellationToken>()));
        var result = _controller.DeleteUser(1);
        var okResult = Assert.IsType<NoContentResult>(result.Result);
    }
    
    [Fact]
    public async Task DeleteUser_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.DeleteUser(It.IsAny<int>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.DeleteUser(1);
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CheckUserName_ReturnsSuccessfully()
    {
        _userService.Setup(s =>
            s.CheckUsername(It.IsAny<string>(), It.IsAny<CancellationToken>()));
        var result = _controller.CheckUsername("somehitng@something");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
    }
    [Fact]
    public async Task CheckUserName_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.CheckUsername(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.CheckUsername("somehitng@something");
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task SendOtp_ReturnsSuccessfully()
    {
        _userService.Setup(s =>
            s.SendOtp(It.IsAny<string>(), It.IsAny<CancellationToken>()));
        var result = _controller.SendOtp("somehitng@something");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
    }
    [Fact]
    public async Task SendOtp_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.SendOtp(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.SendOtp("somehitng@something");
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task ConfirmOtp_ReturnsSuccessfully()
    {
        _userService.Setup(s =>
            s.ConfirmOtp(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        var result = _controller.ConfirmOtp("somehitng@something", "1234");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
    }
    [Fact]
    public async Task ConfirmOtp_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.ConfirmOtp(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.ConfirmOtp("somehitng@something", "1234");
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task ResetPassword_ReturnsSuccessfully()
    {
        _userService.Setup(s =>
            s.ResetPassword(It.IsAny<string>(), It.IsAny<Models.Users.UpdatePassword.ResetPasswordRequest>(), It.IsAny<CancellationToken>()));
        var result = _controller.ResetPassword("somehitng@something", new Models.Users.UpdatePassword.ResetPasswordRequest());
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
    }
    [Fact]
    public async Task ResetPassword_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.ResetPassword(It.IsAny<string>(), It.IsAny<Models.Users.UpdatePassword.ResetPasswordRequest>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.ResetPassword("somehitng@something", new Models.Users.UpdatePassword.ResetPasswordRequest());
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Login_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.Login(It.IsAny<LoginRequest>(),  It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.Login(new LoginRequest("somehitng@something", "somehitng@something"), new CancellationToken());
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task ChangePassword_WhenSuccessful_Returns200Ok()
    {

        _userService.Setup(s =>
            s.ChangePassword(It.IsAny<ChangePasswordRequest>(), It.IsAny<CancellationToken>()));
        var result = _controller.ChangePassword(new ChangePasswordRequest());
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
    }
    
    [Fact]
    public async Task ChangePassword_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _userService.Setup(s =>
            s.ChangePassword(It.IsAny<ChangePasswordRequest>(),  It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.ChangePassword(new ChangePasswordRequest());
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    private UserRegisterRequest GetUserRegisterRequest()
    {
        return new UserRegisterRequest
        {
            ContactNumber = 13143,
            FirstName = "John",
            LastName = "Doe"
        };
    }

    private AuthenticatedResponse GetAuthenticatedResponse()
    {
        return new AuthenticatedResponse
        {
            IsEnforcePassword = false,
            RefreshToken = "1233",
            RolePriveleges = new List<RolePrivelege>(),
            Token = "testtoken",
            UserId = 1
        };
    }
}