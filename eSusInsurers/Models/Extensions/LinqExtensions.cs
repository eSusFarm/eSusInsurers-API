using eSusInsurers.Models.Common;

namespace eSusInsurers.Models.Extensions
{
    public static class LinqExtensions
    {
        public static PagedResponse<T> ToPagedResult<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var count = query.Count();
            var items = pageSize == 0 ? query.ToList() : query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResponse<T>(items, count, pageNumber, pageSize);
        }

    }
}
