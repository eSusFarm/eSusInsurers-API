using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class ApplicationSettingsRepository : Repository<ApplicationSetting>, IApplicationSettingsRepository
    {
        public ApplicationSettingsRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<ApplicationSetting>> GetApplicationSettingsDetails(CancellationToken cancellationToken)
        {
            return await GetAll().Where(x => x.IsActive == true).ToListAsync(cancellationToken);
        }
    }
}
