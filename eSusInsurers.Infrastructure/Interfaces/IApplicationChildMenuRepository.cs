using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IApplicationChildMenuRepository : IRepository<ApplicationChildMenu>
    {
        Task<List<ApplicationChildMenu?>> GetApplicationChildMenus(long ApplicationMenuId, CancellationToken CancellationToken);
    }
}
