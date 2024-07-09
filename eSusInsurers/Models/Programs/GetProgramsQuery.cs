using eSusInsurers.Models.Common;
using eSusInsurers.Models.Users.GetUsers;

namespace eSusInsurers.Models.Programs
{
    public class GetProgramsQuery
    {
        public PagingOptions pagingOptions { get; set; }
        public ProgramFilterOptions? filter { get; set; }
        public SortingOptions? sortingOptions { get; set; }
    }
}
