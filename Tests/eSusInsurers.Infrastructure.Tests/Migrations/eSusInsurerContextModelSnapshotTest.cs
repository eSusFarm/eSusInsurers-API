using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Domain.Migrations;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Migrations;

[TestSubject(typeof(eSusInsurerContextModelSnapshot))]
public class eSusInsurerContextModelSnapshotTest
{
    private eSusInsurerContextModelSnapshot _insurerContext;
    private Mock<DbContextOptions<eSusInsurerContext>> _optionsMock;
    private Mock<ModelBuilder> _modelBuilderMock;

    public eSusInsurerContextModelSnapshotTest()
    {
        _optionsMock = new Mock<DbContextOptions<eSusInsurerContext>>();
        _modelBuilderMock = new Mock<ModelBuilder>();
        var options = new DbContextOptionsBuilder<eSusInsurerContext>()
            .UseInMemoryDatabase(databaseName: "esusinsurer_nonprod")
            .Options;
        _insurerContext = new eSusInsurerContextModelSnapshot();
    }

    [Fact]
    public void OnModelCreating_AppEvent_ShouldCreateTableSuccessfully()
    {
        // Assert - Query the in-memory database to verify schema elements
        // Example: Check if a table exists, if a column is present, etc.
        var tableExists = _insurerContext.Model.FindEntityType(typeof(AppEvent)) != null;
        Assert.True(tableExists);
        // Further assertions on properties, relationships, etc.
    }
}