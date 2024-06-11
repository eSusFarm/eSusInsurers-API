using AutoMapper;
using EmailService.Interfaces;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Helpers;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.Users.ChangePassword;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.RefreshAccessToken;
using eSusInsurers.Models.Users.UpdatePassword;
using eSusInsurers.Services.Interfaces;
using System.Security.Claims;
using Claim = System.Security.Claims.Claim;

namespace eSusInsurers.Services.Implementations
{
    public class UserService(IUnitOfWork unitOfWork,
                             IConfiguration configuration,
                             IMapper mapper,
                             ITokenService tokenService,
                             IDateTime dateTime,
                             IEmailService emailService) : IUserService
    {
        #region Fields
        private readonly string _pepper = configuration.GetValue<string>("PasswordHashPepper") ?? throw new Exception("Hash configuration is missing.");

        private readonly int _iteration = 3;
        #endregion

        public async Task<bool> Register(UserRegisterRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var user = await unitOfWork.UserRepository.GetByEmailIdAsync(request.UserName, cancellationToken);

            if (user != null)
                throw new Exception($"Username ({request.UserName}) already exists.");

            user = mapper.Map<User>(request);

            var password = PasswordHasher.GeneratePassword();

            user.PasswordHash = PasswordHasher.ComputeHash(password, user.PasswordSalt, _pepper, _iteration);

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await unitOfWork.UserRepository.AddAsync(user, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                _ = Task.Run(async () =>
                {
                    //var parameters = new NotificationContentParameters()
                    //{
                    //    EmailId = user.UserName,
                    //    Password = password,
                    //    UserName = user.UserName
                    //};

                    //await emailService.SendEmailAsync(parameters, AppEvents.CreateUser, [user.UserName], null, cancellationToken);
                });
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return true;
        }

        public async Task<string> CheckUsername(string userName, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userName, nameof(userName));

            var user = await unitOfWork.UserRepository.GetByEmailIdAsync(userName, cancellationToken);

            if (user == null)
                throw new Exception($"Username ({userName}) doesn't exist.");

            return user.PasswordSalt;
        }

        public async Task<bool> SendOtp(string userName, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userName, nameof(userName));

            var user = await unitOfWork.UserRepository.GetByEmailIdAsync(userName, cancellationToken);

            if (user == null)
                throw new NotFoundException($"Username ({userName}) doesn't exist.");

            user.Otp = RandomOTP.CreateRandomOTP();

            user.OtpExipiryTime = dateTime.Now.AddMinutes(5);

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            var parameters = new NotificationContentParameters()
            {
                Index0 = user.Otp[0].ToString(),
                Index1 = user.Otp[1].ToString(),
                Index2 = user.Otp[2].ToString(),
                Index3 = user.Otp[3].ToString(),
            };

            _ = Task.Run(async () =>
            {
                await emailService.SendEmailAsync(parameters, AppEvents.SendOtp, new string[] { userName }, null, cancellationToken);
            });

            return true;
        }

        public async Task<string> ConfirmOtp(string userName, string otp, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userName, nameof(userName));

            ArgumentNullException.ThrowIfNull(otp, nameof(otp));

            var user = await unitOfWork.UserRepository.GetByEmailIdAsync(userName, cancellationToken);

            if (user is null)
                throw new NotFoundException($"Username ({userName}) doesn't exist.");

            if (user.OtpExipiryTime < dateTime.Now)
                throw new BadRequestException($"Otp has been expired.");

            if (user.Otp != otp)
                throw new BadRequestException($"Invalid Otp.");

            user.Otp = null;

            user.OtpExipiryTime = null;

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return user.PasswordSalt;
        }

        public async Task<bool> ResetPassword(string userName, ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userName, nameof(userName));

            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var user = await unitOfWork.UserRepository.GetByEmailIdAsync(userName, cancellationToken);

            if (user == null)
                throw new NotFoundException($"Username ({userName}) doesn't exist.");

            user.PasswordHash = PasswordHasher.ComputeHash(request.NewPassword, user.PasswordSalt, _pepper, _iteration);

            user.IsEnforcePassword = false;

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return true;
        }

        public async Task<AuthenticatedResponse> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var user = await unitOfWork.UserRepository.GetByEmailIdAsync(request.Username, cancellationToken);

            if (user == null)
                throw new UnauthorizedException("Username or password did not match.");

            var passwordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _pepper, _iteration);

            if (user.PasswordHash != passwordHash)
                throw new UnauthorizedException("Username or password did not match.");

            var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.FirstName + " "+user.LastName),
            new Claim(ClaimTypes.Role, user.Role.RoleName),
        };
            var accessToken = tokenService.GenerateAccessToken(claims);

            var refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshTokenExpiryTime = dateTime.Now.AddDays(refreshTokenValidityInDays);

            user.LastLoggedInDate = dateTime.Now;

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id,
            };
        }

        public async Task<object> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserId, null, false, cancellationToken);

            if (user == null)
                return new NotFoundException($"User Id: ({request.UserId}) doesn't exist.");

            var passwordHash = PasswordHasher.ComputeHash(request.OldPassword, user.PasswordSalt, _pepper, _iteration);

            if (user.PasswordHash != passwordHash)
                return new UnauthorizedException("Incorrect old password.");

            user.PasswordHash = PasswordHasher.ComputeHash(request.NewPassword, user.PasswordSalt, _pepper, _iteration);

            var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return true;
        }

        //public async Task<AuthenticatedResponse> RefreshTokenAsync(string token, IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken = default)
        //{

        //    string emailId = httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value.ToString();

        //    if (string.IsNullOrEmpty(emailId))
        //        throw new UnauthorizedException();

        //    var user = await _unitOfWork.InternalUserRepository.GetByEmailIdAsync(emailId, cancellationToken);

        //    var tokenModel = new TokenApiModel()
        //    {
        //        RefreshToken = user.RefreshToken
        //    };

        //    return await _tokenService.RefreshToken(tokenModel, emailId, token, cancellationToken);
        //}
    }
}
