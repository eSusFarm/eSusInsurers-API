using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class InsuranceRequestsRepository : Repository<InsuranceRequest>, IInsuranceRequestsRepository
    {
        public InsuranceRequestsRepository(DbContext context) : base(context)
        {
        }
    }
}

