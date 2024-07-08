using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSusInsurers.Infrastructure.Interfaces
{
    public interface IProgramRepository : IRepository<eSusInsurers.Domain.Entities.Program>
    {
        Task<eSusInsurers.Domain.Entities.Program?> GetByProgarmAndInstitutionNameAsync(string programName, string institutionName, CancellationToken cancellationToken);
    }
}
