using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure
{
    public class CropCategoryRepository : Repository<CropCategory>, ICropCategoryRepository
    {
        public CropCategoryRepository(DbContext context) : base(context)
        {

        }
    }
}
