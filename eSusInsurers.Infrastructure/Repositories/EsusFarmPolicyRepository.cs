using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class EsusFarmPolicyRepository : Repository<EsusFarmPolicy>, IEsusFarmPolicyRepository
    {
        public EsusFarmPolicyRepository(DbContext context) : base(context)
        {
        }
    }
}