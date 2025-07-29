using eSusInsurers.Controllers;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Models;
using eSusInsurers.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace eSusInsurers.Tests.Controllers
{
    public class CropCategoriesControllerTests
    {
        private readonly Mock<ICropCategoryService> _mockCropCategoryService;
        private readonly ITestOutputHelper _output;
        private readonly CropCategoriesController _cropCategoriesController;

        public CropCategoriesControllerTests(ITestOutputHelper output)
        {
            _mockCropCategoryService = new Mock<ICropCategoryService>();
            _cropCategoriesController = new CropCategoriesController(_mockCropCategoryService.Object);
            _output = output;
        }

        [Fact]
        public async Task GetCropCategories_ShouldReturnAllCropCategories()
        {
            //Arrange
            const int cropCategory = 1;
            var cropCategories = new List<CropCategoryModel>
            {
                new CropCategoryModel {CropCategoryName = "CropCategoryModel 1" },
                new CropCategoryModel {CropCategoryName = "CropCategoryModel 1" }
            };
            _mockCropCategoryService
                .Setup(service => service.GetCropCategories(new CancellationToken())).ReturnsAsync(cropCategories);
            // Act
            var actionResult = await _cropCategoriesController.GetCropCategories();
            // Assert
            var result = Assert.IsType<ActionResult<List<CropCategoryModel>>>(actionResult);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCrops = Assert.IsAssignableFrom<List<CropCategoryModel>>(okResult.Value);
            Assert.Equal(cropCategories.Count, returnedCrops.Count);
        }
        
        [Fact]
        public async Task GetCropCategories_WhenExceptionThrown_ShouldReturnBadRequest()
        {
            //Arrange
            var expectedException = new Exception("Test error");
            _mockCropCategoryService
                .Setup(service => service.GetCropCategories(new CancellationToken())).Throws(expectedException);
            // Act
            var actionResult = await _cropCategoriesController.GetCropCategories();
            // Assert
            var badRequestResult = actionResult.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
            error.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetCropCategories_ShouldReturnNoCropCategories()
        {
            //Arrange
            const int cropCategory = 1;
            var cropCategories = new List<CropCategoryModel>();
            _mockCropCategoryService
                .Setup(service => service.GetCropCategories(new CancellationToken())).ReturnsAsync(cropCategories);
            // Act
            var actionResult = await _cropCategoriesController.GetCropCategories();
            // Assert
            var result = Assert.IsType<ActionResult<List<CropCategoryModel>>>(actionResult);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCrops = Assert.IsAssignableFrom<List<CropCategoryModel>>(okResult.Value);
            Assert.Equal(cropCategories.Count, returnedCrops.Count);
        }
    }
}

