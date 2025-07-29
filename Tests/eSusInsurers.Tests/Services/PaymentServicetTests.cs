using System.Security.Cryptography;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models.Payment;
using eSusInsurers.Services.Implementations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Services;

public class PaymentServicetTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IPremiumPaymentsRepository> _premiumPaymentsRepository;
    private readonly PaymentService _service;
    private ILogger<PaymentService> _logger;
    private readonly Mock<IDbContextTransaction> _transaction;
    
    public PaymentServicetTests()
    {
        _transaction = new Mock<IDbContextTransaction>();
        _premiumPaymentsRepository = new Mock<IPremiumPaymentsRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(x => x.PremiumPaymentsRepository).Returns(_premiumPaymentsRepository.Object);
        _logger = new Mock<ILogger<PaymentService>>().Object;
        _service = new PaymentService(_unitOfWork.Object, _logger);
    }

    [Fact]
    public async Task RegisterAsync_ShouldReturnSuccess()
    {
        _premiumPaymentsRepository.Setup(p=>p.AddAsync(It.IsAny<InsurancePremiumPayment>(), It.IsAny<CancellationToken>())).ReturnsAsync(CreateInsurancePremiumPayment());
        var result =await _service.registerPayment(CreateRegisterPaymentRequest(), new CancellationToken());
        Assert.NotNull(result);
    }
    
    
    [Fact]
    public async Task RegisterAsync_WhenThrowsException_ShouldReturnSuccess()
    {
        _premiumPaymentsRepository.Setup(p=>p.AddAsync(It.IsAny<InsurancePremiumPayment>(), It.IsAny<CancellationToken>())).ReturnsAsync(CreateInsurancePremiumPayment());
        var result =await _service.registerPayment(null, new CancellationToken());
        Assert.NotNull(result);
    }

    private RegisterPaymentRequest CreateRegisterPaymentRequest()
    {
        return new RegisterPaymentRequest
        {
            CropInsuranceId = 2,
            currency = "UGX",
            policyNumber = "1234",
            ModeOfPayment = "test",
            TaxAmount = 2334,
            PaidAmount = 1344,
            TotalPaidAmount = 23455,
        };
    }

    private InsurancePremiumPayment CreateInsurancePremiumPayment()
    {
        return new InsurancePremiumPayment
        {
            CropInsuranceId = 2,
            Currency = "UGX",
            policyNumber = "1234",
            ModeOfPayment = "test",
            TaxAmount = 2334,
            PaidAmount = 1344,
            TotalPaidAmount = 23455,
            CropInsurance = new CropInsurance
            {
                CreatedDate = DateTime.Now,
                FarmerCropId = 1,
                Farmer = new Farmer
                {
                  Address  =   "unit 6 block 134",
                  AdminComments = "test",
                  ChiefName = "tester",
                  City = "tester",
                  Country = "tester",
                  MobileNumber =   869609,
                  Msisdn =   869609,
                  Password = "tester",
                  MobilePin =1736863,
                  Name =   "test",
                  TnCaccepted = true,
                  Surname = "tester",
                  Dob  = "tester",
                  Idnumber = "tester",
                  Gender = "tester",
                  Province = "tester",
                  CountryId=1,
                  IsActive=false,
                  ProgramName = false,
                  EnterProgramName = "tester",
                  FarmingCommodity = 12,
                  FarmSize=2346,
                  DataConfirmation = true,
                  MainOrTraditionalLand = 1,
                  StreetName = "tester",
                  VillageName = "tester",
                  RiverName = "tester",
                  LevelOfEducation = "tester",
                  DipTank = "tester",
                  NearestMountain = "tester",
                  ProgramId = 1,
                  IsFarmerDeletedbySuperAdmin = false,
                  IsSuspended = false,
                  Comments = "dqwd",
                  CreatedDate = DateTime.Now,
                  Latitude = 131313,
                  Longitude = 131313,
                  Location = "tester",
                  ProfilePicture = "tester",
                  IdfrontView = "tester",
                  IdbackView = "tester",
                  ServiceProvider = "tester",
                  Claims = new List<Claim>
                  {
                      new ()
                      {
                          ClaimNumber="Claim",
                          FarmerId = 1,
                          CropInsuranceId=2,
                          CropInsurancePremiumId=2,
                          RequestedOn = DateTime.Now,
                          AllowedAmount=70,
                          OtherChargesAmount=70,
                          DisallowAmount=70,
                          Currency="UGX",
                          PaidDate=DateTime.Now,
                          Status = "pending",
                          IsActive = true,
                          CropInsurance = new CropInsurance{
                          CreatedDate = DateTime.Now,
                          FarmerId = 2,
                          FarmerCropId = 2,
                          Longitude=70,
                          Latitude=70,
                          InsurancePolicyId=70,
                          CropName="UGX",
                          InsuranceRiskId = 1,
                          Status  = "pending",
                          Comments = "dqwd",
                          IsActive = true
                      }
                      }
                  },
                  ClaimsAus = new List<ClaimsAu>
                  {
                      new()
                      {
                          ClaimNumber="Claim",
                          FarmerId = 1,
                          CropInsuranceId=2,
                          CropInsurancePremiumId=2,
                          RequestedOn = DateTime.Now,
                          AllowedAmount=70,
                          OtherChargesAmount=70,
                          DisallowAmount=70,
                          Currency="UGX",
                          PaidDate=DateTime.Now,
                          Status = "pending",
                          IsActive = true,
                          CropInsurance = new CropInsurance{
                              CreatedDate = DateTime.Now,
                              FarmerId = 2,
                              FarmerCropId = 2,
                              Longitude=70,
                              Latitude=70,
                              InsurancePolicyId=70,
                              CropName="UGX",
                              InsuranceRiskId = 1,
                              Status  = "pending",
                              Comments = "dqwd",
                              IsActive = true
                      }
                  }},
                  FarmerCrops = new List<FarmerCrop>
                  {
                      new () {
                          Comments = "test",
                          FarmerId = 1,
                          CreatedDate = DateTime.Now,
                          CreatedBy = "test", 
                          IsActive = true, 
                          Id = 2, 
                          ModifiedDate = DateTime.Now , 
                          FarmLandSize = 568,
                          IsPrecultCompleted = false,
                          IsCultCompleted = false,
                          IsPlantGrowthCompleted = false,
                          IsHarvestCompleted = false,
                          PreCultStartDate = DateTime.Now,
                          PreCultEndDate = DateTime.Now,
                          CultStartDate = DateTime.Now,
                          CultEndDate = DateTime.Now,
                          PlantGrowthStartDate = DateTime.Now,
                          PlantGrowthEndDate = DateTime.Now,
                          HarvestingStartDate = DateTime.Now,
                          HarvestingEndDate = DateTime.Now,
                          Crop = new Crop
                      {
                          CreatedDate = DateTime.Now,
                          CreatedBy = "tester",
                          CropCategoryId = 1,
                          CropName = "tester"
                      }}
                  }
                },
                CropInsurancePremia = new List<CropInsurancePremium>
                {
                    new ()
                    {
                        CreatedBy = "test",
                        CropInsurance = new CropInsurance
                        {
                            CreatedDate = DateTime.Now,
                            FarmerId = 1,
                            FarmerCropId = 2
                        },
                        CreatedDate = DateTime.Today,
                        IsActive = true,
                        Id = 2,
                        ModifiedDate = DateTime.Now ,
                        InsurancePremium = new InsurancePremium
                        {
                            CreatedDate = DateTime.Now,
                            CreatedBy = "tester",
                            InsurancePolicyId = 1,
                            InsurancePolicy = new InsurancePolicy
                            {
                                CreatedDate = DateTime.Now,
                                CreatedBy = "tester"
                            }
                        }
                    }
                },
                Comments = "test",
                FarmerId=2,
                CropName = "test",
                Longitude=23586809,
                Latitude=23586809,
                InsurancePolicyId=1,
                InsuranceRiskId=1,
                Status="stattus",
                IsActive = true
            }
        };
    }
}