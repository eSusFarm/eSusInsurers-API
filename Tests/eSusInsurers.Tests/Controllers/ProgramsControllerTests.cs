using eSusInsurers.Controllers;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceProducts;
using eSusInsurers.Models.Programs;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace eSusInsurers.Tests.Controllers;

public class ProgramsControllerTests
{
    private readonly Mock<IProgramsService> _programsService;
    private readonly ITestOutputHelper _output;
    private readonly ProgramsController _controller;
    
    public ProgramsControllerTests(ITestOutputHelper output)
    {
        _programsService = new Mock<IProgramsService>();
        _controller = new ProgramsController(_programsService.Object);
        _output = output;
    }
    
     [Fact]
        public async Task GetCropCategories_ShouldReturnAllCropCategories()
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
            var expectedResult = new PagedResult<ProgramsModel>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalPages = 1,
                TotalRecordCount = 1,
                Records = new List<ProgramsModel> { new ProgramsModel
                {
                    DistrictName  = "Test District",
                } }
            };
            _programsService.Setup(s => s.GetPrograms(It.IsAny<GetProgramsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Getprograms(pagingOptions);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedResponse = Assert.IsType<PagedResult<ProgramsModel>>(okResult.Value);
            Assert.Equal(expectedResult.CurrentPage, returnedResponse.CurrentPage);
            Assert.Equal(expectedResult.PageSize, returnedResponse.PageSize);
            Assert.Equal(expectedResult.TotalPages, returnedResponse.TotalPages);
            Assert.Equal(expectedResult.TotalRecordCount, returnedResponse.TotalRecordCount);
            Assert.Equal(expectedResult.Records.Count, returnedResponse.Records.Count);
            Assert.Equal(expectedResult.Records[0].DistrictName, returnedResponse.Records[0].DistrictName);
        }
        
        [Fact]
        public async Task AddPrograms_ReturnsCreatedResult_WhenSuccessful()
        {
            // Arrange
            var request = new ProgramRequest();
            bool expectedResultBool = true;

            _programsService.Setup(s => s.AddProgram(It.IsAny<ProgramRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultBool);
            // Act
            var result = await _controller.AddProgram(request);
            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(expectedResultBool, createdResult.Value);
        }
        
        [Fact]
        public async Task UpdatePrograms_ReturnsUpdatedResult_WhenSuccessful()
        {
            // Arrange
            int programId = 1;
            var request = new ProgramRequest();
            bool expectedResultBool = false;

            _programsService.Setup(s => s.UpdateProgram(programId, It.IsAny<ProgramRequest>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            // Act
            var result = await _controller.AddProgram(request);
            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(expectedResultBool, createdResult.Value);
        }
}