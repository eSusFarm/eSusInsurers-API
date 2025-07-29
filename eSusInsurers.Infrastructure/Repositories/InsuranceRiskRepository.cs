using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class InsuranceRiskRepository: Repository<InsuranceRisk>, Interfaces.IInsuranceRiskRepository
    {
        public InsuranceRiskRepository(DbContext context) : base(context)
        {
        }
    }
}

