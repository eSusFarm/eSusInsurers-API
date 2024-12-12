using eSusInsurers.Domain.Entities;
using eSusInsurers.Domain.Models;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class EtheriscPolicyRepository: Repository<EtheriscPolicy>, IEtheriscPolicyRepository
    {
        public EtheriscPolicyRepository(DbContext context) : base(context)
        {
        }
    } 
}

