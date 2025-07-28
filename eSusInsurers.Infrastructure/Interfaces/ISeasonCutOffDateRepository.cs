using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure
{
    public interface ISeasonCutOffDateRepository: IRepository<SeasonCutOffDate>
    {
        Task<SeasonCutOffDate?> GetBySeasonCutOffDateAsync(int seasonId, int regionId, int? cropCategoryId, int? cropId, CancellationToken cancellationToken);

        Task<SeasonCutOffDate?> GetBySeasonCutOffDateAsync(int seasonCutOffDateId, int seasonId, int regionId, int? cropCategoryId, int? cropId, CancellationToken cancellationToken);

        Task<SeasonCutOffDate?> GetByCropAndSeason(int seasonId, int regionId, int? cropCategoryId, int? cropId, CancellationToken cancellationToken);
    }
}
