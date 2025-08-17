using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories
{
    public class ApplicationChildMenuRepositoryTests
    {
        private readonly DbContext _dbContext;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IApplicationChildMenuRepository _repository;

        public ApplicationChildMenuRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<eSusInsurerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _unitOfWork = new Mock<IUnitOfWork>();
            _dbContext = new eSusInsurerContext(options);
            _repository = new ApplicationChildMenuRepository(_dbContext);
        }

        [Fact]
        public async Task GetApplicationChildMenus_FiltersOut_NonMatching_MenuId_And_Inactive()
        {
            var targetMenuId = 1;
            var otherMenuId = 2;

            _dbContext.Set<ApplicationChildMenu>().AddRange(
                new ApplicationChildMenu
                {
                    ApplicationMenuId = targetMenuId,
                    ApplicationChildMenuName = "Child A",
                    ApplicationChildMenuLink = "/a",
                    ApplicationChildMenuIcon = "icon-a",
                    IsActive = true,
                    Sequence = 1,
                    CreatedBy = "TestUser",
                },
                new ApplicationChildMenu
                {
                    ApplicationMenuId = targetMenuId,
                    ApplicationChildMenuName = "Child B",
                    ApplicationChildMenuLink = "/b",
                    ApplicationChildMenuIcon = "icon-b",
                    IsActive = true,
                    Sequence = 2,
                    CreatedBy = "TestUser",
                },
                new ApplicationChildMenu
                {
                    ApplicationMenuId = otherMenuId,
                    ApplicationChildMenuName = "Child C",
                    ApplicationChildMenuLink = "/c",
                    ApplicationChildMenuIcon = "icon-c",
                    IsActive = true,
                    Sequence = 3,
                    CreatedBy = "TestUser",
                },
                new ApplicationChildMenu
                {
                    ApplicationMenuId = targetMenuId,
                    ApplicationChildMenuName = "Child D",
                    ApplicationChildMenuLink = "/d",
                    ApplicationChildMenuIcon = "icon-d",
                    IsActive = false, // Inactive menu
                    Sequence = 4,
                    CreatedBy = "TestUser",
                }
            );
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetApplicationChildMenus(targetMenuId, CancellationToken.None);

            Assert.Equal(2, result.Count);
            Assert.All(result, x =>
            {
                Assert.True(x.IsActive);
                Assert.Equal(targetMenuId, x.ApplicationMenuId);
            });

            Assert.True(result.SequenceEqual(result.OrderBy(r => r.Sequence)),
                "Results are not ordered by Sequence as expected.");
            Assert.Collection(result,
                first => Assert.Equal(1, first.Sequence),
                second => Assert.Equal(2, second.Sequence));
        }
    }
}
