using eSusInsurers.Domain.Models;
using eSusInsurers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using eSusInsurerContext = eSusInsurers.Domain.eSusInsurerContext;

namespace eSusInsurers.Infrastructure.Tests.Repositories;

public class AppEventsAuTests
{
    private readonly DbContext _dbContext;
    private readonly AppEventsAuRepository _appEventsAuRepository;

    public AppEventsAuTests()
    {
        var options = new DbContextOptionsBuilder<eSusInsurerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _dbContext = new eSusInsurerContext(options);
        _appEventsAuRepository = new AppEventsAuRepository(_dbContext);
    }

    [Fact]
    public async Task Create_AppEventAu_Test()
    {
        var appEventAu = new eSusInsurers.Domain.Entities.AppEventsAu
        {
            CreatedDate = DateTime.Now,
            CreatedBy = "Test",
            EventName = "Test",
            EventId = 1,
            ModifiedBy = "test",
            ModifiedDate = DateTime.Now,
        };
        var createdEvent = await _appEventsAuRepository.AddAsync(appEventAu, new CancellationToken());
        Assert.NotNull(createdEvent);
        Assert.Equal("Test", createdEvent.EventName);
    }
    
}