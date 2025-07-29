using eSusInsurers.Controllers;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceRequests.getInsuranceRequests;
using eSusInsurers.Models.SeasonCutOffDate;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Controllers;

public class InsuranceRequestsControllerTests
{
    private readonly InsuranceRequestsController _controller;
    private readonly Mock<IInsuranceRequestsService> _mockInsuranceRequestsService;

    public InsuranceRequestsControllerTests()
    {
        _mockInsuranceRequestsService = new Mock<IInsuranceRequestsService>();
        _controller = new InsuranceRequestsController(_mockInsuranceRequestsService.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllInsuranceRequests()
    {
        var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
        var expectedResult = new PagedResult<InsuranceRequestModel>
        {
            CurrentPage = 1,
            PageSize = 10,
            TotalPages = 1,
            TotalRecordCount = 1,
            Records = new List<InsuranceRequestModel>{ new InsuranceRequestModel
            {
                CropName = "testCrop",
            }}
        };
        
        _mockInsuranceRequestsService.Setup(s=>s.GetInsuranceRequests(It.IsAny<GetinsuranceRequestsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);
        // Act
        var result = await _controller.GetInsuranceResquests(pagingOptions);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<PagedResult<InsuranceRequestModel>>(okResult.Value);
        Assert.Equal(expectedResult.CurrentPage, returnedResponse.CurrentPage);
        Assert.Equal(expectedResult.PageSize, returnedResponse.PageSize);
        Assert.Equal(expectedResult.TotalPages, returnedResponse.TotalPages);
        Assert.Equal(expectedResult.TotalRecordCount, returnedResponse.TotalRecordCount);
        Assert.Equal(expectedResult.Records.Count, returnedResponse.Records.Count);
        Assert.Equal(expectedResult.Records[0].CropName, returnedResponse.Records[0].CropName);
    }
    [Fact]
    public async Task GetAll_WhenExceptionThrown_ShouldReturnBadRequest()
    {
        var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
        var expectedException = new Exception("Test error");
        
        _mockInsuranceRequestsService.Setup(s=>s.GetInsuranceRequests(It.IsAny<GetinsuranceRequestsQuery>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        // Act
        var result = await _controller.GetInsuranceResquests(pagingOptions);

        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }
}