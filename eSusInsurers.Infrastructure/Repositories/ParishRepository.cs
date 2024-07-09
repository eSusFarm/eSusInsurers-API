using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class ParishRepository : Repository<Parish>, IParishRepository
    {
        public ParishRepository(DbContext context) : base(context)
        {
        }
    }
}
