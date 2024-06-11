using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetByRoleNameAsync(string roleName, CancellationToken cancellationToken);
        Task<Role?> GetByRoleNameAsync(long roleId, string roleName, CancellationToken cancellationToken);
        Task<SP_GetRoleDetailsResult?> GetRoleDetails(long roleId, CancellationToken cancellationToken);
    }
}
