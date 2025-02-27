using eSusInsurers.Models;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.Users.ChangePassword;
using eSusInsurers.Models.Users.GetUsers;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.UpdatePassword;
using eSusInsurers.Models.Users.UpdateUser;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers;

/// <summary>
///     Controller for managing users.
/// </summary>
[Route("insurance/users")]
public class UserController(IUserService userService) : BaseController
{
    /// <summary>
    ///     Get Users
    /// </summary>
    /// <remarks>
    ///     Returns a paginated list of users.
    /// </remarks>
    /// <param name="pagingOptions">Pagination options for response.</param>
    /// <param name="filter">Data filter options.</param>
    /// <param name="sort">Data sorting options.</param>
    /// <response code="200">Returns a paginated list of users.</response>
    /// <returns>Paginated list of users.</returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersResponse))]
    public async Task<ActionResult<GetUsersResponse>> GetUsers([FromQuery] PagingOptions pagingOptions = default!,
        [FromQuery] UserFilterOptions filter = default!,
        [FromQuery] SortingOptions sort = default!)
    {
        try
        {
            var query = new GetUsersQuery
            {
                pagingOptions = pagingOptions,
                filter = filter,
                sortingOptions = sort
            };

            var result = await userService.GetUsers(query, new CancellationToken());

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Get User By Id
    /// </summary>
    /// <remarks>
    ///     Returns user details.
    /// </remarks>
    /// <param name="userId">User id of the user.</param>
    /// <response code="200">Returns user details.</response>
    /// <returns>Returns user details..</returns>
    [HttpGet("{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
    public async Task<ActionResult<UserModel>> GetUserById(long userId)
    {
        try
        {
            var result = await userService.GetUserById(userId, new CancellationToken());

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Add new user
    /// </summary>
    /// <remarks>
    ///     Add new user
    /// </remarks>
    /// <param name="request">Information of the user to register</param>
    /// <response code="201">Indicates the user is successfully created.</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        try
        {
            var response = await userService.Register(request, new CancellationToken());

            return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Update a user
    /// </summary>
    /// <remarks>
    ///     Update a user
    /// </remarks>
    /// <param name="request">user details of the user</param>
    /// <param name="userId">user id of the user</param>
    /// <response code="204">Indicates the user details is updated</response>
    [HttpPut("{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUser(int userId, UpdateUserRequestModel request)
    {
        try
        {
            await userService.UpdateUser(userId, request, new CancellationToken());

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Update a user profile
    /// </summary>
    /// <remarks>
    ///     Update a user profile
    /// </remarks>
    /// <param name="request">user details of the user profile</param>
    /// <param name="userId">user id of the user profile</param>
    /// <response code="204">Indicates the user profile details is updated</response>
    [HttpPatch("{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUserProfile(int userId, UpdateUserProfileRequestModel request)
    {
        try
        {
            await userService.UpdateUserProfile(userId, request, new CancellationToken());

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Deactivate a user
    /// </summary>
    /// <remarks>
    ///     Deactivate a user
    /// </remarks>
    /// <param name="userId">user id of the user</param>
    /// <response code="204">Indicates the user is inactive</response>
    [HttpDelete("{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        try
        {
            var response = await userService.DeleteUser(userId, new CancellationToken());
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Activate a user
    /// </summary>
    /// <remarks>
    ///     Activate a user
    /// </remarks>
    /// <param name="userId">user id of the user</param>
    /// <response code="204">Indicates the user is active</response>
    [HttpPut("{userId}/activate")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ActivateUser(int userId)
    {
        try
        {
            var response = await userService.ActivateUser(userId, new CancellationToken());
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     User name existence
    /// </summary>
    /// <remarks>
    ///     User name existence
    /// </remarks>
    /// <param name="userName">To check whether user name exists or not</param>
    /// <response code="200">Indicates the user existance.</response>
    [HttpGet("{userName}/check-username")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckUsername(string userName)
    {
        try
        {
            var response = await userService.CheckUsername(userName, new CancellationToken());
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Send Otp to user
    /// </summary>
    /// <remarks>
    ///     Send Otp to user
    /// </remarks>
    /// <param name="userName">Send otp to the userName</param>
    /// <response code="200">Indicates the otp has been successsful.</response>
    [HttpPost("{userName}/send-otp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendOtp(string userName)
    {
        try
        {
            var response = await userService.SendOtp(userName, new CancellationToken());
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Confirm Otp of user
    /// </summary>
    /// <remarks>
    ///     COnfirm Otp od user
    /// </remarks>
    /// <param name="userName">userName of the user</param>
    /// <param name="otp">Otp sent to the userName</param>
    /// <response code="200">Indicates the otp has been confirmed.</response>
    [HttpPost("{userName}/confirm-otp/{otp}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmOtp(string userName, string otp)
    {
        try
        {
            var response = await userService.ConfirmOtp(userName, otp, new CancellationToken());
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Reset Password
    /// </summary>
    /// <remarks>
    ///     Reset Password
    /// </remarks>
    /// <param name="emailId">email Id of the user</param>
    /// <param name="request">Information of the user to reset password</param>
    /// <response code="200">Indicates the user password is successfully updated.</response>
    [HttpPost("{userName}/password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ResetPassword(string userName,
        [FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        try
        {
            var response = await userService.ResetPassword(userName, resetPasswordRequest, new CancellationToken());
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     User Login
    /// </summary>
    /// <remarks>
    ///     User Login
    /// </remarks>
    /// <param name="request">Information of the user to login</param>
    /// <response code="200">Indicates the user successful login.</response>
    [HttpPost("login-with-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await userService.Login(request, new CancellationToken());
            Response.Headers.Append("Access-Control-Expose-Headers", "Authorization");
            Response.Headers.Append("Authorization", response.Token);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    /// <summary>
    ///     Change Password
    /// </summary>
    /// <remarks>
    ///     Change Password
    /// </remarks>
    /// <param name="request">Information of the user to change password</param>
    /// <response code="200">Indicates the user password is successfully updated.</response>
    [Authorize]
    [HttpPost("change_password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        try
        {
            var response = await userService.ChangePassword(request, new CancellationToken());
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }
}