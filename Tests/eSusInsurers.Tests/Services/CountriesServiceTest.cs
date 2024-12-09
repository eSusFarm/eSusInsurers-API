using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Countries;
using eSusInsurers.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Services;

public class CountriesServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IMapper> _mapper;
    private readonly CountriesService _service;
    private readonly Mock<ICountriesRepository> _countriesRepository;
    private readonly Mock<IDistrictRepository> _districtRepository;
    private readonly Mock<ISubcountiesRepository> _subcountiesRepository;
    private readonly Mock<IParishRepository> _parishRepository;

    public CountriesServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _mapper = new Mock<IMapper>();
        _countriesRepository = new Mock<ICountriesRepository>();
        _districtRepository = new Mock<IDistrictRepository>();
        _subcountiesRepository = new Mock<ISubcountiesRepository>();
        _parishRepository = new Mock<IParishRepository>();

        _unitOfWork.Setup(x => x.CountriesRepository).Returns(_countriesRepository.Object);
        _unitOfWork.Setup(x => x.DistrictRepository).Returns(_districtRepository.Object);
        _unitOfWork.Setup(x => x.SubcountiesRepository).Returns(_subcountiesRepository.Object);
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
        _countriesRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Region, RegionModel>()
                .ForMember(d => d.RegionId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetRegions(countryId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _countriesRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }

    [Fact]
    public async Task GetDistricts_WithValidIds_ReturnsDistrictsList()
    {
        // Arrange
        int countryId = 1;
        int regionId = 1;
        var districts = new List<District>
        {
            new() { Id = 1, RegionId = regionId, DistrictName = "District 1", Region = new Region { CountryId = countryId } },
            new() { Id = 2, RegionId = regionId, DistrictName = "District 2", Region = new Region { CountryId = countryId } }
        };

        var mock = districts.AsQueryable().BuildMockDbSet();
        _districtRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());

        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<District, DistrictModel>()
                .ForMember(d => d.DistrictId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetDistricts(countryId, regionId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _districtRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }

    [Fact]
    public async Task GetSubcounties_WithValidIds_ReturnsSubcountiesList()
    {
        // Arrange
        int countryId = 1;
        int regionId = 1;
        int districtId = 1;
        var subcounties = new List<SubCounty>
        {
            new() 
            { 
                Id = 1, 
                DistrictId = districtId, 
                SubCountyName = "SubCounty 1",
                District = new District 
                { 
                    RegionId = regionId,
                    Region = new Region { CountryId = countryId } 
                }
            }
        };

        var mock = subcounties.AsQueryable().BuildMockDbSet();
        _subcountiesRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());

        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<SubCounty, SubCountiesModel>()
                .ForMember(d => d.SubCountyId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetSubcounties(countryId, regionId, districtId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _subcountiesRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }

    [Fact]
    public async Task GetParishes_WithValidIds_ReturnsParishesList()
    {
        // Arrange
        int countryId = 1;
        int regionId = 1;
        int districtId = 1;
        int subCountyId = 1;
        var parishes = new List<Parish>
        {
            new() 
            { 
                Id = 1,
                ParishName = "Parish 1",
                SubCountyId = subCountyId,
                SubCounty = new SubCounty 
                { 
                    DistrictId = districtId,
                    District = new District 
                    { 
                        RegionId = regionId,
                        Region = new Region { CountryId = countryId } 
                    }
                }
            }
        };

        var mock = parishes.AsQueryable().BuildMockDbSet();
        _parishRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());

        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Parish, ParishModel>()
                .ForMember(d => d.ParishId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetParishes(countryId, regionId, districtId, subCountyId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _parishRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }
}