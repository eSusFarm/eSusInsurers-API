using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories;
    public class CropCategoryRepositoryTests
    {
        private readonly DbContext _dbContext;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CropCategoryRepository _cropCategoryRepository;

        public CropCategoryRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<eSusInsurerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _unitOfWork = new Mock<IUnitOfWork>();
            _dbContext = new eSusInsurerContext(options);
            _cropCategoryRepository = new CropCategoryRepository( _dbContext);
        }

        [Fact]
        public async Task AddCropCategory_ShouldAddCategory()
        {
            var cropCategory = new CropCategory
            {
                CropCategoryName = "crop category",
            };
            var createdCrop = await _cropCategoryRepository.AddAsync(cropCategory, CancellationToken.None);
            Assert.NotNull(createdCrop);
            Assert.Equal("crop category", createdCrop.CropCategoryName);
        }
    }


