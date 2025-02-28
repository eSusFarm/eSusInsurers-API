using AutoMapper;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Programs;
using eSusInsurers.Services.Implementations;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Services;

public class ProgramServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IProgramRepository> _programRepository;
    private readonly ProgramService _programService;
    private readonly IConfigurationProvider _mapperConfig;
    private readonly Mock<IMapper> _mapper;

    public ProgramServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _programRepository = new Mock<IProgramRepository>();
        _mapper =new Mock<IMapper>();
        _unitOfWork.Setup(x => x.ProgramRepository).Returns(_programRepository.Object);
        _programService = new ProgramService(_unitOfWork.Object, _mapper.Object);
    }
    
    [Fact]
    public async Task CreateAsync_NullRequest_ShouldThrowArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await  _programService.AddProgram(null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }

    [Fact]
    public async Task CreateAsync_ProgramAlreadyExists_ShouldThrowArgumentException()
    {
        ProgramRequest programRequest = new ProgramRequest
        {
            ProgramName = "Program 1"
        };
        var roles = new List<Program>
        {
            new() { Id = 1, ProgramName = "Program 1", IsActive=true },
        };
        var mock = roles.AsQueryable().BuildMockDbSet();
        _programRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Program, ProgramsModel>()
                .ForMember(d => d.ProgramId, opt => opt.MapFrom(s => s.Id));
        }));
        
        Func<Task> act = async () => await  _programService.AddProgram(programRequest, new CancellationToken());
        await act.Should().ThrowAsync<Exception>().WithMessage("Program Name (Program 1) already exists.");
    }
}