using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailIdAsync(string emailId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[] { "Role", "Insurer" }).FirstOrDefaultAsync(x => x.EmailId == emailId, cancellationToken);
        }

        public async Task<User?> GetByEmailIdNotUserIdAsync(int userId, string emailId, CancellationToken cancellationToken)
        {
            return await GetAll(new string[] { "Role", "Insurer" }).FirstOrDefaultAsync(x =>x.Id != userId && x.EmailId == emailId, cancellationToken);
        }
    }
}
