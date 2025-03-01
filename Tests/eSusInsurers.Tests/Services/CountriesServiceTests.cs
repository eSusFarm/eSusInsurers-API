using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Infrastructure.Repositories;
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
    private readonly Mock<IDistrictRepository> _districtRepository;
    private readonly Mock<ISubcountiesRepository> _subcountiesRepository;
    private readonly Mock<IParishRepository> _parishRepository;
   

    public CountriesServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _regionsRepository = new Mock<IRegionsRepository>();
        _mapper = new Mock<IMapper>();
        _unitOfWork.Setup(x => x.RegionsRepository).Returns(_regionsRepository.Object);
        _districtRepository = new Mock<IDistrictRepository>();
        _unitOfWork.Setup(x => x.DistrictRepository).Returns(_districtRepository.Object);
        _subcountiesRepository = new Mock<ISubcountiesRepository>();
        _unitOfWork.Setup(x => x.SubcountiesRepository).Returns(_subcountiesRepository.Object);
        _parishRepository = new Mock<IParishRepository>();
        _unitOfWork.Setup(x => x.ParishRepository).Returns(_parishRepository.Object);
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

    [Fact]
    public async Task GetdstrictsWithValidCountryId_ReturnsDistrcitsList()
    {
        //Arrange
        int countryId = 1;
        int regionId = 1;
        var districts = new List<District>
        {
            new ()
            {
                Id = 1,
                DistrictName = "District 1",
                RegionId = regionId,
            }
        };
        var mock = districts.AsQueryable().BuildMockDbSet();
        _districtRepository.Setup(x => x.GetAll(null, true)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<District, DistrictModel>()
                .ForMember(d => d.DistrictId, opt => opt.MapFrom(s => s.Id));
        }));
        //Act
        var result = await _service.GetDistricts(countryId,regionId, CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task GetSubCountiesWithValidCountryId_ReturnsCountyList()
    {
        //Arrange
        int countryId = 1;
        int regionId = 1;
        int districtId = 1;
        int subCountyId = 1;
        var parishes = new List<Parish>
        {
            new ()
            {
                Id = 1,
                ParishName = "Parish 1",
                 SubCountyId= 1,
            }
        };
        var mock = parishes.AsQueryable().BuildMockDbSet();
        _parishRepository.Setup(x => x.GetAll(null, true)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Parish, ParishModel>()
                .ForMember(d => d.ParishId, opt => opt.MapFrom(s => s.Id));
        }));
        //Act
        var result = await _service.GetParishes(countryId,regionId, districtId,subCountyId, CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }
}