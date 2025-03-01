using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceProducts;
using eSusInsurers.Models.Users.GetUsers;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MockQueryable.Moq;
using Moq;
using Xunit;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace eSusInsurers.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly UserService _userService;
    private readonly Mock<IDateTime> _dateTimeMock;
    private readonly Mock<IEmailService> _emailService;
    private readonly Mock<ITokenService> _tokenService;
    private readonly Mock<IConfiguration> _configuration;
    
    
    public UserServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _userRepository = new Mock<IUserRepository>();
        _mapper =new Mock<IMapper>();
        _unitOfWork.Setup(x => x.UserRepository).Returns(_userRepository.Object);
        _dateTimeMock = new Mock<IDateTime>();
        _emailService = new Mock<IEmailService>();
        _configuration = new Mock<IConfiguration>();
        _configuration.Setup(x => x.GetSection("PasswordHashPepper").Value).Returns("token");
        _tokenService = new Mock<ITokenService>();
        _userService = new UserService(_unitOfWork.Object, _configuration.Object, _mapper.Object,_tokenService.Object, _dateTimeMock.Object, _emailService.Object);
    }

    [Fact]
    public async Task Register_NullRequest_ThrowsArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await  _userService.Register(null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }
    
    [Fact]
    public async Task Register_ExistingUser_ThrowsException()
    {
        UserRegisterRequest userRegisterRequest = new UserRegisterRequest
        {
            FirstName = "Testy",
            LastName = "Test",
            Gender = "Male",
            EmailId = "testy.test@gmail.com"
        };
        
        var testEmail = "testy.test@gmail.com";
        var existingUser = new User
        {
            FirstName = "Testy",
            LastName = "Test",
            Gender = "Male",
            EmailId = "testy.test@gmail.com",
        };
        _userRepository.Setup(x => x.GetByEmailIdAsync(userRegisterRequest.EmailId, CancellationToken.None)).Returns(Task.FromResult<User?>(existingUser));
        // Act
        Func<Task> act = async () => await  _userService.Register(userRegisterRequest, new CancellationToken());
        await act.Should().ThrowAsync<Exception>().WithMessage("(testy.test@gmail.com) already exists.");
    }

    [Fact]
    public async Task CheckUsername_UserDoesNotExist_ThrowException()
    {
        var testEmail = "testy2ghhhh.test@gmail.com";
        _userRepository.Setup(x => x.GetByEmailIdAsync(testEmail, CancellationToken.None)).Returns(Task.FromResult<User?>(null));
        // Act
        Func<Task> act = async () => await  _userService.CheckUsername(testEmail, new CancellationToken());
        await act.Should().ThrowAsync<Exception>().WithMessage("Username (testy2ghhhh.test@gmail.com) doesn't exist.");
    }

    [Fact]
    public async Task GetUser_Should_ReturnUsersList()
    {
        
        var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
        var filter = new UserFilterOptions();
        var sort = new SortingOptions();

        var adminRole = new Role
        {
            RoleName = "Name",
            IsActive = true,
            Id = 1
        };
        var getUsersQuery = new GetUsersQuery
        {
            pagingOptions = pagingOptions,
            filter = filter,
            sortingOptions = sort
        };
        var users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Testy",
                LastName = "Test",
                Gender = "Male",
                EmailId = "testy.test@gmail.com",
                Role = adminRole,
                RoleId = 1,
                ReportingTo = 1,
            },
            new User
            {
                Id = 2,
                FirstName = "Testy",
                LastName = "Test",
                Gender = "Male",
                EmailId = "testy.test@gmail.com",
                Role = adminRole,
                RoleId = 1,
                ReportingTo = 2
            }
        };
        var mock = users.AsQueryable().BuildMockDbSet();
        _userRepository.Setup(x => x.GetAll(new[]
        {
            "Insurer", "Role", "ReportingToNavigation"
        }, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<User, UserModel>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Id));
        }));
        
        //Act
        var result = await _userService.GetUsers(getUsersQuery, CancellationToken.None);
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRecordCount);
    }

    [Fact]
    public async Task GetUserById_Should_ReturnUser()
    {
        var testUserId = 1;
        var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
        var filter = new UserFilterOptions();
        var sort = new SortingOptions();
        
        var adminRole = new Role
        {
            RoleName = "Name",
            IsActive = true,
            Id = 1
        };
        var getUsersQuery = new GetUsersQuery
        {
            pagingOptions = pagingOptions,
            filter = filter,
            sortingOptions = sort
        };
        var users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Testy",
                LastName = "Test",
                Gender = "Male",
                EmailId = "testy.test@gmail.com",
                Role = adminRole,
                RoleId = 1,
                ReportingTo = 1,
            },
        };
        var mock = users.AsQueryable().BuildMockDbSet();
        _userRepository.Setup(x => x.GetAll(new[]
        {
            "Insurer", "Role", "ReportingToNavigation"
        }, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<User, UserModel>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Id));
        }));
        
        var result = await _userService.GetUserById(testUserId, CancellationToken.None);
        Assert.NotNull(result);
        Assert.Equal(testUserId, result.UserId);
    }

    [Fact]
    public async Task UpdateUser_NullRequest_ThrowsArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await  _userService.UpdateUser(1, null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }
    
    [Fact]
    public async Task UpdateUserProfile_NullRequest_ThrowsArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await  _userService.UpdateUserProfile(1, null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }
    
    [Fact]
    public async Task ResetPassword_NullUserName_ThrowsArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await  _userService.ResetPassword(null, null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'userName')");
    }
    
    [Fact]
    public async Task ResetPassword_NullRequest_ThrowsArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await  _userService.ResetPassword("Testy", null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }

    [Fact]
    public async Task Login_NullRequest_ThrowsArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await  _userService.Login(null, new CancellationToken());
        await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
    }

    
}