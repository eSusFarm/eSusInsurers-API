using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories;

public class FarmerCroprepository :Repository<FarmerCrop>, IFarmerCroprepository
{
    public FarmerCroprepository(DbContext context) : base(context)
    {
    }
}