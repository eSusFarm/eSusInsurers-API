using eSusInsurers.Domain.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Models;

[TestSubject(typeof(eSusInsurerContext))]
public class eSusInsurerContextTest
{
    private eSusInsurerContext _insurerContext;
    private Mock<DbContextOptions<eSusInsurerContext>> _optionsMock;
    private Mock<ModelBuilder> _modelBuilderMock;
    public eSusInsurerContextTest()
    {
        _optionsMock = new Mock<DbContextOptions<eSusInsurerContext>>();
        _modelBuilderMock = new Mock<ModelBuilder>();
        var options = new DbContextOptionsBuilder<eSusInsurerContext>()
            .UseInMemoryDatabase(databaseName: "esusinsurer_nonprod")
            .Options;
        _insurerContext = new eSusInsurerContext(options);
    }


    [Fact]
    public void OnModelCreating_AppEvent_ShouldCreateTableSuccessfully()
    {
        _insurerContext.Database.EnsureCreated();
        // Act - Ensure the model is built
        _insurerContext.Database.EnsureCreated(); 

        // Assert - Query the in-memory database to verify schema elements
        // Example: Check if a table exists, if a column is present, etc.
        var tableExists = _insurerContext.Model.FindEntityType(typeof(AppEvent)) != null;
        Assert.True(tableExists);
        // Further assertions on properties, relationships, etc.
    }
    
    [Fact]
    public void OnModelCreating_AppEventsAu_ShouldCreateTableSuccessfully()
    {
        // Act - Ensure the model is built
        _insurerContext.Database.EnsureCreated(); 

        // Assert - Query the in-memory database to verify schema elements
        // Example: Check if a table exists, if a column is present, etc.
        var tableExists = _insurerContext.Model.FindEntityType(typeof(AppEventsAu)) != null;
        Assert.True(tableExists);
        // Further assertions on properties, relationships, etc.
    }
    
    [Fact]
    public void OnModelCreating_ApplicationChildMenu_ShouldCreateTableSuccessfully()
    {
        // Act - Ensure the model is built
        _insurerContext.Database.EnsureCreated(); 

        // Assert - Query the in-memory database to verify schema elements
        // Example: Check if a table exists, if a column is present, etc.
        var tableExists = _insurerContext.Model.FindEntityType(typeof(ApplicationChildMenu)) != null;
        Assert.True(tableExists);
        // Further assertions on properties, relationships, etc.
    }
    
    [Fact]
    public void OnModelCreating_ApplicationFunctionality_ShouldCreateTableSuccessfully()
    {
        // Act - Ensure the model is built
        _insurerContext.Database.EnsureCreated(); 
        // Assert - Query the in-memory database to verify schema elements
        // Example: Check if a table exists, if a column is present, etc.
        var tableExists = _insurerContext.Model.FindEntityType(typeof(ApplicationFunctionality)) != null;
        Assert.True(tableExists);
        // Further assertions on properties, relationships, etc.
    }
}