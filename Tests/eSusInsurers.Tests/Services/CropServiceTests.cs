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
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Services
{
    public class CropServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ICropRepository> _CropRepository;
        private readonly CropService _cropService;
        private readonly IConfigurationProvider _mapperConfig;
        private readonly Mock<IMapper> _mapper;

        public CropServiceTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _CropRepository = new Mock<ICropRepository>();
            _mapper = new Mock<IMapper>();
            _cropService = new CropService(_unitOfWork.Object, _mapper.Object);
            _unitOfWork.Setup(x => x.CropRepository).Returns(_CropRepository.Object);
        }
        
        [Fact]
        public async Task GetCrop_WithValidCropId_ReturnsCrop()
        {
            // Arrange
            int cropCategoryId = 1;
            var crops = new List<Crop>
            {
                new() { Id = 1, CropName = "Crop 1", CropCategoryId = cropCategoryId}
            };

            var mock = crops.AsQueryable().BuildMockDbSet();
            _CropRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        
            _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Crop, CropModel>()
                    .ForMember(d => d.CropId, opt => opt.MapFrom(s => s.Id));
            }));

            // Act
            var result = await _cropService.GetCropsByCropCategoryId(cropCategoryId, CancellationToken.None);

            // Assert
            Assert.Equal(0,result.Count);
            _CropRepository.Verify(x => x.GetAll(null, false), Times.Once());
        }
        
        [Fact]
        public async Task GetCrop_WithVInalidCropId_ReturnsNoCrop()
        {
            // Arrange
            int cropCategoryId = 5;
            var crops = new List<Crop>
            {
                new() { Id = 1, CropName = "Crop 1", CropCategoryId = cropCategoryId}
            };

            var mock = crops.AsQueryable().BuildMockDbSet();
            _CropRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        
            _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Crop, CropModel>()
                    .ForMember(d => d.CropId, opt => opt.MapFrom(s => s.Id));
            }));

            // Act
            var result = await _cropService.GetCropsByCropCategoryId(cropCategoryId, CancellationToken.None);

            // Assert
            Assert.Equal(0,result.Count);
            _CropRepository.Verify(x => x.GetAll(null, false), Times.Once());
        }

    }
}