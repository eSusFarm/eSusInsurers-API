using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories;

public class AppEventTests
{
    private readonly DbContext _dbContext;
    private readonly AppEventRepository _appEventRepository;

    public AppEventTests()
    { var options = new DbContextOptionsBuilder<eSusInsurerContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _dbContext = new eSusInsurerContext(options);
        _appEventRepository = new AppEventRepository(_dbContext);
    }

    [Fact]
    public async Task Create_AppEvent_Should_Be_Created()
    {
        var appEvent = new AppEvent
        {
            EventName = "Test",
            IsActive = true
        };
        var createdAppEvent = await _appEventRepository.AddAsync(appEvent, CancellationToken.None);
        Assert.NotNull(createdAppEvent);
        Assert.Equal("Test", createdAppEvent.EventName);
    }
}