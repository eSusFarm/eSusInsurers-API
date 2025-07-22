using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.Seasons
{
    public class GetSeasonsResponse
    {
        public PagedResult<SeasonModel> Roles { get; set; } = PagedResult<SeasonModel>.Empty;
    }
}
