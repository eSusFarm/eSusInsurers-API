using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Repositories;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories;

[TestSubject(typeof(DistrictRepository))]
public class DistrictRepositoryTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly DbContext _dbContext;
    private readonly DistrictRepository _districtRepository;

    public DistrictRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<eSusInsurerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(x => x.DistrictRepository).Returns(_districtRepository);
        _dbContext = new eSusInsurerContext(options);
        _districtRepository = new DistrictRepository(_dbContext);
    }


    [Fact]
    public async Task CreateDistrict_ShouldReturnTrue_WhenDistrictExists()
    {
        var district = CreateDistrict();
        var createdDistrict = await _districtRepository.AddAsync(district, CancellationToken.None);
        Assert.NotNull(createdDistrict);
        Assert.Equal(district.DistrictName, createdDistrict.DistrictName);
    }

    private District CreateDistrict()
    {
        return new District
        {
            Id = 1,
            CreatedDate = DateTime.Now,
            CreatedBy = "Test",
            DistrictName = "Test",
            ModifiedDate = DateTime.Now,
            ModifiedBy = "Test"
        };
    }
}