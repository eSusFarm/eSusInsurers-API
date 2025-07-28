using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    
    public class LocationsRepository: Repository<Location>, ILocationsRepository
    {
        public readonly DbContext _dbContext;
        public LocationsRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }


        public async Task<Location?> GetByRegionDistrictAndSubCounty(int regionId, int districtId, int subCountyId, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.RegionId == regionId && x.SubCountyId == subCountyId, cancellationToken);
        }
    }
}
