namespace eSusInsurers.Models.Common
{
    /// <summary>
    /// Paginations options for response.
    /// </summary>
    public class PagingOptions
    {
        /// <summary>
        /// Desired page of results.
        /// </summary>
        /// <example>1</example>
        public int? Page { get; set; } = 1;

        /// <summary>
        /// Desired number of results per page.
        /// </summary>
        /// <example>25</example>
        public int? PageSize { get; set; } = 0;

        public PagingOptions()
        {
        }

        public PagingOptions(int? page, int? pageSize)
        {
            Page = page < 1 ? 1 : page;
            PageSize = pageSize < 0 ? 25 : pageSize;
        }
    }
}
