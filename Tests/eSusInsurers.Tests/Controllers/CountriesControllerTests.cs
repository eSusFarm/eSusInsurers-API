using eSusInsurers.Controllers;
using eSusInsurers.Models.Countries;
using eSusInsurers.Services.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace eSusInsurers.Tests.Controllers
{
   public class CountriesControllerTests
   {
       private readonly ICountriesService _countriesService;
       private readonly CountriesController _controller;

       public CountriesControllerTests()
       {
           _countriesService = A.Fake<ICountriesService>();
           _controller = new CountriesController(_countriesService);
       }

       [Fact]
       public async Task GetRegions_WhenSuccessful_ReturnsOkWithRegions()
       {
           // Arrange
           long countryId = 1;
           var expectedRegions = new List<RegionModel>
           {
               new() { RegionId = 1, RegionName = "East" },
               new() { RegionId = 2, RegionName = "West" }
           };

           A.CallTo(() => _countriesService.GetRegions(countryId, A<CancellationToken>._))
               .Returns(expectedRegions);

           // Act
           var result = await _controller.GetRegions(countryId);

           // Assert
           var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
           var regions = okResult.Value.Should().BeAssignableTo<List<RegionModel>>().Subject;
           regions.Should().BeEquivalentTo(expectedRegions);
       }
       
       [Fact]
       public async Task GetRegions_WhenNoRegions_ReturnsOkWithNoRegions()
       {
           // Arrange
           long countryId = 1;
           var expectedRegions = new List<RegionModel>();
           A.CallTo(() => _countriesService.GetRegions(countryId, A<CancellationToken>._))
               .Returns(expectedRegions);
           // Act
           var result = await _controller.GetRegions(countryId);
           // Assert
           var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
           var regions = okResult.Value.Should().BeAssignableTo<List<RegionModel>>().Subject;
           regions.Should().BeEquivalentTo(expectedRegions);
       }
       

       [Fact]
       public async Task GetRegions_WhenExceptionOccurs_ReturnsBadRequest()
       {
           // Arrange
           long countryId = 1;
           var expectedException = new Exception("Test error");

           A.CallTo(() => _countriesService.GetRegions(countryId, A<CancellationToken>._))
               .Throws(expectedException);

           // Act
           var result = await _controller.GetRegions(countryId);

           // Assert
           var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
           var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
           error.Should().NotBeNull();
       }

       [Fact]
       public async Task GetDistricts_WhenSuccessful_ReturnsOkWithDistricts()
       {
           // Arrange
           long countryId = 1;
           long regionId = 1;
           var expectedDistricts = new List<DistrictModel>
           {
               new() { DistrictId = 1, DistrictName = "District1" },
               new() { DistrictId = 2, DistrictName = "District2" }
           };

           A.CallTo(() => _countriesService.GetDistricts(countryId, regionId, A<CancellationToken>._))
               .Returns(expectedDistricts);

           // Act
           var result = await _controller.GetDistricts(countryId, regionId);

           // Assert
           var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
           var districts = okResult.Value.Should().BeAssignableTo<List<DistrictModel>>().Subject;
           districts.Should().BeEquivalentTo(expectedDistricts);
       }

       [Fact]
       public async Task GetSubcounties_WhenSuccessful_ReturnsOkWithSubcounties()
       {
           // Arrange
           long countryId = 1;
           long regionId = 1;
           long districtId = 1;
           var expectedSubcounties = new List<SubCountiesModel>
           {
               new() { SubCountyId = 1, SubCountyName = "Subcounty1" },
               new() { SubCountyId = 2, SubCountyName = "Subcounty2" }
           };

           A.CallTo(() => _countriesService.GetSubcounties(countryId, regionId, districtId, A<CancellationToken>._))
               .Returns(expectedSubcounties);

           // Act
           var result = await _controller.GetSubcounties(countryId, regionId, districtId);

           // Assert
           var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
           var subcounties = okResult.Value.Should().BeAssignableTo<List<SubCountiesModel>>().Subject;
           subcounties.Should().BeEquivalentTo(expectedSubcounties);
       }
       
       [Fact]
       public async Task GetDistricts_WhenExceptionOccurs_ReturnsBadRequest()
       {
           // Arrange
           long countryId = 1;
           long regionId = 1;
           var expectedException = new Exception("Test error");
           A.CallTo(() => _countriesService.GetDistricts(countryId, regionId,A<CancellationToken>._))
               .Throws(expectedException);
           // Act
           var result = await _controller.GetDistricts(countryId,regionId);
           // Assert
           var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
           var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
           error.Should().NotBeNull();
       }

       [Fact]
       public async Task GetParishes_WhenSuccessful_ReturnsOkWithParishes()
       {
           // Arrange
           long countryId = 1;
           long regionId = 1;
           long districtId = 1;
           long subcountyId = 1;
           var expectedParishes = new List<ParishModel>
           {
               new() { ParishId = 1, ParishName = "Parish1" },
               new() { ParishId = 2, ParishName = "Parish2" }
           };

           A.CallTo(() => _countriesService.GetParishes(countryId, regionId, districtId, subcountyId, A<CancellationToken>._))
               .Returns(expectedParishes);

           // Act
           var result = await _controller.GetParishes(countryId, regionId, districtId, subcountyId);

           // Assert
           var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
           var parishes = okResult.Value.Should().BeAssignableTo<List<ParishModel>>().Subject;
           parishes.Should().BeEquivalentTo(expectedParishes);
       }
       
       [Fact]
       public async Task GetParishes_WheneXCEPTION_ReturnsBadrequest()
       {
           // Arrange
           long countryId = 1;
           long regionId = 1;
           long districtId = 1;
           long subcountyId = 1;
           var expectedException = new Exception("Test error");
           A.CallTo(() => _countriesService.GetParishes(countryId, regionId, districtId, subcountyId, A<CancellationToken>._))
               .Throws(expectedException);

           // Act
           var result = await _controller.GetParishes(countryId, regionId, districtId, subcountyId);

           // Assert
           var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
           var error = badRequestResult.Value.Should().BeAssignableTo<object>().Subject;
           error.Should().NotBeNull();
       }
   }
}