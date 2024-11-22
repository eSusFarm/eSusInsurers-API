using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.Programs;

public class GetProgramsQuery
{
    public PagingOptions pagingOptions { get; set; }
    public ProgramFilterOptions? filter { get; set; }
    public SortingOptions? sortingOptions { get; set; }
}