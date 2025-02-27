using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eSusInsurers.Controllers;
using eSusInsurers.Models.InsuranceCompany;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace eSusInsurers.Tests.Controllers
{
    public class InsuranceCompanyControllerTests
    {
        private readonly Mock<IInsuranceCompanyService> _mockInsuranceCompanyService;
        private readonly InsuranceCompanyController _controller;

        public InsuranceCompanyControllerTests()
        {
            _mockInsuranceCompanyService = new Mock<IInsuranceCompanyService>();
            _controller = new InsuranceCompanyController(_mockInsuranceCompanyService.Object);
        }

        [Fact]
        public async Task GetInsuranceCompanies_ReturnsOkResult_WithListOfCompanies()
        {
            // Arrange
            var expectedCompanies = new List<InsuranceCompanyModel>
            {
                new InsuranceCompanyModel { CompanyId = 1, CompanyName = "Company A" },
                new InsuranceCompanyModel { CompanyId = 2, CompanyName = "Company B" }
            };

            _mockInsuranceCompanyService
                .Setup(service => service.GetCompanies(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCompanies);

            // Act
            var result = await _controller.GetInsuranceCompanies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCompanies = Assert.IsType<List<InsuranceCompanyModel>>(okResult.Value);
            Assert.Equal(expectedCompanies.Count, returnedCompanies.Count);
            Assert.Equal(expectedCompanies[0].CompanyId, returnedCompanies[0].CompanyId);
            Assert.Equal(expectedCompanies[0].CompanyName, returnedCompanies[0].CompanyName);
            Assert.Equal(expectedCompanies[1].CompanyId, returnedCompanies[1].CompanyId);
            Assert.Equal(expectedCompanies[1].CompanyName, returnedCompanies[1].CompanyName);
        }

        [Fact]
        public async Task GetInsuranceCompanies_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var expectedErrorMessage = "An error occurred while fetching insurance companies.";
            _mockInsuranceCompanyService
                .Setup(service => service.GetCompanies(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(expectedErrorMessage));

            // Act
            var result = await _controller.GetInsuranceCompanies();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<List<InsuranceCompanyModel>>>(result);
            var badRequestResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            var errorObject = Assert.IsAssignableFrom<object>(badRequestResult.Value);
            Assert.Equal(expectedErrorMessage, errorObject.GetType().GetProperty("ErrorMessage")?.GetValue(errorObject));
        }
    }

    // Helper class to represent the anonymous object returned in the BadRequest case
    class Anonymous<T>
    {
        public T ErrorMessage { get; set; }
    }
}

