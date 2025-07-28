using eSusInsurers.Models.Common;
using eSusInsurers.Models.Users.GetUsers;

namespace eSusInsurers.Models.InsuranceProducts
{
    public class InsuranceProductQuery
    {
        public PagingOptions pagingOptions { get; set; }
        public InsuranceProductFilterOption? filter { get; set; }
        public SortingOptions? sortingOptions { get; set; }
    }
}
