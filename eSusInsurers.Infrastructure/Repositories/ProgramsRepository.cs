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
    public class ProgramsRepository : Repository<eSusInsurers.Domain.Entities.Program>, IProgramRepository
    {
        public ProgramsRepository(DbContext context) : base(context)
        {
            
        }

        public async Task<eSusInsurers.Domain.Entities.Program?> GetByProgarmAndInstitutionNameAsync(string programName, string institutionName, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.ProgramName == programName && x.InstitutionName == institutionName, cancellationToken);
        }
    }
}
