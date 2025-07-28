using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using PremiumPayment = eSusInsurers.Domain.Entities.PremiumPayment;

namespace eSusInsurers.Infrastructure.Repositories;

public class PremiumPaymentsRepository:  Repository<InsurancePremiumPayment>,IPremiumPaymentsRepository
{
    public PremiumPaymentsRepository(DbContext context) : base(context)
    {
    }
}