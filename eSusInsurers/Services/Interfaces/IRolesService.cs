using eSusInsurers.Models.Common;
using eSusInsurers.Models.Roles.GetRoles;
using eSusInsurers.Models.Roles.RoleDetails;
using WMS.Models.Roles;
using WMS.Models.Roles.UpdateRole;

namespace eSusInsurers.Services.Interfaces;

public interface IRolesService
{
    Task<List<ApplicationMenuItems>?> GetApplicationMenuItems(CancellationToken cancellationToken);
    Task<PagedResult<RoleModel>> GetRoles(GetRolesQuery request, CancellationToken cancellationToken);
    Task<long> AddRole(RoleRequest request, CancellationToken cancellationToken);
    Task UpdateRole(int roleId, UpdateRoleRequestModel request, CancellationToken cancellationToken);
    Task<RoleDetails?> RoleDetails(int roleId, CancellationToken cancellationToken);
}