using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.Roles.GetRoles;

public class GetRolesQuery
{
    public PagingOptions pagingOptions { get; set; }
    public RoleFilterOptions? filter { get; set; }
    public SortingOptions? sortingOptions { get; set; }
}