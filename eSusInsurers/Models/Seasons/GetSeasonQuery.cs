using eSusInsurers.Models.Common;
using eSusInsurers.Models.Roles.GetRoles;

namespace eSusInsurers.Models.Seasons
{
    public class GetSeasonQuery
    {
        public PagingOptions pagingOptions { get; set; }
        public SeasonFilterOptions? filter { get; set; }
        public SortingOptions? sortingOptions { get; set; }
    }
}
