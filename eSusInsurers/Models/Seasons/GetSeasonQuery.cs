using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.Seasons;

public class GetSeasonQuery
{
    public PagingOptions pagingOptions { get; set; }
    public SeasonFilterOptions? filter { get; set; }
    public SortingOptions? sortingOptions { get; set; }
}