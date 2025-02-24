using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eSusInsurers.Controllers;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.Roles.GetRoles;
using eSusInsurers.Models.Roles.RoleDetails;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WMS.Models.Roles;
using WMS.Models.Roles.UpdateRole;
using Xunit;

namespace eSusInsurers.Tests.Controllers
{
    public class RoleControllerTests
    {
        private readonly Mock<IRolesService> _mockRolesService;
        private readonly RoleController _controller;

        public RoleControllerTests()
        {
            _mockRolesService = new Mock<IRolesService>();
            _controller = new RoleController(_mockRolesService.Object);
        }

        [Fact]
        public async Task GetApplicationMenuItemsMasterList_ReturnsOkResult_WithApplicationMenuItems()
        {
            // Arrange
            var expectedResult = new List<ApplicationMenuItems>();
            _mockRolesService.Setup(s => s.GetApplicationMenuItems(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetApplicationMenuItemsMasterList(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedApplicationMenuItems = Assert.IsType<List<ApplicationMenuItems>>(okResult.Value);
            Assert.Equal(expectedResult, returnedApplicationMenuItems);
        }

        [Fact]
        public async Task GetRoles_ReturnsOkResult_WithPagedResultOfRoleModel()
        {
            // Arrange
            var pagingOptions = new PagingOptions { Page = 1, PageSize = 10 };
            var filter = new RoleFilterOptions();
            var sort = new SortingOptions();
            var expectedResult = new PagedResult<RoleModel>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalPages = 1,
                TotalRecordCount = 1,
                Records = new List<RoleModel> { new RoleModel { RoleId  = 1, Role = "TestRole" } }
            };

            _mockRolesService.Setup(s => s.GetRoles(It.IsAny<GetRolesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetRoles(pagingOptions, filter, sort);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedResponse = Assert.IsType<PagedResult<RoleModel>>(okResult.Value);
            Assert.Equal(expectedResult.CurrentPage, returnedResponse.CurrentPage);
            Assert.Equal(expectedResult.PageSize, returnedResponse.PageSize);
            Assert.Equal(expectedResult.TotalPages, returnedResponse.TotalPages);
            Assert.Equal(expectedResult.TotalRecordCount, returnedResponse.TotalRecordCount);
            Assert.Equal(expectedResult.Records.Count, returnedResponse.Records.Count);
            Assert.Equal(expectedResult.Records[0].RoleId, returnedResponse.Records[0].RoleId);
            Assert.Equal(expectedResult.Records[0].Role, returnedResponse.Records[0].Role);
        }

        [Fact]
        public async Task AddRole_ReturnsCreatedResult_WhenSuccessful()
        {
            // Arrange
            var request = new RoleRequest();
            long expectedRoleId = 1;

            _mockRolesService.Setup(s => s.AddRole(It.IsAny<RoleRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRoleId);

            // Act
            var result = await _controller.AddRole(request);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(expectedRoleId, createdResult.Value);
        }

        [Fact]
        public async Task UpdateRole_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            int roleId = 1;
            var request = new UpdateRoleRequestModel();

            _mockRolesService.Setup(s => s.UpdateRole(It.IsAny<int>(), It.IsAny<UpdateRoleRequestModel>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateRole(roleId, request);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task GetApplicationMenuItemsMasterList_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            _mockRolesService.Setup(s => s.GetApplicationMenuItems(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetApplicationMenuItemsMasterList(CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Test exception", (badRequestResult.Value as dynamic).ErrorMessage);
        }

        [Fact]
        public async Task GetRoles_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            _mockRolesService.Setup(s => s.GetRoles(It.IsAny<GetRolesQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetRoles();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Test exception", (badRequestResult.Value as dynamic).ErrorMessage);
        }

        [Fact]
        public async Task AddRole_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var request = new RoleRequest();
            _mockRolesService.Setup(s => s.AddRole(It.IsAny<RoleRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.AddRole(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Test exception", (badRequestResult.Value as dynamic).ErrorMessage);
        }

        [Fact]
        public async Task UpdateRole_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            int roleId = 1;
            var request = new UpdateRoleRequestModel();
            _mockRolesService.Setup(s => s.UpdateRole(It.IsAny<int>(), It.IsAny<UpdateRoleRequestModel>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.UpdateRole(roleId, request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Test exception", (badRequestResult.Value as dynamic).ErrorMessage);
        }

        [Fact]
        public async Task RoleDetails_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            int roleId = 1;
            _mockRolesService.Setup(s => s.RoleDetails(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.RoleDetails(roleId, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Test exception", (badRequestResult.Value as dynamic).ErrorMessage);
        }
    }
}

