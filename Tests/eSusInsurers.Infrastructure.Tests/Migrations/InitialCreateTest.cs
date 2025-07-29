using eSusInsurers.Domain.Entities;
using eSusInsurers.Domain.Migrations;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Migrations;

[TestSubject(typeof(InitialCreate))]
public class InitialCreateTest
{

    private eSusInsurers.Domain.Migrations.InitialCreate _initialCreate;


    public InitialCreateTest()
    {
        _initialCreate = new InitialCreate();
    }

    [Fact]
    public void OnModelCreating_AppEvent_ShouldCreateTableSuccessfully()
    {
        var tableExists =  _initialCreate.TargetModel.FindEntityTypes(typeof(AppEvent)) != null;
        Assert.True(tableExists);
    }
    
    [Fact]
    public void OnUpCreating_Count_ShouldNotBeZero()
    {
        Assert.True(_initialCreate.UpOperations.Count > 0 );
    }
    
    [Fact]
    public void OnDownCreating_Count_ShouldNotBeZero()
    {
        Assert.True(_initialCreate.DownOperations.Count > 0 );
    }
}