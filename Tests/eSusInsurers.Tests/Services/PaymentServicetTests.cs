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