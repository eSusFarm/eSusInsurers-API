using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.SeasonCutOffDate
{
    public class GetSeasonCutOffDatesResponse
    {
        public PagedResult<SeasonCutOffDatesModel> Roles { get; set; } = PagedResult<SeasonCutOffDatesModel>.Empty;
    }
}
