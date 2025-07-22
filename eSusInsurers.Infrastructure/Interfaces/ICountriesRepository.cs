using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface ICountriesRepository : IRepository<Region>
    {
        Task<Region?> GetByRegionName(String regionName, CancellationToken cancellationToken);
    }
}
