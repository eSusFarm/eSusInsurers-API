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
    public class CountriesRepository : Repository<Country>, ICountriesRepository
    {
        public CountriesRepository(DbContext context) : base(context)
        {
            
        }
    }
}
