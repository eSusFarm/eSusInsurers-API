using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure
{
    public class SeasonRepository : Repository<Season>, ISeasonRepository
    {
        public readonly DbContext _dbContext;

        public SeasonRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Season?> GetBySeasonNameAsync(string seasonName, string seasonYear, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.SeasonName.ToLower() == seasonName.ToLower() 
            && x.SeasonYear != null && x.SeasonYear.ToLower() == seasonYear.ToLower(), cancellationToken);
        }

        public async Task<Season?> GetBySeasonNameAsync(int seasonId,string seasonName, string seasonYear, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.SeasonName.ToLower() == seasonName.ToLower()
            && x.SeasonYear != null && x.Id != seasonId && x.SeasonYear.ToLower() == seasonYear.ToLower(), cancellationToken);
        }
    }
}
