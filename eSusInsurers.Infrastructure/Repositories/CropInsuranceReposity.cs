using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class CropInsuranceReposity: Repository<CropInsurance>, ICropInsuranceRepository
    {
        public CropInsuranceReposity(DbContext context) : base(context)
        {
        }
    }
}

