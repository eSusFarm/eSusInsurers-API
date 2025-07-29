
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
        
        public async Task<Crop?> GetCropByName(string cropName, CancellationToken cancellationToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.CropName.ToLower() == cropName.ToLower(), cancellationToken);
        }
    }
}