using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Repositories;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories;

[TestSubject(typeof(CropInsuranceReposity))]
public class CropInsuranceReposityTest
{

    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly DbContext _dbContext;
    private readonly CropInsuranceReposity _cropInsuranceReposity;

    public CropInsuranceReposityTest()
    {
        var options = new DbContextOptionsBuilder<eSusInsurerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(x => x.CropInsuranceRepository).Returns(_cropInsuranceReposity);
        _dbContext = new eSusInsurerContext(options);
        _cropInsuranceReposity = new CropInsuranceReposity(_dbContext);
    }

    [Fact]
    public async Task AddCropInsurance_ShouldAddCropInsuranceSuccessfully()
    {
        var cropInsurance = GetTestCropInsurance();
        var createdCropInsurance = await _cropInsuranceReposity.AddAsync(cropInsurance, CancellationToken.None);
        Assert.NotNull(createdCropInsurance);
        Assert.Equal(cropInsurance.CropName, createdCropInsurance.CropName);
    }

    private CropInsurance GetTestCropInsurance()
    {
        return new CropInsurance
        {
            Id = 1,
            Comments = "Test comment",
            CreatedBy = DateTime.Now.ToString(),
            CreatedDate = DateTime.Now,
            CropName = "Test crop",
            Longitude = 808080,
            Latitude = 909090,
            InsurancePolicyId = 123,
            InsuranceRiskId = 13,
            Status = "active",
            IsActive = true
        };
    }
}