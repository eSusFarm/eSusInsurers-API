using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IApplicationFunctionalitiesRepository : IRepository<ApplicationFunctionality>
    {
        Task<List<ApplicationFunctionality>> GetApplicationFunctionalities(long ApplicationMenuId, long ApplicationChildMenuId, CancellationToken cancellationToken);
        Task<List<ApplicationFunctionality>> GetApplicationFunctionality(long ApplicationMenuId, CancellationToken cancellationToken);
    }
}
