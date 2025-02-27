using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Roles.GetRoles;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using MockQueryable.Moq;
using Moq;
using Xunit;
using IConfigurationProvider = Microsoft.Extensions.Configuration.IConfigurationProvider;

namespace eSusInsurers.Tests.Services;

public class RolesServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IRoleRepository> _roleRepository;
    private readonly RolesService _rolesService;
    private readonly IConfigurationProvider _mapperConfig;
    private readonly Mock<IMapper> _mapper;

    public RolesServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _roleRepository = new Mock<IRoleRepository>();
        _mapper =new Mock<IMapper>();
        _unitOfWork.Setup(x => x.RoleRepository).Returns(_roleRepository.Object);
        _rolesService = new RolesService(_unitOfWork.Object, _mapper.Object);
    }
    
    [Fact]
    public async Task GetRoles_WitRoles_ReturnsRolesList()
    {
        int ReportingToId = 3;
        var getRolesQuery = new GetRolesQuery();
        // Arrange
        var roles = new List<Role>
        {
            new() { Id = 1, RoleName = "_Role 1", IsActive=true, ReportingToId=3 },
            new() { Id = 2,  RoleName = "_Role 2", IsActive=true ,ReportingToId=3}
        };
        var mock = roles.AsQueryable().BuildMockDbSet();
        _roleRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Role, RoleModel>()
                .ForMember(d => d.RoleId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _rolesService.GetRoles(getRolesQuery, CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }  
    
    [Fact]
    public async Task GetRoles_WithNoRoles_ReturnsNoRoles()
    {
        int ReportingToId = 5;
        var getRolesQuery = new GetRolesQuery();
        // Arrange
        var roles = new List<Role>();
        var mock = roles.AsQueryable().BuildMockDbSet();
        _roleRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Role, RoleModel>()
                .ForMember(d => d.RoleId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _rolesService.GetRoles(getRolesQuery, CancellationToken.None);
        // Assert
        Assert.Equal(0, result.TotalRecordCount);
    }

}