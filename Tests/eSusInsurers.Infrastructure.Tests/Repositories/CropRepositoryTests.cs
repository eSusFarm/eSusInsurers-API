using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories;

public class CropRepositoryTests
{
    private readonly DbContext _dbContext;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CropRepository _cropRepository;
    private readonly CropCategoryRepository _cropCategoryRepository;

    public CropRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<eSusInsurerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _unitOfWork = new Mock<IUnitOfWork>();
        _dbContext = new eSusInsurerContext(options);
        _cropRepository = new CropRepository(_dbContext);
        _cropCategoryRepository = new CropCategoryRepository(_dbContext);
        _unitOfWork.Setup(x => x.CropRepository).Returns(_cropRepository);
        _unitOfWork.Setup(x => x.CropCategoryRepository).Returns(_cropCategoryRepository);
    }

    [Fact]
    public async Task AddCrop_ShouldAddCrop()
    {
        var CropCategory = await createCropCategory();
        var Crop = createCrop("Test Crop", CropCategory);
        var createdCrop = await _cropRepository.AddAsync(Crop, new CancellationToken(true));
        Assert.NotNull(createdCrop);
        Assert.Equal(Crop.CropName, createdCrop.CropName);
    }
    
    private Crop createCrop(String cropName, CropCategory cropCategory)
    {
        return new Crop
        {
            CropName = "TestCrop",
            CropCategory = cropCategory,
            CropCategoryId = cropCategory.Id
        };
    }

    private async Task<CropCategory> createCropCategory()
    {
        var CropCategory =
            await _cropCategoryRepository.AddAsync(new CropCategory { CropCategoryName = "Test Category" },
                new CancellationToken(true));
        return CropCategory;
    }
}