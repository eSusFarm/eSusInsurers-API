using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class FunctionalityApprovalProcessRepository : Repository<FunctionalityApprovalProcess>, IFunctionalityApprovalProcessRepository
    {
        public FunctionalityApprovalProcessRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<FunctionalityApprovalProcess>> GetFunctionalityApprovalProcesses(long ApplicationFunctionalityId, CancellationToken cancellationToken)
        {
            return await GetAll().Where(x => x.ApplicationFunctionalityId == ApplicationFunctionalityId).OrderBy(x => x.CreatedDate).ToListAsync(cancellationToken);
        }
    }
}
