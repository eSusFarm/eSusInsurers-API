using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailIdAsync(string userName, CancellationToken cancellationToken);
    }
}
