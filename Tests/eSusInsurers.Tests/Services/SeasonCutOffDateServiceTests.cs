using AutoMapper;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Countries;
using eSusInsurers.Models.SeasonCutOffDate;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using WMS.Models.Roles;
using Xunit;
using IConfigurationProvider = Microsoft.Extensions.Configuration.IConfigurationProvider;

namespace eSusInsurers.Tests.Services;

public class SeasonCutOffDateServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<ISeasonCutOffDateRepository> _seasonCutOffDateRepository;
    private readonly ISeasonCutOffDateService _seasonCutOffDateService;
    private readonly IConfigurationProvider _mapperConfig;
    private readonly Mock<IMapper> _mapper;

    public SeasonCutOffDateServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _seasonCutOffDateRepository = new Mock<ISeasonCutOffDateRepository>();
        _mapper = new Mock<IMapper>();
        _unitOfWork.Setup(x => x.SeasonCutOffDateRepository).Returns(_seasonCutOffDateRepository.Object);
        _seasonCutOffDateService = new SeasonCutOffDateService(_unitOfWork.Object, _mapper.Object);
    }
    
    [Fact]
    public async Task GetSeasonCutoffDateById_WithValidSeasonCutoffId_ReturnsSeasonCutoffList()
    {
        // Arrange
        int seasonId = 1;
        var seasonCutOffDates = new List<SeasonCutOffDate>
        {
            new() { Id = 1, SeasonId = 1,RegionId =1,  CropCategoryId= 1, CropId=1 ,IsActive=true},
            new() { Id = 2, SeasonId = 1,RegionId =1,  CropCategoryId= 1, CropId=1 ,IsActive=true},
        };
        var mock = seasonCutOffDates.AsQueryable().BuildMockDbSet();
        _seasonCutOffDateRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<SeasonCutOffDate, SeasonCutOffDatesModel>()
                .ForMember(d => d.SeasonId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _seasonCutOffDateService.GetSeasonCutOffDatesById(seasonId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _seasonCutOffDateRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }
    
    [Fact]
    public async Task GetSeasonCutoffDateById_WithInValidSeasonCutoffId_ReturnsSeasonNothing()
    {
        // Arrange
        int seasonId = 1;
        var regions = new List<SeasonCutOffDate>();
        var mock = regions.AsQueryable().BuildMockDbSet();
        _seasonCutOffDateRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<SeasonCutOffDate, SeasonCutOffDatesModel>()
                .ForMember(d => d.SeasonId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _seasonCutOffDateService.GetSeasonCutOffDatesById(seasonId, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _seasonCutOffDateRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }

    [Fact]
    public async Task GetSeasonCutOffDates_WithValidSeasonCutoffDateQ_ReturnsSeasonCutOffDatesList()
    {
        // Arrange
        var getSeasonCutOffDatesQuery = new GetSeasonCutOffDatesQuery();
        var seasonCutOffDates = new List<SeasonCutOffDate>
        {
            new() { Id = 1, SeasonId = 1,RegionId =1,  CropCategoryId= 1, CropId=1 ,IsActive=true},
            new() { Id = 2, SeasonId = 1,RegionId =1,  CropCategoryId= 1, CropId=1 ,IsActive=true},
        };
        var mock = seasonCutOffDates.AsQueryable().BuildMockDbSet();
        _seasonCutOffDateRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<SeasonCutOffDate, SeasonCutOffDatesModel>()
                .ForMember(d => d.SeasonId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _seasonCutOffDateService.GetSeasonCutOffDates(getSeasonCutOffDatesQuery, CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task GetSeasonCutOffDates_WithNoSeasonCutoffDateQ_ReturnsEmptySeasonCutOffDatesList()
    {
        // Arrange
        var getSeasonCutOffDatesQuery = new GetSeasonCutOffDatesQuery();
        var seasonCutOffDates = new List<SeasonCutOffDate>();
        var mock = seasonCutOffDates.AsQueryable().BuildMockDbSet();
        _seasonCutOffDateRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<SeasonCutOffDate, SeasonCutOffDatesModel>()
                .ForMember(d => d.SeasonId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _seasonCutOffDateService.GetSeasonCutOffDates(getSeasonCutOffDatesQuery, CancellationToken.None);
        // Assert
        Assert.Equal(0, result.TotalRecordCount);
    }
    
    [Fact]
    public async Task AddSeasonCutOff_NullRequest_ThrowsArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await  _seasonCutOffDateService.AddSeasonCutOffDates(null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }
    
    [Fact]
    public async Task AddSeasonCutOff_SeasonCutOffAlreadyExists_ReturnsException()
    {

        var expected = new SeasonCutOffDate
        {
            SeasonId = 1,
            RegionId = 1,
            CropCategoryId = 1,
            CropId = 1
        };

        var requests = new List<SeasonCutOffDatesRequest>
        {
            new SeasonCutOffDatesRequest
            {
                SeasonId = 1,
                RegionId = 1,
                CropCategoryId = 1,
                CropId = 1
            }
        };
       
        // Arrange
        var seasonCutOffDates = new List<SeasonCutOffDate>
        {
            new() { Id = 1, SeasonId = 1,RegionId =1,  CropCategoryId= 1, CropId=1 ,IsActive=true},
            new() { Id = 2, SeasonId = 1,RegionId =1,  CropCategoryId= 1, CropId=1 ,IsActive=true},
        };
        var mock = seasonCutOffDates.AsQueryable().BuildMockDbSet();
        _seasonCutOffDateRepository.Setup(x => x.GetBySeasonCutOffDateAsync(1,1, 1, 1, new CancellationToken())).ReturnsAsync(expected);

        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<SeasonCutOffDate, SeasonCutOffDatesModel>()
                .ForMember(d => d.SeasonId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        Func<Task> act = async () => await  _seasonCutOffDateService.AddSeasonCutOffDates(requests, new CancellationToken());
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Request already exists.");
    }

}