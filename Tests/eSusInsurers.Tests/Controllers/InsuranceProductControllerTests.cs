using eSusInsurers.Controllers;
using eSusInsurers.Models;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.InsuranceProducts;
using eSusInsurers.Models.Roles.GetRoles;
using eSusInsurers.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace eSusInsurers.Tests.Controllers
{
    public class InsuranceProductControllerTests
    {
        private readonly Mock<IInsuranceProductService> _insuranceProductServiceMock;
        private readonly ITestOutputHelper _output;
        private readonly InsuranceProductController _controller;

        public InsuranceProductControllerTests(ITestOutputHelper output)
        {
            _insuranceProductServiceMock = new Mock<IInsuranceProductService>();
            _controller = new InsuranceProductController(_insuranceProductServiceMock.Object);
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
            var expectedResult = new PagedResult<InsuranceProductModel>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalPages = 1,
                TotalRecordCount = 1,
                Records = new List<InsuranceProductModel> { new InsuranceProductModel
                {
                    InsurancePolicyId  = 1
                } }
            };
            _insuranceProductServiceMock.Setup(s => s.GetInsuranceProducts(It.IsAny<InsuranceProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetInsuranceProducts(pagingOptions, filter, sort);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedResponse = Assert.IsType<PagedResult<InsuranceProductModel>>(okResult.Value);
            Assert.Equal(expectedResult.CurrentPage, returnedResponse.CurrentPage);
            Assert.Equal(expectedResult.PageSize, returnedResponse.PageSize);
            Assert.Equal(expectedResult.TotalPages, returnedResponse.TotalPages);
            Assert.Equal(expectedResult.TotalRecordCount, returnedResponse.TotalRecordCount);
            Assert.Equal(expectedResult.Records.Count, returnedResponse.Records.Count);
            Assert.Equal(expectedResult.Records[0].InsurancePolicyId, returnedResponse.Records[0].InsurancePolicyId);
            Assert.Equal(expectedResult.Records[0].CompanyName, returnedResponse.Records[0].CompanyName);
        }
        
         [Fact]
        public async Task GetCropCategories_ServiceThrowsException_ShouldReturnBadRequest()
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
            var expectedException = new Exception("Test error");
            _insuranceProductServiceMock.Setup(s => s.GetInsuranceProducts(It.IsAny<InsuranceProductQuery>(), It.IsAny<CancellationToken>()))
                .Throws(expectedException);

            // Act
            var result = await _controller.GetInsuranceProducts(pagingOptions, filter, sort);

            // Assert
            var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
            error.Should().NotBeNull();
        }
    }
}

