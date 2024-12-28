using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.Users.GetUsers;

public class GetUsersQuery
{
    public PagingOptions pagingOptions { get; set; }
    public UserFilterOptions? filter { get; set; }
    public SortingOptions? sortingOptions { get; set; }
}