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
    }
}
