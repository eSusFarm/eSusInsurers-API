using eSusInsurers.Models;
using eSusInsurers.Models.Users.ChangePassword;
using eSusInsurers.Models.Users.GetUsers;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.UpdatePassword;
using eSusInsurers.Models.Users.UpdateUser;

namespace eSusInsurers.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(UserRegisterRequest request, CancellationToken cancellationToken);

        Task<bool> SendOtp(string userName, CancellationToken cancellationToken);

        Task<AuthenticatedResponse> Login(LoginRequest resource, CancellationToken cancellationToken);

        Task<object> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken);

        Task<string> CheckUsername(string userName, CancellationToken cancellationToken);

        Task<string> ConfirmOtp(string userName, string otp, CancellationToken cancellationToken);

        Task<bool> ResetPassword(string userName, ResetPasswordRequest request, CancellationToken cancellationToken);

        Task<Models.Common.PagedResult<UserModel>> GetUsers(GetUsersQuery request, CancellationToken cancellationToken);

        Task UpdateUser(int userId, UpdateUserRequestModel request, CancellationToken cancellationToken);

        Task<object> DeleteUser(int userId, CancellationToken cancellationToken);

        Task<object> ActivateUser(int userId, CancellationToken cancellationToken);


    }
}
