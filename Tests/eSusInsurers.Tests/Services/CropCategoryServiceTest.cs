using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models;
using eSusInsurers.Services;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Services;

public class CropCategoryServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IMapper> _mapper;
    private readonly CropCategoryService _cropCategoryService;
    private readonly Mock<ICropCategoryRepository> _cropCategoryRepository;

    public CropCategoryServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _mapper = new Mock<IMapper>();
        _cropCategoryRepository = new Mock<ICropCategoryRepository>();
        _unitOfWork.Setup(x => x.CropCategoryRepository).Returns(_cropCategoryRepository.Object);
        _cropCategoryService = new CropCategoryService(_unitOfWork.Object, _mapper.Object);
    }
    
    [Fact]
    public async Task GetCropCategory_WithValidCropCategoryId_CropCategoryList()
    {
        // Arrange
        var cropCategories = new List<CropCategory>
        {
            new() { Id = 1,  CropCategoryName = "CropCategory 1" , IsActive=true },
            new() { Id = 2, CropCategoryName = "CropCategory 2" , IsActive=true }
        };
        var mock = cropCategories.AsQueryable().BuildMockDbSet();
        _cropCategoryRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<CropCategory, CropCategoryModel>()
                .ForMember(d => d.CropCategoryId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _cropCategoryService.GetCropCategories(CancellationToken.None);
        // Assert
        Assert.Equal(2, result.Count());
        _cropCategoryRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }
    
    [Fact]
    public async Task GetRegions_WithInValidCountryId_ReturnsNoRegionsList()
    {
        // Arrange
        var cropCategories = new List<CropCategory>();
        
        var mock = cropCategories.AsQueryable().BuildMockDbSet();
        _cropCategoryRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<CropCategory, CropCategoryModel>()
                .ForMember(d => d.CropCategoryId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _cropCategoryService.GetCropCategories(CancellationToken.None);
        // Assert
        Assert.Equal(0, result.Count());
        _cropCategoryRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }
}