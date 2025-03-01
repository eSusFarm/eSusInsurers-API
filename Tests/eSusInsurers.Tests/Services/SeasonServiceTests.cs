using AutoMapper;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Countries;
using eSusInsurers.Models.Seasons;
using eSusInsurers.Services;
using eSusInsurers.Services.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using MockQueryable.Moq;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;
using Times = Moq.Times;

namespace eSusInsurers.Tests.Services;

public class SeasonServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<ISeasonRepository> _seasonRepository;
    private readonly ISeasonService _service;
    private readonly IConfigurationProvider _mapperConfig;
    private readonly Mock<IMapper> _mapper;
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly IMapper _fakeMapper;
    private readonly ISeasonService _fakeSeasonService;
    
    
    public SeasonServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _seasonRepository = new Mock<ISeasonRepository>();
        _mapper = new Mock<IMapper>();
        _unitOfWork.Setup(x => x.SeasonRepository).Returns(_seasonRepository.Object);
        _service = new SeasonService(_unitOfWork.Object, _mapper.Object);
        
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _fakeMapper = A.Fake<IMapper>();
        _fakeSeasonService = new SeasonService(_fakeUnitOfWork, _fakeMapper);
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

    [Fact]
    public async Task AddSeason_NullSeason_ReturnsException()
    {
        // Act
        Func<Task> act = async () => await  _service.AddSeason(null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }
    
    [Fact]
    public async Task AddSeason_SeasonAlreadyExists_ReturnsException()
    {
        SeasonRequest seasonRequest = new SeasonRequest
        {
            SeasonName = "Season 1",
            SeasonYear = "2025",
        };
        // Arrange
        var season = new Season
            { Id = 1, SeasonYear = "2025", SeasonName = "Season 1", IsActive = true };
        _seasonRepository.Setup(x => x.GetBySeasonNameAsync(seasonRequest.SeasonName,seasonRequest.SeasonYear, new CancellationToken())).Returns(Task.FromResult(season));
        // Act
        Func<Task> act = async () => await  _service.AddSeason(seasonRequest, new CancellationToken());
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Season name: (Season 1) already exists.");
    }
    
    [Fact]
    public async Task UpdateSeason_NullSeason_ReturnsException()
    {
        // Act
        Func<Task> act = async () => await  _service.UpdateSeason(1, null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }
    
    [Fact]
    public async Task UpdateSeason_NonExistantSeason_ReturnsException()
    {
        UpdateSeasonRequest seasonRequest = new UpdateSeasonRequest
        {
            SeasonName = "Season 20",
            SeasonYear = "2025",
        };
        // Arrange
        var season = new Season
            { Id = 1, SeasonYear = "2025", SeasonName = "Season 1", IsActive = true };
        _seasonRepository.Setup(x => x.GetBySeasonNameAsync(seasonRequest.SeasonName,seasonRequest.SeasonYear, new CancellationToken())).Returns(Task.FromResult(season));
        // Act
        Func<Task> act = async () => await  _service.UpdateSeason(1, seasonRequest, new CancellationToken());
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Season Id doesn't exist.");
    }
    
    [Fact]
    public async Task DeleteSeason_NonExistantSeason_ReturnsException()
    {
        UpdateSeasonRequest seasonRequest = new UpdateSeasonRequest
        {
            SeasonName = "Season 20",
            SeasonYear = "2025",
        };
        // Arrange
        var season = new Season
            { Id = 1, SeasonYear = "2025", SeasonName = "Season 1", IsActive = true };
        _seasonRepository.Setup(x => x.GetBySeasonNameAsync(seasonRequest.SeasonName,seasonRequest.SeasonYear, new CancellationToken())).Returns(Task.FromResult(season));
        // Act
        Func<Task> act = async () => await  _service.DeleteSeason(25, new CancellationToken());
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Season Id: (25) doesn't exist.");
    }
    
    [Fact]
    public async Task ActivateSeason_NonExistantSeason_ReturnsException()
    {
        UpdateSeasonRequest seasonRequest = new UpdateSeasonRequest
        {
            SeasonName = "Season 20",
            SeasonYear = "2025",
        };
        // Arrange
        var season = new Season
            { Id = 1, SeasonYear = "2025", SeasonName = "Season 1", IsActive = true };
        _seasonRepository.Setup(x => x.GetBySeasonNameAsync(seasonRequest.SeasonName,seasonRequest.SeasonYear, new CancellationToken())).Returns(Task.FromResult(season));
        // Act
        Func<Task> act = async () => await  _service.DeleteSeason(25, new CancellationToken());
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Season Id: (25) doesn't exist.");
    }

    private SeasonRequest GetCreateSeasonRequest()
    {
        var seasonRequest = new Filler<SeasonRequest>();
        seasonRequest.Setup()
            .OnProperty(x => x.SeasonName).Use(new RealNames(NameStyle.FirstName))
            .OnProperty(x => x.SeasonYear).Use("2025");
        return seasonRequest.Create();
    }
    
    private static Task<Season> CreateNullTask()
    {
        return null;
    }

}