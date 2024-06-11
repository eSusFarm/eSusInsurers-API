using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class ApplicationMenuRepository : Repository<ApplicationMenu>, IApplicationMenuRepository
    {
        public readonly DbContext _dbContext;
        public ApplicationMenuRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<string?> GetApplicationMenuList(CancellationToken cancellationToken)
        {
            var response = await ((eSusInsurerContext)_dbContext).Procedures.SP_GetApplicationMenusAsync(null, cancellationToken);
            return response.FirstOrDefault()?.ApplicationMenus;
        }
    }
}
