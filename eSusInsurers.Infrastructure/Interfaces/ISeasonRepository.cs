using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure
{
    public interface ISeasonRepository : IRepository<Season>
    {
        Task<Season?> GetBySeasonNameAsync(string seasonName, string seasonYear, CancellationToken cancellationToken);
        Task<Season?> GetBySeasonNameAsync(int seasonId, string seasonName, string seasonYear, CancellationToken cancellationToken);
    }
}
