using eSusInsurers.Controllers;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceProducts;
using eSusInsurers.Models.Programs;
using eSusInsurers.Models.SeasonCutOffDate;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WMS.Models.Roles;
using Xunit;

namespace eSusInsurers.Tests.Controllers;

public class SeasonCutOffDatesControllerTests
{
    private readonly Mock<ISeasonCutOffDateService> _mockSeasonCutOffDateService;
    private readonly SeasonCutOffDatesController _controller;

    public SeasonCutOffDatesControllerTests()
    {
        _mockSeasonCutOffDateService = new Mock<ISeasonCutOffDateService>();
        _controller = new SeasonCutOffDatesController(_mockSeasonCutOffDateService.Object);
    }
    
    [Fact]
    public async Task GetSeasonCutOffDates_ShouldReturnAllSeasonCutOffDates()
    {
        //Arrange
        const int cropCategory = 1;
        var cropCategories = new List<SeasonCutOffDatesModel>
        {
            new SeasonCutOffDatesModel {SeasonCutOffDateId = 1 },
        };
            
        // Arrange
        var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
        var filter = new InsuranceProductFilterOption();
        var sort = new SortingOptions();
        var expectedResult = new PagedResult<SeasonCutOffDatesModel>
        {
            CurrentPage = 1,
            PageSize = 10,
            TotalPages = 1,
            TotalRecordCount = 1,
            Records = new List<SeasonCutOffDatesModel> { new SeasonCutOffDatesModel
            {
                SeasonCutOffDateId  = 1,
            } }
        };
        _mockSeasonCutOffDateService.Setup(s => s.GetSeasonCutOffDates(It.IsAny<GetSeasonCutOffDatesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.GetSeasonCutOffDates(pagingOptions);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<PagedResult<SeasonCutOffDatesModel>>(okResult.Value);
        Assert.Equal(expectedResult.CurrentPage, returnedResponse.CurrentPage);
        Assert.Equal(expectedResult.PageSize, returnedResponse.PageSize);
        Assert.Equal(expectedResult.TotalPages, returnedResponse.TotalPages);
        Assert.Equal(expectedResult.TotalRecordCount, returnedResponse.TotalRecordCount);
        Assert.Equal(expectedResult.Records.Count, returnedResponse.Records.Count);
        Assert.Equal(expectedResult.Records[0].SeasonCutOffDateId, returnedResponse.Records[0].SeasonCutOffDateId);
    }

    [Fact]
    public async Task GetSeasonCutOffDatesByValidId_ShouldReturnNotFound_WhenSeasonCutOffDates()
    {
        const int seasonCutOffDateId = 1;
        var seasonCutOffDate = new SeasonCutOffDatesModel
        {
            SeasonCutOffDateId = seasonCutOffDateId,
        };
        _mockSeasonCutOffDateService.Setup(s => s.GetSeasonCutOffDatesById(seasonCutOffDateId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(seasonCutOffDate);
        var result = await _controller.GetSeasonCutOffDatesById(seasonCutOffDateId);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
    }
    
    [Fact]
    public async Task AddPGetSeasonCutOffDate_ReturnsCreatedResult_WhenSuccessful()
    {
        // Arrange
        var request = new List<SeasonCutOffDatesRequest>
        {
            new SeasonCutOffDatesRequest()
        };
        bool expectedResultBool = true;
        _mockSeasonCutOffDateService.Setup(s => s.AddSeasonCutOffDates(It.IsAny<List<SeasonCutOffDatesRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Task.CompletedTask.IsCompleted);
        // Act
        var result = await _controller.AddSeasonCutOffDates(request);
        // Assert
        var createdResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
    }
    [Fact]
    public async Task UpdateSeasonCutOffDate_ReturnsUpdatedResult_WhenSuccessful()
    {
        int id = 1;
        // Arrange
        var request = new UpdateSeasonCutOffDatesRequest();
        bool expectedResultBool = true;
        _mockSeasonCutOffDateService.Setup(s => s.UpdateSeasonCutOffDates(1, It.IsAny<UpdateSeasonCutOffDatesRequest>() ,It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        // Act
        var result = await _controller.UpdateSeasonCutOffDates(id, request);
        // Assert
        Assert.NotNull( result);
    }
    
    [Fact]
    public async Task DeleteSeasonCutOffDate_ReturnsDeletedResult_WhenSuccessful()
    {
        int id = 1;
        // Arrange
        _mockSeasonCutOffDateService.Setup(s => s.DeleteSeasonCutOffDates(1,It.IsAny<CancellationToken>())).ReturnsAsync(true);
        // Act
        var result = await _controller.DeleteSeasonCutOffDates(id);
        // Assert
        Assert.NotNull( result);
    }
    
    [Fact]
    public async Task ActivateSeasonCutOffDate_ReturnsUpdatedResult_WhenSuccessful()
    {
        int id = 1;
        // Arrange
        _mockSeasonCutOffDateService.Setup(s => s.ActivateSeasonCutOffDates(1,It.IsAny<CancellationToken>())).ReturnsAsync(true);
        // Act
        var result = await _controller.ActivateSeason(id);
        // Assert
        Assert.NotNull( result);
    }
    
    [Fact]
    public async Task SeasonCutOffDatesExistenceCheck_ReturnsResult_WhenSuccessful()
    {
        int id = 1;
        // Arrange
        _mockSeasonCutOffDateService.Setup(s => s.SeasonCutOffDatesExistenceCheck(It.IsAny<SeasonCutOffDatesRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        // Act
        var result = await _controller.ActivateSeason(id);
        // Assert
        Assert.NotNull( result);
    }
}