using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class ProgramsRepository : Repository<Program>, IProgramRepository
    {
        public ProgramsRepository(DbContext context) : base(context)
        {
            
        }

        public async Task<Program?> GetByProgarmAndInstitutionNameAsync(string programName, string institutionName, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.ProgramName == programName && x.InstitutionName == institutionName, cancellationToken);
        }
    }
}
