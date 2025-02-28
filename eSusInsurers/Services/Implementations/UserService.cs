using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Constants;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Helpers;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.enums;
using eSusInsurers.Models.Extensions;
using eSusInsurers.Models.Helpers;
using eSusInsurers.Models.Users.ChangePassword;
using eSusInsurers.Models.Users.GetUsers;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.UpdatePassword;
using eSusInsurers.Models.Users.UpdateUser;
using eSusInsurers.Services.Common;
using eSusInsurers.Services.Interfaces;
using ApplicationChildMenu = eSusInsurers.Models.Users.Login.ApplicationChildMenu;
using Claim = System.Security.Claims.Claim;

namespace eSusInsurers.Services.Implementations;

public class UserService(
    IUnitOfWork unitOfWork,
    IConfiguration configuration,
    IMapper mapper,
    ITokenService tokenService,
    IDateTime dateTime,
    IEmailService emailService) : IUserService
{
    
    
    public async Task<bool> Register(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var user = await unitOfWork.UserRepository.GetByEmailIdAsync(request.EmailId, cancellationToken);

        if (user != null)
            throw new Exception($"({request.EmailId}) already exists.");

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
                var parameters = new NotificationContentParameters
                {
                    EmailId = user.EmailId,
                    Password = password,
                    UserName = user.FirstName + " " + user.LastName
                };

                await emailService.SendEmailAsync(parameters, AppEvents.CreateUser, [user.EmailId], null,
                    cancellationToken);
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

    public async Task<Models.Common.PagedResult<UserModel>> GetUsers(GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Models.Common.Filter> filters = UsersFilters(request);
        Expression<Func<User, bool>> predicate = ExpressionBuilder<User>.BuildFilterExpression(filters);
        Dictionary<string, Models.Common.Filter> paginationFilters =
            FilterHelper.CreatePaginationFilters(request.pagingOptions);
        var paginationParams = FilterHelper.GetPaginationParams(paginationFilters);
        Dictionary<string, Models.Common.Filter> orderByFilters =
            FilterHelper.CreateOrderByFilters(request.sortingOptions);
        var orderByParams = OrderByHelper.GetOrderByParams(orderByFilters);

        var query = unitOfWork.UserRepository.GetAll(
                new[]
                {
                    "Insurer", "Role", "ReportingToNavigation"
                })
            .Where(predicate)
            .OrderByDescending(x => x.Id)
            .ProjectTo<UserModel>(mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(orderByParams) && orderByFilters.ContainsKey(ApplicationConstants.sortBy) &&
            orderByFilters[ApplicationConstants.sortBy].Value.ToLower() == "descending")
            orderByParams += ApplicationConstants.descending;

        if (!string.IsNullOrEmpty(orderByParams)) query = query.OrderBy(orderByParams);

        var users = query.ToPagedResult(paginationParams.Page, paginationParams.PageSize);

        return new Models.Common.PagedResult<UserModel>
        {
            PageSize = paginationParams.PageSize,
            TotalPages = users.TotalPages,
            TotalRecordCount = users.TotalRecordCount,
            Records = users.Records,
            CurrentPage = paginationParams.Page
        };
    }

    public async Task<UserModel?> GetUserById(long userId, CancellationToken cancellationToken)
    {
        Dictionary<string, Models.Common.Filter> filters = UserFilterById(userId);
        Expression<Func<User, bool>> predicate = ExpressionBuilder<User>.BuildFilterExpression(filters);

        var query = unitOfWork.UserRepository.GetAll(
                new[]
                {
                    "Insurer", "Role", "ReportingToNavigation"
                })
            .Where(predicate)
            .OrderByDescending(x => x.Id)
            .ProjectTo<UserModel>(mapper.ConfigurationProvider);

        return query.AsQueryable().FirstOrDefault();
    }

    public async Task UpdateUser(int userId, UpdateUserRequestModel request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        var user = await unitOfWork.UserRepository.GetByEmailIdNotUserIdAsync(userId, request.EmailId,
            cancellationToken);

        if (user != null)
            throw new Exception($"Email Id ({request.EmailId}) already exists.");

        user = await unitOfWork.UserRepository.GetByIdAsync(userId, null, false, cancellationToken);

        if (user == null)
            throw new NotFoundException("User Id doesn't exist.");

        var userType = await unitOfWork.RoleRepository.GetByIdAsync(request.RoleId, null, false, cancellationToken);

        if (userType == null)
            throw new BadRequestException($"User Type Id ({request.RoleId}) doesn't exist.");

        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            mapper.Map(request, user);

            await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }

    public async Task UpdateUserProfile(int userId, UpdateUserProfileRequestModel request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        var user = await unitOfWork.UserRepository.GetByIdAsync(userId, null, false, cancellationToken);

        if (user == null)
            throw new NotFoundException("User Id doesn't exist.");

        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            mapper.Map(request, user);

            await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }

    public async Task<object> DeleteUser(int userId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        var user = await unitOfWork.UserRepository.GetByIdAsync(userId, null, false, cancellationToken);

        if (user == null)
            throw new NotFoundException($"User Id: ({userId}) doesn't exist.");

        user.IsActive = false;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<object> ActivateUser(int userId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        var user = await unitOfWork.UserRepository.GetByIdAsync(userId, null, false, cancellationToken);

        if (user == null)
            throw new NotFoundException($"User Id: ({userId}) doesn't exist.");

        user.IsActive = true;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
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

        var parameters = new NotificationContentParameters
        {
            Index0 = user.Otp[0].ToString(),
            Index1 = user.Otp[1].ToString(),
            Index2 = user.Otp[2].ToString(),
            Index3 = user.Otp[3].ToString()
        };

        _ = Task.Run(async () =>
        {
            await emailService.SendEmailAsync(parameters, AppEvents.SendOtp, new[] { userName }, null,
                cancellationToken);
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
            throw new BadRequestException("Otp has been expired.");

        if (user.Otp != otp)
            throw new BadRequestException("Invalid Otp.");

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

    public async Task<bool> ResetPassword(string userName, ResetPasswordRequest request,
        CancellationToken cancellationToken)
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
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
            new Claim(ClaimTypes.Email, user.EmailId),
            new Claim(ClaimTypes.Role, user.Role.RoleName)
        };
        var accessToken = tokenService.GenerateAccessToken(claims);

        var refreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;

        _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out var refreshTokenValidityInDays);

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

        var menuRolesPrivileges =
            await unitOfWork.MenuRolesPrivilegeRepository.GetByRoleIdAsync(user.RoleId, cancellationToken);

        var priveleges = menuRolesPrivileges
            .GroupBy(menu => new
            {
                menu.ApplicationMenuId,
                menu.ApplicationMenu.ApplicationMenuName,
                menu.ApplicationMenu.ApplicationMenuLink,
                menu.ApplicationMenu.ApplicationMenuIcon,
                menu.ApplicationMenu.ApplicationMenuIcon2,
                menu.ApplicationMenu.Sequence
            })
            .Select(group => new RolePrivelege
            {
                ApplicationMenuId = group.Key.ApplicationMenuId,
                ApplicationMenuName = group.Key.ApplicationMenuName,
                ApplicationMenuLink = group.Key.ApplicationMenuLink,
                ApplicationMenuIcon = group.Key.ApplicationMenuIcon,
                ApplicationMenuIcon2 = group.Key.ApplicationMenuIcon2,
                ApplicationMenuSequence = group.Key.Sequence,
                ChildMenus = group.Where(menu => menu.ApplicationChildMenuId.HasValue).Select(menu =>
                    new ApplicationChildMenu
                    {
                        ApplicationChildMenuId = menu.ApplicationChildMenuId,
                        ApplicationChildMenuName = menu.ApplicationChildMenu?.ApplicationChildMenuName,
                        ApplicationChildMenuLink = menu.ApplicationChildMenu?.ApplicationChildMenuLink,
                        ApplicationChildMenuIcon = menu.ApplicationChildMenu?.ApplicationChildMenuIcon,
                        ApplicationChildMenuIcon2 = menu.ApplicationChildMenu?.ApplicationChildMenuIcon2,
                        ApplicationChildMenuSequence = menu.ApplicationChildMenu?.Sequence,
                        Read = menu.Read,
                        Create = menu.Create,
                        Update = menu.Update,
                        Delete = menu.Delete
                    }).ToList(),
                Read = !group.Any(menu => menu.ApplicationChildMenuId.HasValue) ? group.First().Read : null,
                Create = !group.Any(menu => menu.ApplicationChildMenuId.HasValue) ? group.First().Create : null,
                Update = !group.Any(menu => menu.ApplicationChildMenuId.HasValue) ? group.First().Update : null,
                Delete = !group.Any(menu => menu.ApplicationChildMenuId.HasValue) ? group.First().Delete : null
            }).ToList();

        priveleges = priveleges.OrderBy(cm => cm.ApplicationMenuSequence).ToList();

        priveleges.ForEach(rp =>
        {
            if (rp.ChildMenus != null)
                rp.ChildMenus = rp.ChildMenus.OrderBy(cm => cm.ApplicationChildMenuSequence).ToList();
        });

        return new AuthenticatedResponse
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            UserId = user.Id,
            IsEnforcePassword = user.IsEnforcePassword,
            RolePriveleges = priveleges
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

    #region Fields

    private readonly string _pepper = configuration.GetValue<string>("PasswordHashPepper") ??
                                      throw new Exception("Hash configuration is missing.");

    private readonly int _iteration = 3;

    #endregion

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

    #region Private Methods

    private static Dictionary<string, Models.Common.Filter> UsersFilters(GetUsersQuery invQuery)
    {
        var inboundDto = invQuery.filter;
        var filters = new Dictionary<string, Models.Common.Filter>();

        if (inboundDto != null)
        {
            Filters.AddFilterIfNotEmpty(filters, inboundDto?.UserNameOrEmailId, "FirstName",
                SearchOperationEnum.Contains);

            Filters.AddFilterIfNotEmpty(filters, inboundDto?.UserNameOrEmailId, "LastName",
                SearchOperationEnum.Contains);

            Filters.AddFilterIfNotEmpty(filters, inboundDto?.UserNameOrEmailId, "EmailId", SearchOperationEnum.Contains,
                true);

            if (inboundDto?.IsActive != null)
                Filters.AddFilterIfNotEmpty(filters, inboundDto.IsActive == true ? "True" : "False", "IsActive",
                    SearchOperationEnum.Equal);

            Filters.AddFilterIfNotEmpty(filters, inboundDto?.Role, "Role.RoleName", SearchOperationEnum.Equal);
        }

        return filters;
    }

    private static Dictionary<string, Models.Common.Filter> UserFilterById(long userId)
    {
        var filters = new Dictionary<string, Models.Common.Filter>();

        Filters.AddFilterIfValueGreaterThanZero(filters, userId, "Id", SearchOperationEnum.Equal);

        return filters;
    }

    #endregion
}