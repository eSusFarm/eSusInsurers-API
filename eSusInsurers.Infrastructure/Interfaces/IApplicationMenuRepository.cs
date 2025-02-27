using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IApplicationMenuRepository : IRepository<ApplicationMenu>
    {
        Task<string?> GetApplicationMenuList(CancellationToken cancellationToken);
    }
}
