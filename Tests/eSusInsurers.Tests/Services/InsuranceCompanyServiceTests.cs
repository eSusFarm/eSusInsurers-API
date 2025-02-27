using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models;
using eSusInsurers.Models.InsuranceCompany;
using eSusInsurers.Services.Implementations;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Services;

public class InsuranceCompanyServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IInsuranceCompanyRepository> _insuranceCompanyRepository;
    private readonly InsuranceCompanyService _insuranceCompanyService;
    private readonly IConfigurationProvider _mapperConfig;
    private readonly Mock<IMapper> _mapper;

    public InsuranceCompanyServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _insuranceCompanyRepository = new Mock<IInsuranceCompanyRepository>();
        _mapper = new Mock<IMapper>();
        _insuranceCompanyService = new InsuranceCompanyService(_unitOfWork.Object, _mapper.Object);
        _unitOfWork.Setup(x => x.InsuranceCompanyRepository).Returns(_insuranceCompanyRepository.Object);
    }
    
     [Fact]
        public async Task GetInsuranceCompany_WithValidInsuranceCompany_ReturnsCrop()
        {
            // Arrange
            var insuranceCompanies = new List<InsuranceCompany>
            {
                new() { Id = 1, CompanyName = "Company 1", IsActive=true}
            };
            var mock = insuranceCompanies.AsQueryable().BuildMockDbSet();
            _insuranceCompanyRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
            _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<InsuranceCompany, InsuranceCompanyModel>()
                    .ForMember(d => d.CompanyId, opt => opt.MapFrom(s => s.Id));
            }));
            // Act
            var result = await _insuranceCompanyService.GetCompanies( CancellationToken.None);
            // Assert
            Assert.Equal(1,result.Count);
            _insuranceCompanyRepository.Verify(x => x.GetAll(null, false), Times.Once());
        }
        
        [Fact]
        public async Task GetInsuranceCompany_WithInValidInsuranceCompany_ReturnsCrop()
        {
            // Arrange
            int cropCategoryId = 5;
            var crops = new List<InsuranceCompany>();
            var mock = crops.AsQueryable().BuildMockDbSet();
            _insuranceCompanyRepository.Setup(x => x.GetAll(null, false)).Returns(mock.Object.AsQueryable());
            _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<InsuranceCompany, InsuranceCompanyModel>()
                    .ForMember(d => d.CompanyId, opt => opt.MapFrom(s => s.Id));
            }));
            // Act
            var result = await _insuranceCompanyService.GetCompanies( CancellationToken.None);
            // Assert
            Assert.Equal(0,result.Count);
            _insuranceCompanyRepository.Verify(x => x.GetAll(null, false), Times.Once());
        }
}