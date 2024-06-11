using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.Roles.GetRoles
{
    public class GetRolesResponse
    {
        public PagedResult<RoleModel> Roles { get; set; } = PagedResult<RoleModel>.Empty;
    }
}
