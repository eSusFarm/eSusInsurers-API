namespace eSusInsurers.Models.Common
{
    public class PagedResponse<T>
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public int TotalPages { get; set; }
        public int TotalRecordCount { get; set; }
        public List<T> Records { get; set; } = new();

        public PagedResponse(List<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalRecordCount = count;
            Records = items;
        }
    }
}
