using eSusInsurers.Domain.Entities;
using eSusInsurers.Domain.Models;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class EtheriscPolicyRepository: Repository<EtheriscPolicy>, IEtheriscPolicyRepository
    {
        public readonly DbContext _dbContext;
        public EtheriscPolicyRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<EtheriscPolicy> GetByPolicyNumber(string PolicyNumber, CancellationToken cancellationToken)
        {
            return await GetAll(new string[]{"policyNumber", "policyId", "personId", "locationId", "configId", "riskId"}).FirstOrDefaultAsync(x=> x.policyNumber == PolicyNumber, cancellationToken);
        }
    } 
}

