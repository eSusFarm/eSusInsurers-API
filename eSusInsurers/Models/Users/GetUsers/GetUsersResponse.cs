using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.Users.GetUsers;

public class GetUsersResponse
{
    public PagedResult<UserModel> Users { get; set; } = PagedResult<UserModel>.Empty;
}