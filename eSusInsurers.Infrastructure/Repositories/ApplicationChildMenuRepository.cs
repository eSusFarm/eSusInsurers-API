using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class ApplicationChildMenuRepository : Repository<ApplicationChildMenu>, IApplicationChildMenuRepository
    {
        public ApplicationChildMenuRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<ApplicationChildMenu>> GetApplicationChildMenus(long ApplicationMenuId, CancellationToken CancellationToken)
        {
            return await GetAll().Where(x => x.IsActive == true && x.ApplicationMenuId == ApplicationMenuId).OrderBy(s => s.Sequence).ToListAsync(CancellationToken);
        }
    }
}
