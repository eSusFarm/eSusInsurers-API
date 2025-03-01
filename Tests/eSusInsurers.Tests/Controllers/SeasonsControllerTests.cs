using eSusInsurers.Controllers;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceProducts;
using eSusInsurers.Models.Programs;
using eSusInsurers.Models.Seasons;
using eSusInsurers.Services;
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
        //Arrange
        const int cropCategory = 1;
        var cropCategories = new List<InsuranceProductModel>
        {
            new InsuranceProductModel {InsurancePolicyId = 1 },
        };
            
        // Arrange
        var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
        var filter = new InsuranceProductFilterOption();
        var sort = new SortingOptions();
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

    }