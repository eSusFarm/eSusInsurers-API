using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories;

public class FarmerRepository: Repository<Farmer>, IFarmerRepository
{
    public FarmerRepository(DbContext context) : base(context)
    {
    }
}