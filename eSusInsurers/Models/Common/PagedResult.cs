namespace eSusInsurers.Models.Common;

public class PagedResult<T>
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public int TotalPages { get; set; }
    public int TotalRecordCount { get; set; }
    public List<T> Records { get; set; } = new();


    public static PagedResult<T> Empty =>
        new()
        {
            CurrentPage = 1,
            PageSize = 25,
            Records = new List<T>(),
            TotalPages = 1,
            TotalRecordCount = 0
        };
}