using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.InsuranceCompany;
using eSusInsurers.Models.InsuranceProducts;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using MockQueryable.Moq;
using Moq;
using Xunit;
using IConfigurationProvider = Microsoft.Extensions.Configuration.IConfigurationProvider;

namespace eSusInsurers.Tests.Services;

public class InsuranceProductServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IInsuranceProductRepository> _insuranceProductRepository;
    private readonly   InsuranceProductService _insuranceProductService;    
    private readonly IConfigurationProvider _mapperConfig;
    private readonly Mock<IMapper> _mapper;

    public InsuranceProductServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _insuranceProductRepository = new Mock<IInsuranceProductRepository>();
        _mapper = new Mock<IMapper>();
        _insuranceProductService  = new InsuranceProductService(_unitOfWork.Object, _mapper.Object);
        _unitOfWork.Setup(x => x.InsuranceProductRepository).Returns(_insuranceProductRepository.Object);
    }
    
    [Fact]
    public async Task GetInsuranceProduct_WithValidInsuranceProduct_ReturnsInsuranceProducts()
    {
        // Arrange
        var insuranceProducts = new List<InsurancePolicy1>
        {
            new() { Id = 1, CompanyId=1,CategoryId=1, PolicyName = "Policy 1", IsActive=true}
        };
        var insuranceProductQuery = new InsuranceProductQuery();
        var mock = insuranceProducts.AsQueryable().BuildMockDbSet();
        _insuranceProductRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<InsurancePolicy1, InsuranceProductModel>()
                .ForMember(d => d.InsurancePolicyId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _insuranceProductService.GetInsuranceProducts( insuranceProductQuery, CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task GetInsuranceProduct_WithNoInsuranceProduct_ReturnsNoInsuranceProducts()
    {
        // Arrange
        var insuranceProducts = new List<InsurancePolicy1>();
        var insuranceProductQuery = new InsuranceProductQuery();
        var mock = insuranceProducts.AsQueryable().BuildMockDbSet();
        _insuranceProductRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<InsurancePolicy1, InsuranceProductModel>()
                .ForMember(d => d.InsurancePolicyId, opt => opt.MapFrom(s => s.Id));
        }));
        // Act
        var result = await _insuranceProductService.GetInsuranceProducts( insuranceProductQuery, CancellationToken.None);
        // Assert
        Assert.Equal(0, result.TotalRecordCount);
    }
}