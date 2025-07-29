using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories;

public class AppEventRepository: Repository<AppEvent> ,IAppEventRepository
{
    public AppEventRepository(DbContext context) : base(context)
    {
    }
}