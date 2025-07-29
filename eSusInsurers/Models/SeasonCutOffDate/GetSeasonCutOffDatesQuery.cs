using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.SeasonCutOffDate
{
    public class GetSeasonCutOffDatesQuery
    {
        public PagingOptions pagingOptions { get; set; }
        public SeasonCutOffDatesFilterOptions? filter { get; set; }
        public SortingOptions? sortingOptions { get; set; }
    }
}
