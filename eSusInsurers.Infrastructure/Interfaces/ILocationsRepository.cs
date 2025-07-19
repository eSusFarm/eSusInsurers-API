using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces;

public interface ILocationsRepository: IRepository<Location>
{
    Task<Location?> GetByRegionDistrictAndSubCounty(int regionId, int districtId, int subCountyId, CancellationToken cancellationToken);
}