using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IMenuRolesFunctionalityRepository : IRepository<MenuRolesFunctionality>
    {
        Task<List<MenuRolesFunctionality>> GetByRoleIdAsync(long roleId, CancellationToken cancellationToken);
        Task<List<MenuRolesFunctionality>> GetAllRoleIdAsync(long roleId, CancellationToken cancellationToken);
    }
}
