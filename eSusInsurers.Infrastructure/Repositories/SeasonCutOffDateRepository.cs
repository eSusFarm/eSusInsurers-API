using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure
{
    public class SeasonCutOffDateRepository : Repository<SeasonCutOffDate>, ISeasonCutOffDateRepository
    {
        public readonly DbContext _dbContext;

        public SeasonCutOffDateRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<SeasonCutOffDate?> GetBySeasonCutOffDateAsync(int seasonId, int regionId, int? cropCategoryId, int? cropId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[]
                   {
                       "Crop",  "CropCategory",  "Region",  "Season"
                   }).FirstOrDefaultAsync(x => x.SeasonId == seasonId
            && x.RegionId == regionId && x.CropCategoryId == cropCategoryId
            && x.CropId == cropId, cancellationToken);
        }

        public async Task<SeasonCutOffDate?> GetBySeasonCutOffDateAsync(int seasonCutOffDateId, int seasonId, int regionId, int? cropCategoryId, int? cropId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[]
                   {
                       "Crop",  "CropCategory",  "Region",  "Season"
                   }).FirstOrDefaultAsync(x => x.Id != seasonCutOffDateId && x.SeasonId == seasonId
            && x.RegionId == regionId && x.CropCategoryId == cropCategoryId
            && x.CropId == cropId, cancellationToken);
        }
    }
}
