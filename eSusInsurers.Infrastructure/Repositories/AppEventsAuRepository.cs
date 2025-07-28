using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories;

public class AppEventsAuRepository: Repository<AppEventsAu>, IAppEventsAuRepository
{
    public AppEventsAuRepository(DbContext context) : base(context)
    {
    }
}