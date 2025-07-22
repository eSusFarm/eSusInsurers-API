using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class CountriesRepository : Repository<Region>, ICountriesRepository
    {
        public CountriesRepository(DbContext context) : base(context)
        {
            
        }

        public async Task<Region?> GetByRegionName(string regionName, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.RegionName == regionName);
        }
    }
}
