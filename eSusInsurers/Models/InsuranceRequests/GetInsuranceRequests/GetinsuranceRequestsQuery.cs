using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.InsuranceRequests.getInsuranceRequests
{
    /// <summary>
    /// GetinsuranceRequestsQuery
    /// </summary>
    public class GetinsuranceRequestsQuery
    {
        /// <summary>
        /// pagingOptions
        /// </summary>
        public PagingOptions? pagingOptions { get; set; }
        
        /// <summary>
        /// InsuranceRequestFilterOptions
        /// </summary>
        public InsuranceRequestFilterOptions? filter { get; set; }
        
        /// <summary>
        /// SortingOptions
        /// </summary>
        public SortingOptions? sortingOptions { get; set; }
    }
}

