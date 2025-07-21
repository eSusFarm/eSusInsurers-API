using AutoMapper;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Roles.GetRoles;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using WMS.Models.Roles;
using WMS.Models.Roles.UpdateRole;
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
        var getRolesQuery = new GetRolesQuery();
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

        var result = await _rolesService.GetRoles(getRolesQuery, CancellationToken.None);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetRoles_WithNoRoles_ReturnsNoRoles()
    {
        var getRolesQuery = new GetRolesQuery();
        var roles = new List<Role>();
        var mock = roles.AsQueryable().BuildMockDbSet();
        _roleRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Role, RoleModel>()
                .ForMember(d => d.RoleId, opt => opt.MapFrom(s => s.Id));
        }));

        var result = await _rolesService.GetRoles(getRolesQuery, CancellationToken.None);
        Assert.Equal(0, result.TotalRecordCount);
    }

    [Fact]
    public async Task AddRoles_NullRequest_ThrowsArgumentNullException()
    {
        Func<Task> act = async () => await _rolesService.AddRole(null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }

    [Fact]
    public async Task AddRole_RoleAlreadyExists_ThrowsBadRequestException()
    {
        var request = new RoleRequest { RoleName = "ExistingRole" };
        _roleRepository.Setup(r => r.GetByRoleNameAsync("ExistingRole", It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new Role());

        Func<Task> act = async () => await _rolesService.AddRole(request, CancellationToken.None);
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Role name: (ExistingRole) already exists.");
    }

    [Fact]
    public async Task UpdateRole_WithInvalidId_ThrowsNotFoundException()
    {
        var request = new UpdateRoleRequestModel { RoleName = "NewName" };
        _roleRepository.Setup(r => r.GetByIdAsync(1, null, false, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Role)null);

        Func<Task> act = async () => await _rolesService.UpdateRole(1, request, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Role Id doesn't exist.");
    }

    [Fact]
    public async Task UpdateRole_WithDuplicateName_ThrowsBadRequestException()
    {
        var role = new Role { Id = 1, RoleName = "Role1" };
        var request = new UpdateRoleRequestModel { RoleName = "DuplicateName" };

        _roleRepository.Setup(r => r.GetByIdAsync(1, null, false, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(role);
        _roleRepository.Setup(r => r.GetByRoleNameAsync(1, "DuplicateName", It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new Role { Id = 2, RoleName = "DuplicateName" });

        Func<Task> act = async () => await _rolesService.UpdateRole(1, request, CancellationToken.None);
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Role name: (DuplicateName) already exists.");
    }

    [Fact]
    public async Task RoleDetails_WithNullResult_ThrowsApplicationException()
    {
        int roleId = 1;
        _roleRepository.Setup(x => x.GetRoleDetails(roleId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((SP_GetRoleDetailsResult)null);

        Func<Task> act = async () => await _rolesService.RoleDetails(roleId, CancellationToken.None);
        await act.Should().ThrowAsync<ApplicationException>();
    }
} 
