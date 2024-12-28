using eSusInsurers.Models.Common;
using eSusInsurers.Models.Seasons;

namespace eSusInsurers.Services;

public interface ISeasonService
{
    Task<PagedResult<SeasonModel>> GetSeasons(GetSeasonQuery request, CancellationToken cancellationToken);
    Task<int> AddSeason(SeasonRequest request, CancellationToken cancellationToken);
    Task UpdateSeason(int seasonId, UpdateSeasonRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteSeason(int seasonId, CancellationToken cancellationToken);
    Task<bool> ActivateSeason(int seasonId, CancellationToken cancellationToken);
    Task<List<SeasonModel>?> GetSeasonByYear(string year, CancellationToken cancellationToken);
}