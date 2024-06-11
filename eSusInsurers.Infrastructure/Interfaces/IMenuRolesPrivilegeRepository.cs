using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IMenuRolesPrivilegeRepository : IRepository<MenuRolesPrivilege>
    {
        Task<List<MenuRolesPrivilege>> GetByRoleIdAsync(long roleId, CancellationToken cancellationToken);
        Task<List<MenuRolesPrivilege>> GetAllRoleIdAsync(long roleId, CancellationToken cancellationToken);
    }
}
