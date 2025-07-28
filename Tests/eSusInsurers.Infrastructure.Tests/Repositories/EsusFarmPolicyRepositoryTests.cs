using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories;

public class EsusFarmPolicyRepositoryTests
{
    private readonly DbContext _dbContext;
    private readonly Mock<IUnitOfWork> _unitOfWork; 
    private readonly EsusFarmPolicyRepository _sut;

    public EsusFarmPolicyRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<eSusInsurerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _unitOfWork = new Mock<IUnitOfWork>();
        _dbContext = new eSusInsurerContext(options);
        _sut = new EsusFarmPolicyRepository(_dbContext);
        _unitOfWork.Setup(x => x.EsusFarmPolicyRepository).Returns(_sut);
    }

    [Fact]
    public async Task AddEsusFarmPolicy_ShouldCreatePolicy()
    {
        var policy = CreateEsusFarmPolicy();
        var createdPolicy = await _sut.AddAsync(policy, CancellationToken.None);
        Assert.NotNull(createdPolicy);
        Assert.Equal(policy.PolicyId, createdPolicy.PolicyId);
    }

    private EsusFarmPolicy CreateEsusFarmPolicy()
    {
        return new EsusFarmPolicy
        {
            ExternalId = "TEST",
            PolicyId = "TEST",
            PersonId = "TEST",
            PremiumAmount = 250,
            RiskId ="1"
        };
    }
}