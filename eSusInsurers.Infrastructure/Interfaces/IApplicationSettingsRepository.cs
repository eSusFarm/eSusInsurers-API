using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IApplicationSettingsRepository : IRepository<ApplicationSetting>
    {
        Task<List<ApplicationSetting>> GetApplicationSettingsDetails(CancellationToken cancellationToken);
    }
}
