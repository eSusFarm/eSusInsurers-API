using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Repositories;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eSusInsurers.Infrastructure.Tests.Repositories;

[TestSubject(typeof(EmailTemplateRepository))]
public class EmailTemplateRepositoryTest
{
    private readonly DbContext _dbContext;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly EmailTemplateRepository _emailTemplateRepository;

    public EmailTemplateRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<eSusInsurerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _unitOfWork = new Mock<IUnitOfWork>();
        _dbContext = new eSusInsurerContext(options);
        _emailTemplateRepository = new EmailTemplateRepository(_dbContext);
        _unitOfWork.Setup(x=> x.EmailTemplateRepository).Returns(_emailTemplateRepository);
    }
    
    [Fact]
    public async Task AddEmailTemplates_ShouldAddEmailTemplates()
    {
        var emailTemplate = await _unitOfWork.Object.EmailTemplateRepository.AddAsync(new EmailTemplate{Event = new AppEvent{EventName = "Test"}, IsActive = true}, CancellationToken.None);
        Assert.NotNull(emailTemplate);
        Assert.Equal("Test", emailTemplate.Event.EventName);
    }
    
    private async Task<EmailTemplate> createEmailTemplate()
    {
        var emailTemplate = await _unitOfWork.Object.EmailTemplateRepository.AddAsync(new EmailTemplate{Event = new AppEvent{EventName = "Test"}, IsActive = true}, CancellationToken.None);
        await _unitOfWork.Object.SaveChangesAsync(CancellationToken.None);
        return emailTemplate;
    }
}