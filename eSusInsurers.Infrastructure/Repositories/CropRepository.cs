using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure
{
    public class CropRepository : Repository<Crop>, ICropRepository
    {
        public CropRepository(DbContext context) : base(context)
        {

        }
    }
}