using eSusInsurers.Controllers;
using eSusInsurers.Models.EsusFarm;
using eSusInsurers.Models.Etherisc.Policy;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Controllers;

public class EsusFarmPolicyControllerTests
{
    private readonly EsusFarmPolicyController _controller;
    private readonly Mock<IEsusFarmPolicyService> _esusFarmPolicyServiceMock;

    public EsusFarmPolicyControllerTests()
    {
        _esusFarmPolicyServiceMock = new Mock<IEsusFarmPolicyService>();
        _controller = new EsusFarmPolicyController(_esusFarmPolicyServiceMock.Object);
    }

    [Fact]
    public async Task CreatePolicy_WhenSucceessful_ShouldReturnCreated()
    {
        _esusFarmPolicyServiceMock.Setup(s =>
                s.ProcessPolicyRequestAsync(It.IsAny<EsusFarmPolicyRequestDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var result = await _controller.CreatePolicy(new EsusFarmPolicyRequestDto(), new CancellationToken());
        // Assert
        var createdResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, createdResult.StatusCode);
    }
    [Fact]
    public async Task CreatePolicy_WhenThrowsException_ShouldReturnCreated()
    {
        var expectedException = new Exception("Policy request processing failed. Check logs for details");
        _esusFarmPolicyServiceMock.Setup(s =>
                s.ProcessPolicyRequestAsync(It.IsAny<EsusFarmPolicyRequestDto>(), It.IsAny<CancellationToken>()))
            .Throws(expectedException);
        var result = _controller.CreatePolicy(new EsusFarmPolicyRequestDto(), new CancellationToken());
        // Assert
        // Assert
        Assert.NotNull( result);
    }
}