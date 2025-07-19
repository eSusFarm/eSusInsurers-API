using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories
{
    public class CountriesRepositoryTests
    {

        private readonly DbContext _dbContext;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CountriesRepository _repository;

        public CountriesRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<eSusInsurerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _unitOfWork = new Mock<IUnitOfWork>();
            _dbContext = new eSusInsurerContext(options);
            _repository = new CountriesRepository(_dbContext);
        }
} 
}

