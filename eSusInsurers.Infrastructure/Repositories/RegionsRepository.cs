using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class RegionsRepository: Repository<Region>, IRegionsRepository
    {
        public RegionsRepository(DbContext context) : base(context)
        {
            
        }
    }   
}

