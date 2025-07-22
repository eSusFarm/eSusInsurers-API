using eSusInsurers.Domain.Entities;
using eSusInsurers.Models.Etherisc;
using eSusInsurers.Models.Etherisc.Policy;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Abstractions;
namespace eSusInsurers.Controllers;

public class BlockChainControllerTests
{
    private readonly BlockChainController _controller;
    private readonly Mock<IEtheriscService> _mockEtheriscService;

    public BlockChainControllerTests()
    {
        _mockEtheriscService = new Mock<IEtheriscService>();
        _controller = new BlockChainController(_mockEtheriscService.Object);
    }

    [Fact]
    public async Task BlockChainController_CreateBlock_CreatesBlockSuccessfully()
    {
        var expectedResponse = new AddPolicyResponse
        {
            cropInsuranceId = 1,
            message = "test"
        };
        _mockEtheriscService.Setup(s=>s.AddPolicy(It.IsAny<AddPolicyRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);
        var result =  _controller.AddPolicy(new AddPolicyRequest(), new CancellationToken());
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<AddPolicyResponse>(okResult.Value);
        Assert.Equal(expectedResponse, returnedResponse);
    }
    [Fact]
    public async Task BlockChainController_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _mockEtheriscService.Setup(s=>s.AddPolicy(It.IsAny<AddPolicyRequest>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result =  _controller.AddPolicy(new AddPolicyRequest(), new CancellationToken());
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CreatePlolicy_CreateBlock_CreatesBlockSuccessfully()
    {
        var expectedResponse = new AddPolicyResponse
        {
            cropInsuranceId = 1,
            message = "test"
        };
        _mockEtheriscService.Setup(s=>s.CreateBlockChainPolicy(It.IsAny<AddPolicyRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);
        var result =  _controller.AddBlockChainPolicy(new AddPolicyRequest(), new CancellationToken());
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<AddPolicyResponse>(okResult.Value);
        Assert.Equal(expectedResponse, returnedResponse);
    }
    [Fact]
    public async Task CreatePlolicy_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _mockEtheriscService.Setup(s=>s.CreateBlockChainPolicy(It.IsAny<AddPolicyRequest>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result =  _controller.AddBlockChainPolicy(new AddPolicyRequest(), new CancellationToken());
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    [Fact]
    public async Task CreatePlolicyMetaData_CreateBlock_CreatesBlockSuccessfully()
    {
        var expectedResponse = new AddPolicyResponse
        {
            cropInsuranceId = 1,
            message = "test"
        };
        _mockEtheriscService.Setup(s=>s.AddPolicyMetadata(It.IsAny<AddPolicyRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);
        var result =  _controller.AddPolicyMetaData(new AddPolicyRequest(), new CancellationToken());
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<AddPolicyResponse>(okResult.Value);
        Assert.Equal(expectedResponse, returnedResponse);
    }
    [Fact]
    public async Task CreatePlolicyMetaData_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _mockEtheriscService.Setup(s=>s.AddPolicyMetadata(It.IsAny<AddPolicyRequest>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result =  _controller.AddPolicyMetaData(new AddPolicyRequest(), new CancellationToken());
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task? UpdateInsuranceRick_CreateBlock_CreatesBlockSuccessfully()
    {
        var expectedResponse = new UpdateRiskResponse
        {
            configId = "1",
            crop = "test"
        };
        _mockEtheriscService.Setup(s=>s.UpdateRisk(It.IsAny<UpdateRiskRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);
        var result =  _controller.UpdateInsuranceRisk(new UpdateRiskRequest(), new CancellationToken());
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<UpdateRiskResponse>(okResult.Value);
        Assert.Equal(expectedResponse, returnedResponse);
    }
    [Fact]
    public async Task UpdateInsuranceRick_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _mockEtheriscService.Setup(s=>s.UpdateRisk(It.IsAny<UpdateRiskRequest>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result =  _controller.UpdateInsuranceRisk(new UpdateRiskRequest(), new CancellationToken());
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
    
    [Fact]
    public async Task? SyncInsuranceRick_CreateBlock_CreatesBlockSuccessfully()
    {
        var expectedResponse = "done";
        _mockEtheriscService.Setup(s=>s.SyncRisk(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);
        var result =  _controller.SyncInsuranceRisk("test", new CancellationToken());
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<string>(okResult.Value);
        Assert.Equal(expectedResponse, returnedResponse);
    }
    [Fact]
    public async Task SyncInsuranceRick_ThrowsException_ReturnsBadRequest()
    {
        var expectedException = new Exception("Test error");
        _mockEtheriscService.Setup(s=>s.SyncRisk(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result =  _controller.SyncInsuranceRisk("test", new CancellationToken());
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
}
