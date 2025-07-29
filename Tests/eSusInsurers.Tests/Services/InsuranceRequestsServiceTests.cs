using AutoMapper;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceRequests.getInsuranceRequests;
using eSusInsurers.Services.Implementations;
using Microsoft.Extensions.Logging;
using MockQueryable.FakeItEasy;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Services;

public class InsuranceRequestsServiceTests
{
    private readonly InsuranceRequestsService _insuranceRequestsService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IInsuranceRequestsRepository> _insuranceRequestsRepository;
   
    
    public InsuranceRequestsServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _mapper = new Mock<IMapper>();
        _insuranceRequestsRepository = new Mock<IInsuranceRequestsRepository>();
        _unitOfWork.Setup(i => i.InsuranceRequestsRepository).Returns(_insuranceRequestsRepository.Object);
        _insuranceRequestsService = new InsuranceRequestsService(_unitOfWork.Object, _mapper.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnInsuranceRequest()
    {

        var insuranceRequests = new List<InsuranceRequest>
        {
            new () { Id = 1, FarmerId = 1, FarmerCropId = 1, IsActive=true},
            new () { Id = 1, FarmerId = 1, FarmerCropId = 1,IsActive=true },
        };
        var mock = insuranceRequests.AsQueryable().BuildMockDbSet();
        _insuranceRequestsRepository.Setup(x => x.GetAll(null, true)).Returns(mock);
        _mapper.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<InsuranceRequest, InsuranceRequestModel>()
                .ForMember(d => d.IsRequestSubmitted, o => o.MapFrom(s => s.IsRequestSubmitted))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive))
                .ForMember(d => d.InsuranceCompanyName, o => o.MapFrom(s => s.InsuranceCompanyName))
                .ForMember(d => d.ProgramName, o => o.MapFrom(s => s.ProgramName))
                .ForMember(d => d.HomeLocationVillage, o => o.MapFrom(s => s.HomeLocationVillage))
                .ForMember(d => d.FarmLocationDistrict, o => o.MapFrom(s => s.FarmLocationDistrict))
                .ForMember(d => d.FarmLocationVillage, o => o.MapFrom(s => s.FarmLocationVillage))
                .ForMember(d => d.FarmerName, o => o.MapFrom(s => s.FarmerName))
                .ForMember(d => d.CropName, o => o.MapFrom(s => s.CropName))
                .ForMember(d => d.InsuredAmount, o => o.MapFrom(s => s.InsuredAmount))
                .ForMember(d => d.PremiumAmount, o => o.MapFrom(s => s.PremiumAmount))
                .ForMember(d => d.FarmLocationSubCounty, o => o.MapFrom(s => s.FarmLocationSubCounty));
        }));
        var result = await _insuranceRequestsService.GetInsuranceRequests(BuildQuery(), new CancellationToken());
        Assert.NotNull(result);
        _insuranceRequestsRepository.Verify(x => x.GetAll(null, false), Times.Once());
    }

    private GetinsuranceRequestsQuery BuildQuery()
    {
        return new GetinsuranceRequestsQuery
        {
            pagingOptions = new PagingOptions { Page = 1, PageSize = 10 },
            filter = new InsuranceRequestFilterOptions
            {
                IsActive = true,
            },
        };
    }
}