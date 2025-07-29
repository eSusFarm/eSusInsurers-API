using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories;

public class CropInsuranceTests
{
    private readonly DbContext _dbContext;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CropInsuranceReposity _cropInsuranceReposity;

    public CropInsuranceTests()
    {
        var options = new DbContextOptionsBuilder<eSusInsurerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _unitOfWork = new Mock<IUnitOfWork>();
        _dbContext = new eSusInsurerContext(options);
        _cropInsuranceReposity = new CropInsuranceReposity(_dbContext);
    }

    [Fact]
    public async Task CropInsurance_ShouldSave()
    {
        var cropInsurance = new CropInsurance
        {
            FarmerId = 1,
            FarmerCropId = 1,
            CropName = "Test"
        };
        var createdCropInsurance =await  _cropInsuranceReposity.AddAsync(cropInsurance, new CancellationToken());
        Assert.NotNull(createdCropInsurance);
        Assert.Equal("Test", createdCropInsurance.CropName);
    }
}