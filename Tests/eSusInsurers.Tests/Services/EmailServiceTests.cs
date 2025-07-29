using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using eSusInsurers.Models.Common;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Helpers;
using EmailService.Models;
using EmailService.Interfaces;

public class EmailServiceTests
{
    private readonly Mock<IUpdateNotificationTemplate> _mockUpdateNotificationTemplate;
    private readonly Mock<IEmailSender> _mockEmailSender;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IEmailTemplateRepository> _mockEmailTemplateRepository;
    private readonly Mock<IServiceScopeFactory> _mockServiceScopeFactory;
    private readonly Mock<IServiceScope> _mockServiceScope;
    private readonly Mock<IServiceProvider> _mockServiceProvider;

    public EmailServiceTests()
    {
        _mockUpdateNotificationTemplate = new Mock<IUpdateNotificationTemplate>();
        _mockEmailSender = new Mock<IEmailSender>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockEmailTemplateRepository = new Mock<IEmailTemplateRepository>();
        _mockServiceScopeFactory = new Mock<IServiceScopeFactory>();
        _mockServiceScope = new Mock<IServiceScope>();
        _mockServiceProvider = new Mock<IServiceProvider>();

        _mockUnitOfWork.SetupGet(u => u.EmailTemplateRepository).Returns(_mockEmailTemplateRepository.Object);
        
        // Setup the service scope factory chain
        _mockServiceScopeFactory.Setup(x => x.CreateScope()).Returns(_mockServiceScope.Object);
        _mockServiceScope.Setup(x => x.ServiceProvider).Returns(_mockServiceProvider.Object);
        
        // Use the base GetService method instead of the generic extension method
        _mockServiceProvider.Setup(x => x.GetService(typeof(IUnitOfWork))).Returns(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task SendEmailAsync_ShouldSendEmail_WhenTemplateExists()
    {
        // Arrange
        var parameters = new NotificationContentParameters();
        var eventName = "TestEvent";
        var toAddresses = new[] { "test@example.com" };
        var attachments = Mock.Of<IFormFileCollection>();
        var cancellationToken = CancellationToken.None;

        var template = new EmailTemplate
        {
            MailSubject = "Subject",
            MailContent = "Content"
        };

        _mockEmailTemplateRepository
            .Setup(r => r.GetByEventNameAsync(eventName, cancellationToken))
            .ReturnsAsync(template);

        _mockUpdateNotificationTemplate
            .Setup(t => t.UpdateNotificationContentParametrs(parameters, template.MailSubject, eventName))
            .Returns("UpdatedSubject");

        _mockUpdateNotificationTemplate
            .Setup(t => t.UpdateNotificationContentParametrs(parameters, template.MailContent, eventName))
            .Returns("UpdatedContent");

        var fireForget = new FireForget(_mockServiceScopeFactory.Object);

        var emailService = new eSusInsurers.Services.Implementations.EmailService(
            _mockUpdateNotificationTemplate.Object,
            _mockEmailSender.Object,
            _mockUnitOfWork.Object,
            fireForget
        );

        // Act
        await emailService.SendEmailAsync(parameters, eventName, toAddresses, attachments, cancellationToken);

        // Give time for the fire-and-forget operation to complete
        await Task.Delay(500);

        // Assert
        _mockEmailSender.Verify(
            s => s.SendEmailAsync(It.Is<Message>(m =>
                m.Subject == "UpdatedSubject" &&
                m.Content == "UpdatedContent" &&
                m.Attachments != null && m.Attachments.Count == attachments.Count
            )),
            Times.Once
        );
    }

    [Fact]
    public async Task SendEmailAsync_ShouldNotSendEmail_WhenTemplateDoesNotExist()
    {
        // Arrange
        var parameters = new NotificationContentParameters();
        var eventName = "MissingEvent";
        var toAddresses = new[] { "test@example.com" };
        var cancellationToken = CancellationToken.None;

        _mockEmailTemplateRepository
            .Setup(r => r.GetByEventNameAsync(eventName, cancellationToken))
            .ReturnsAsync((EmailTemplate?)null);

        var fireForget = new FireForget(_mockServiceScopeFactory.Object);

        var emailService = new eSusInsurers.Services.Implementations.EmailService(
            _mockUpdateNotificationTemplate.Object,
            _mockEmailSender.Object,
            _mockUnitOfWork.Object,
            fireForget
        );

        // Act
        await emailService.SendEmailAsync(parameters, eventName, toAddresses, null, cancellationToken);

        // Give time for the fire-and-forget operation to complete
        await Task.Delay(500);

        // Assert
        _mockEmailSender.Verify(s => s.SendEmailAsync(It.IsAny<Message>()), Times.Never);
    }

    [Fact]
    public async Task SendEmailAsync_ShouldHandleException_WhenRepositoryThrows()
    {
        // Arrange
        var parameters = new NotificationContentParameters();
        var eventName = "TestEvent";
        var toAddresses = new[] { "test@example.com" };
        var cancellationToken = CancellationToken.None;

        _mockEmailTemplateRepository
            .Setup(r => r.GetByEventNameAsync(eventName, cancellationToken))
            .ThrowsAsync(new InvalidOperationException("DB error"));

        var fireForget = new FireForget(_mockServiceScopeFactory.Object);

        var emailService = new eSusInsurers.Services.Implementations.EmailService(
            _mockUpdateNotificationTemplate.Object,
            _mockEmailSender.Object,
            _mockUnitOfWork.Object,
            fireForget
        );

        // Act
        await emailService.SendEmailAsync(parameters, eventName, toAddresses, null, cancellationToken);

        // Give time for the fire-and-forget operation to complete
        await Task.Delay(500);

        // Assert - The exception should be caught and handled by FireForget, so no email should be sent
        _mockEmailSender.Verify(s => s.SendEmailAsync(It.IsAny<Message>()), Times.Never);
    }
}