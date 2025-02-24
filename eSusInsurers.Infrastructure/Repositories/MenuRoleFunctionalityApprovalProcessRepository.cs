using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class MenuRoleFunctionalityApprovalProcessRepository : Repository<MenuRoleFunctionalityApprovalProcess>, IMenuRoleFunctionalityApprovalProcessRepository
    {
        public MenuRoleFunctionalityApprovalProcessRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<MenuRoleFunctionalityApprovalProcess>> GetByRoleIdAsync(long roleId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[] { "Role", "FunctionalityApprovalProcess" })
                        .Where(x => x.RoleId == roleId
                                 && x.IsActive
                                 && (x.Enable))
                        .ToListAsync(cancellationToken);
        }

        public async Task<List<MenuRoleFunctionalityApprovalProcess>> GetAllByRoleIdAsync(long roleId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[] { "Role", "FunctionalityApprovalProcess" })
                        .Where(x => x.RoleId == roleId
                                 && x.IsActive)
                        .ToListAsync(cancellationToken);
        }
    }
}
