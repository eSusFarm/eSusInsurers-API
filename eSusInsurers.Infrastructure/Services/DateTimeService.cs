using eSusInsurers.Infrastructure.Interfaces;

namespace eSusInsurers.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
