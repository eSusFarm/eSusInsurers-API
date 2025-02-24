using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IMenuRoleFunctionalityApprovalProcessRepository : IRepository<MenuRoleFunctionalityApprovalProcess>
    {
        Task<List<MenuRoleFunctionalityApprovalProcess>> GetByRoleIdAsync(long roleId, CancellationToken cancellationToken);
        Task<List<MenuRoleFunctionalityApprovalProcess>> GetAllByRoleIdAsync(long roleId, CancellationToken cancellationToken);
    }
}
