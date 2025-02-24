using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class MenuRolesFunctionalityRepository : Repository<MenuRolesFunctionality>, IMenuRolesFunctionalityRepository
    {
        public MenuRolesFunctionalityRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<MenuRolesFunctionality>> GetByRoleIdAsync(long roleId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[] { "Role", "ApplicationFunctionality" })
                        .Where(x => x.RoleId == roleId
                                 && x.IsActive
                                 && (x.Enable))
                        .ToListAsync(cancellationToken);
        }

        public async Task<List<MenuRolesFunctionality>> GetAllRoleIdAsync(long roleId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[] { "Role", "ApplicationFunctionality" })
                        .Where(x => x.RoleId == roleId
                                 && x.IsActive)
                        .ToListAsync(cancellationToken);
        }
    }
}
