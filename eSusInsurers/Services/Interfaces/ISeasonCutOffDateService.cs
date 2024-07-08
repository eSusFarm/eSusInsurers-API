using eSusInsurers.Models.SeasonCutOffDate;

namespace eSusInsurers.Services.Interfaces
{
    public interface ISeasonCutOffDateService
    {
        Task<Models.Common.PagedResult<SeasonCutOffDatesModel>> GetSeasonCutOffDates(GetSeasonCutOffDatesQuery request, CancellationToken cancellationToken);
        Task<bool> AddSeasonCutOffDates(List<SeasonCutOffDatesRequest> request, CancellationToken cancellationToken);
        Task UpdateSeasonCutOffDates(int seasonCutOffDateId, UpdateSeasonCutOffDatesRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteSeasonCutOffDates(int seasonCutOffDateId, CancellationToken cancellationToken);
        Task<bool> ActivateSeasonCutOffDates(int seasonCutOffDateId, CancellationToken cancellationToken);
        Task<bool> SeasonCutOffDatesExistenceCheck(SeasonCutOffDatesRequest request, CancellationToken cancellationToken);
    }
}
