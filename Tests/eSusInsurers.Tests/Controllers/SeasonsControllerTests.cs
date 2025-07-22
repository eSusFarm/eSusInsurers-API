using eSusInsurers.Controllers;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceProducts;
using eSusInsurers.Models.Programs;
using eSusInsurers.Models.Seasons;
using eSusInsurers.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace eSusInsurers.Tests.Controllers;

public class SeasonsControllerTests
{
    private readonly Mock<ISeasonService> _seasonServiceMock;
    private readonly ITestOutputHelper _output;
    private readonly SeasonsController _controller;

    public SeasonsControllerTests(ITestOutputHelper output)
    {
        _seasonServiceMock = new Mock<ISeasonService>();
        _controller = new SeasonsController(_seasonServiceMock.Object);
        _output = output;
    }
    
    [Fact]
    public async Task GetSeasons_ShouldReturnAllCropCategories()
    {
        // Arrange
        var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
        var expectedResult = new PagedResult<SeasonModel>
        {
            CurrentPage = 1,
            PageSize = 10,
            TotalPages = 1,
            TotalRecordCount = 1,
            Records = new List<SeasonModel> { new SeasonModel
            {
                SeasonName  = "Test Season",
            } }
        };
        _seasonServiceMock.Setup(s => s.GetSeasons(It.IsAny<GetSeasonQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.GetSeasons(pagingOptions);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<PagedResult<SeasonModel>>(okResult.Value);
        Assert.Equal(expectedResult.CurrentPage, returnedResponse.CurrentPage);
        Assert.Equal(expectedResult.PageSize, returnedResponse.PageSize);
        Assert.Equal(expectedResult.TotalPages, returnedResponse.TotalPages);
        Assert.Equal(expectedResult.TotalRecordCount, returnedResponse.TotalRecordCount);
        Assert.Equal(expectedResult.Records.Count, returnedResponse.Records.Count);
        Assert.Equal(expectedResult.Records[0].SeasonName, returnedResponse.Records[0].SeasonName);
    }

    [Fact]
    public async Task GetSeasonByYear_ShouldReturnCorrectSeason()
    {
        var expectedResult = new SeasonModel
        {
            SeasonName = "Test Season",
            SeasonYear = "2025"
        };
        var list = new List<SeasonModel>();
        list.Add(expectedResult);
        _seasonServiceMock.Setup(s => s.GetSeasonByYear(It.IsAny<String>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(list);
        var result = await _controller.GetSeasonByYear("2025");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<List<SeasonModel>>(okResult.Value);
        Assert.Equal(expectedResult.SeasonName, returnedResponse[0].SeasonName);
        Assert.Equal(expectedResult.SeasonYear, returnedResponse[0].SeasonYear);
    }

    [Fact]
    public async Task GetSeasonByYear_SerciveThrowsException_ShouldReturnBadRequest()
    {
        var expectedException = new Exception("Test error");
        _seasonServiceMock.Setup(s => s.GetSeasonByYear(It.IsAny<String>(), It.IsAny<CancellationToken>())).ThrowsAsync(expectedException);
        // Act
        var result = await _controller.GetSeasonByYear("2025");

        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }

    [Fact]
    public async Task AddSeason_CreatesSeasonSuccessfully()
    {
        _seasonServiceMock.Setup(s => s.AddSeason(It.IsAny<SeasonRequest>(),It.IsAny<CancellationToken>() )).ReturnsAsync(1);
        var result = _controller.AddSeason(GetSeasonRequest());
        var okResult = Assert.IsType<ObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<int>(okResult.Value);
        Assert.Equal(1, returnedResponse);
    }
    
    [Fact]
    public async Task UpdateSeason_UpatesSeasonSuccessfully()
    {
        _seasonServiceMock.Setup(s =>
            s.UpdateSeason(It.IsAny<int>(), It.IsAny<UpdateSeasonRequest>(), It.IsAny<CancellationToken>()));
        var result = _controller.UpdateSeason(1, GetUpdateSeasonRequest());
        var okResult = Assert.IsType<NoContentResult>(result.Result);
    }
    
    [Fact]
    public async Task DeleteSeason_DeletesSeasonSuccessfully()
    {
        _seasonServiceMock.Setup(s =>
            s.DeleteSeason(It.IsAny<int>(), It.IsAny<CancellationToken>()));
        var result = _controller.DeleteSeason(1);
        var okResult = Assert.IsType<NoContentResult>(result.Result);
    }
    
    [Fact]
    public async Task ActivateSeason_ActivatesSeasonSuccessfully()
    {
        _seasonServiceMock.Setup(s =>
            s.ActivateSeason(It.IsAny<int>(), It.IsAny<CancellationToken>()));
        var result = _controller.ActivateSeason(1);
        var okResult = Assert.IsType<NoContentResult>(result.Result);
    }
    
    [Fact]
    public async Task ActivateSeason_ServiceThrowsException_ActivatesSeasonThrowsException()
    {
        var expectedException = new Exception("Test error");
        _seasonServiceMock.Setup(s =>
            s.ActivateSeason(It.IsAny<int>(), It.IsAny<CancellationToken>())).Throws(expectedException);
        var result = _controller.ActivateSeason(1);
        // Assert
        var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
        error.Should().NotBeNull();
    }

    private SeasonModel GetSeasonModel()
    {
        return  new SeasonModel
        {
            SeasonName = "Test Season",
            SeasonYear = "2025"
        };
    }

    private SeasonRequest GetSeasonRequest()
    {
        return new SeasonRequest
        {
            SeasonName = "Test Season",
            SeasonYear = "2025"
        };
    }
    
    private UpdateSeasonRequest GetUpdateSeasonRequest()
    {
        return new UpdateSeasonRequest
        {
            SeasonName = "Test Season",
            SeasonYear = "2025"
        };
    }
}