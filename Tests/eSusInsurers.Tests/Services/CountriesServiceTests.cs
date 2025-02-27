using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Countries;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using MockQueryable.Moq;
using Moq;
using Xunit;
namespace eSusInsurers.Tests.Services;

public class CountriesServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IRegionsRepository> _regionsRepository;
    private readonly ICountriesService _service;
    private readonly IConfigurationProvider _mapperConfig;
    private readonly Mock<IMapper> _mapper;
    
   

    public CountriesServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _regionsRepository = new Mock<IRegionsRepository>();
        _mapper = new Mock<IMapper>();

        _unitOfWork.Setup(x => x.RegionsRepository).Returns(_regionsRepository.Object);
        _service = new CountriesService(_unitOfWork.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetRegions_WithValidCountryId_ReturnsRegionsList()
    {
        // Arrange
        int countryId = 1;
        var regions = new List<Region>
        {
            new() { Id = 1, CountryId = countryId, RegionName = "Region 1" },
            new() { Id = 2, CountryId = countryId, RegionName = "Region 2" }
        };

        var mock = regions.AsQueryable().BuildMockDbSet();
        _regionsRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Region, RegionModel>()
                .ForMember(d => d.RegionId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetRegions(countryId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _regionsRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }
    
    [Fact]
    public async Task GetRegions_WithInValidCountryId_ReturnsNoRegionsList()
    {
        // Arrange
        int countryId = 1;
        var regions = new List<Region>
        {
            new() { Id = 1, CountryId = 5, RegionName = "Region 1" },
            new() { Id = 2, CountryId = 5, RegionName = "Region 2" }
        };

        var mock = regions.AsQueryable().BuildMockDbSet();
        _regionsRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Region, RegionModel>()
                .ForMember(d => d.RegionId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetRegions(countryId, CancellationToken.None);
        // Assert
        Assert.Equal(0, result.Count);
    }
}