using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class InsurancePoliciesRepository: Repository<InsurancePolicy>, IInsurancePoliciesRepository
    {
        public InsurancePoliciesRepository(DbContext context) : base(context)
        {
        }
    }
}

