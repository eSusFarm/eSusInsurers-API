using eSusInsurers.Controllers;
using eSusInsurers.Models.Payment;
using eSusInsurers.Models.SeasonCutOffDate;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Controllers;

public class PaymentsControllerTests
{
    private readonly PaymentsController _controller;
    private readonly Mock<IPaymentService> _paymentServiceMock;

    public PaymentsControllerTests()
    {
        _paymentServiceMock = new Mock<IPaymentService>();
        _controller = new PaymentsController(_paymentServiceMock.Object);
    }

    [Fact]
    public async Task CreatePayments_WhenSuccessful_CreatesPayment()
    {

        var expectedResult = new RegisterPaymentResponse
        {
            CropInsuranceId = 1,
            message = "test",
            PremiumPaymentId = 1,
            statusCode = 200
        };
        _paymentServiceMock
            .Setup(s => s.registerPayment(It.IsAny<RegisterPaymentRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        var result =  await _controller.AddPolicy(new RegisterPaymentRequest(), new CancellationToken());
        var createdResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, createdResult.StatusCode);
        Assert.Equal(expectedResult, createdResult.Value);
    }
    
    [Fact]
    public async Task CreatePayments_ThrowsException_ReturnsBadRequest()
    {

        var expectedException = new Exception("Test error");
        _paymentServiceMock
            .Setup(s => s.registerPayment(It.IsAny<RegisterPaymentRequest>(), It.IsAny<CancellationToken>()))
            .Throws(expectedException);
        var result =   _controller.AddPolicy(new RegisterPaymentRequest(), new CancellationToken());
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    
}