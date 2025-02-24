using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models;
using eSusInsurers.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Services
{
    public class CropServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ICropRepository> _mockCropRepository;
        private readonly CropService _cropService;
        private readonly IConfigurationProvider _mapperConfig;

        public CropServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCropRepository = new Mock<ICropRepository>();
            
            // Create mapper configuration
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Crop, CropModel>()
                    .ForMember(dest => dest.CropId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.CropName, opt => opt.MapFrom(src => src.CropName))
                    .ForMember(dest => dest.CropCategoryId, opt => opt.MapFrom(src => src.CropCategoryId));
            });
            
            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.ConfigurationProvider).Returns(_mapperConfig);
            
            _cropService = new CropService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetCropsByCropCategoryId_ReturnsFilteredCrops()
        {
            // Arrange
            const int cropCategoryId = 1;
            var crops = new List<Crop>
            {
                new Crop { Id = 1, CropName = "Wheat", CropCategoryId = 1, IsActive = true },
                new Crop { Id = 2, CropName = "Corn", CropCategoryId = 1, IsActive = true },
                new Crop { Id = 3, CropName = "Rice", CropCategoryId = 2, IsActive = true },
                new Crop { Id = 4, CropName = "Barley", CropCategoryId = 1, IsActive = false }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Crop>>();
            mockDbSet.As<IQueryable<Crop>>().Setup(m => m.Provider).Returns(crops.Provider);
            mockDbSet.As<IQueryable<Crop>>().Setup(m => m.Expression).Returns(crops.Expression);
            mockDbSet.As<IQueryable<Crop>>().Setup(m => m.ElementType).Returns(crops.ElementType);
            mockDbSet.As<IQueryable<Crop>>().Setup(m => m.GetEnumerator()).Returns(crops.GetEnumerator());

            _mockCropRepository
                .Setup(r => r.GetAll(It.IsAny<string[]>(), It.IsAny<bool>()))
                .Returns(mockDbSet.Object);

            _mockUnitOfWork
                .Setup(uow => uow.CropRepository)
                .Returns(_mockCropRepository.Object);

            // Act
            var result = await _cropService.GetCropsByCropCategoryId(cropCategoryId, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.All(resultList, crop => Assert.Equal(cropCategoryId, crop.CropCategoryId));
            Assert.Contains(resultList, c => c.CropName == "Wheat" && c.CropId == 1);
            Assert.Contains(resultList, c => c.CropName == "Corn" && c.CropId == 2);
        }
    }
}