using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IFunctionalityApprovalProcessRepository : IRepository<FunctionalityApprovalProcess>
    {
        Task<List<FunctionalityApprovalProcess>> GetFunctionalityApprovalProcesses(long ApplicationFunctionalityId, CancellationToken cancellationToken);
    }
}
