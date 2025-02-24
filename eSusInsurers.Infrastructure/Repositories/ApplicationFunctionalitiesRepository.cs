using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class ApplicationFunctionalitiesRepository : Repository<ApplicationFunctionality>, IApplicationFunctionalitiesRepository
    {
        public ApplicationFunctionalitiesRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<ApplicationFunctionality>> GetApplicationFunctionalities(long ApplicationMenuId, long ApplicationChildMenuId, CancellationToken cancellationToken)
        {
            return await GetAll().Where(x => x.ApplicationMenuId == ApplicationMenuId && x.ApplicationChildMenuId == ApplicationChildMenuId).OrderBy(x => x.CreatedDate).ToListAsync(cancellationToken);
        }

        public async Task<List<ApplicationFunctionality>> GetApplicationFunctionality(long ApplicationMenuId, CancellationToken cancellationToken)
        {
            return await GetAll().Where(x => x.ApplicationMenuId == ApplicationMenuId && x.ApplicationChildMenuId == null).OrderBy(x => x.CreatedDate).ToListAsync(cancellationToken);
        }
    }
}
