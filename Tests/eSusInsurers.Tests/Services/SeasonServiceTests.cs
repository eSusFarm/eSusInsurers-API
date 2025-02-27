using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Countries;
using eSusInsurers.Models.Seasons;
using eSusInsurers.Services;
using eSusInsurers.Services.Interfaces;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Services;

public class SeasonServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<ISeasonRepository> _seasonRepository;
    private readonly ISeasonService _service;
    private readonly IConfigurationProvider _mapperConfig;
    private readonly Mock<IMapper> _mapper;


    public SeasonServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _seasonRepository = new Mock<ISeasonRepository>();
        _mapper = new Mock<IMapper>();
        _unitOfWork.Setup(x => x.SeasonRepository).Returns(_seasonRepository.Object);
        _service = new SeasonService(_unitOfWork.Object, _mapper.Object);
    }
    
    [Fact]
    public async Task GetSeasonsByYear_ValidYear_ReturnsSeasons()
    {
        // Arrange
        string SeasonYear = "2025";
        var seasons = new List<Season>
        {
            new() { Id = 1, SeasonYear = "2025", SeasonName = "Season 1" , IsActive = true},
        };

        var mock = seasons.AsQueryable().BuildMockDbSet();
        _seasonRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Season, SeasonModel>()
                .ForMember(d => d.SeasonId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetSeasonByYear(SeasonYear, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _seasonRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }
    [Fact]
    public async Task GetSeasonsByYear_InValidYear_ReturnsNoSeasons()
    {
        // Arrange
        string SeasonYear = "2026";
        var seasons = new List<Season>
        {
            new() { Id = 1, SeasonYear = "2025", SeasonName = "Season 1" , IsActive = true},
        };

        var mock = seasons.AsQueryable().BuildMockDbSet();
        _seasonRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Season, SeasonModel>()
                .ForMember(d => d.SeasonId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetSeasonByYear(SeasonYear, CancellationToken.None);

        // Assert
        Assert.Equal(0, result.Count);
        _seasonRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }
    
    [Fact]
    public async Task GetSeasons_ValidSeasons_ReturnsSeasons()
    {

        var GetSeasonQuery = new GetSeasonQuery();
        
        // Arrange
        var seasons = new List<Season>
        {
            new() { Id = 1, SeasonYear = "2025", SeasonName = "Season 1" , IsActive = true},
        };

        var mock = seasons.AsQueryable().BuildMockDbSet();
        _seasonRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Season, SeasonModel>()
                .ForMember(d => d.SeasonId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetSeasons (GetSeasonQuery, CancellationToken.None);

        // Assert
        Assert.Equal(1, result.TotalRecordCount);
        _seasonRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }
    
    [Fact]
    public async Task GetSeasons_InValidSeasons_ReturnsSeasons()
    {

        var GetSeasonQuery = new GetSeasonQuery();
        
        // Arrange
        var seasons = new List<Season>();

        var mock = seasons.AsQueryable().BuildMockDbSet();
        _seasonRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Season, SeasonModel>()
                .ForMember(d => d.SeasonId, opt => opt.MapFrom(s => s.Id));
        }));

        // Act
        var result = await _service.GetSeasons (GetSeasonQuery, CancellationToken.None);

        // Assert
        Assert.Equal(0, result.TotalRecordCount);
        _seasonRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }
}