using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {

        public readonly DbContext _dbContext;
        public RoleRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Role?> GetByRoleNameAsync(string roleName, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.RoleName.ToLower() == roleName.ToLower(), cancellationToken);
        }

        public async Task<Role?> GetByRoleNameAsync(long roleId, string roleName, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id != roleId && x.RoleName.ToLower() == roleName.ToLower(), cancellationToken);
        }

        public async Task<SP_GetRoleDetailsResult?> GetRoleDetails(long roleId, CancellationToken cancellationToken)
        {
            var response = await ((eSusInsurerContext)_dbContext).Procedures.SP_GetRoleDetailsAsync(roleId, null, cancellationToken);
            if (response != null)
            {
                return response.FirstOrDefault();
            }

            return default;
        }
    }
}
