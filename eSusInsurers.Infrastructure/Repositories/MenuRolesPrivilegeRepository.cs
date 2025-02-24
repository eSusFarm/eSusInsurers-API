using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class MenuRolesPrivilegeRepository : Repository<MenuRolesPrivilege>, IMenuRolesPrivilegeRepository
    {
        public MenuRolesPrivilegeRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<MenuRolesPrivilege>> GetByRoleIdAsync(long roleId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[] { "Role", "ApplicationMenu", "ApplicationChildMenu" })
                        .Where(x => x.RoleId == roleId
                                 && x.IsActive
                                 && (x.Read || x.Create || x.Update || x.Delete))
                        .ToListAsync(cancellationToken);
        }

        public async Task<List<MenuRolesPrivilege>> GetAllRoleIdAsync(long roleId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[] { "Role", "ApplicationMenu", "ApplicationChildMenu" })
                        .Where(x => x.RoleId == roleId && x.IsActive)
                        .ToListAsync(cancellationToken);
        }
    }
}
