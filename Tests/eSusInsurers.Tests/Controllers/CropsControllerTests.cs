using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using eSusInsurers.Controllers;
using eSusInsurers.Models;
using eSusInsurers.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;

namespace eSusInsurers.Tests.Controllers
{
    public class CropsControllerTests
    {
        private readonly Mock<ICropService> _mockCropService;
        private readonly CropsController _controller;
        private readonly ITestOutputHelper _output;

        public CropsControllerTests(ITestOutputHelper output)
        {
            _mockCropService = new Mock<ICropService>();
            _controller = new CropsController(_mockCropService.Object);
            _output = output;
        }

        [Fact]
        public async Task GetCropsByCropCategoryId_ReturnsOkResult_WithCrops()
        {
            // Arrange
            const int cropCategoryId = 1;
            var expectedCrops = new List<CropModel>
            {
                new CropModel { CropId = 1, CropName = "Wheat" },
                new CropModel { CropId = 2, CropName = "Corn" }
            };

            _mockCropService
                .Setup(service => service.GetCropsByCropCategoryId(cropCategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCrops);

            // Act
            var actionResult = await _controller.GetCropsByCropCategoryId(cropCategoryId);

            // Assert
            var result = Assert.IsType<ActionResult<CropModel>>(actionResult);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCrops = Assert.IsAssignableFrom<List<CropModel>>(okResult.Value);
            Assert.Equal(expectedCrops.Count, returnedCrops.Count);
        }
        
        [Fact]
        public async Task GetCropsByCropCategoryId_ReturnsOkResult_WithNoCrops()
        {
            // Arrange
            const int cropCategoryId = 1;
            var expectedCrops = new List<CropModel>();

            _mockCropService
                .Setup(service => service.GetCropsByCropCategoryId(cropCategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCrops);

            // Act
            var actionResult = await _controller.GetCropsByCropCategoryId(cropCategoryId);

            // Assert
            var result = Assert.IsType<ActionResult<CropModel>>(actionResult);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCrops = Assert.IsAssignableFrom<List<CropModel>>(okResult.Value);
            Assert.Equal(expectedCrops.Count, returnedCrops.Count);
        }

        [Fact]
        public async Task GetCropsByCropCategoryId_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            const int cropCategoryId = 1;
            var expectedException = new Exception("Test exception");

            _mockCropService
                .Setup(service => service.GetCropsByCropCategoryId(cropCategoryId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(expectedException);

            // Act
            var actionResult = await _controller.GetCropsByCropCategoryId(cropCategoryId);

            // Assert
            var result = Assert.IsType<ActionResult<CropModel>>(actionResult);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.NotNull(badRequestResult.Value);

            // Debug the structure of the response
            _output.WriteLine($"BadRequest Value Type: {badRequestResult.Value.GetType()}");
            _output.WriteLine($"BadRequest Value: {JsonConvert.SerializeObject(badRequestResult.Value, Formatting.Indented)}");

            // Get the properties of the anonymous type
            var properties = badRequestResult.Value.GetType().GetProperties();
            foreach (var prop in properties)
            {
                _output.WriteLine($"Property: {prop.Name} = {prop.GetValue(badRequestResult.Value)}");
            }

            // Try to get the error message
            var actualErrorMessage = properties
                .FirstOrDefault(p => p.Name.ToLower().Contains("error") || p.Name.ToLower().Contains("message"))
                ?.GetValue(badRequestResult.Value)?.ToString();

            Assert.NotNull(actualErrorMessage);
            Assert.Equal(expectedException.Message, actualErrorMessage);
        }
    }
}