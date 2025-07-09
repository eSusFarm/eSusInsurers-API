using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using eSusInsurers.Models.Common;
using eSusInsurers.Infrastructure.Common;
using EmailService.Models;
using EmailService.Interfaces;

public class EmailServiceTests
{
    private readonly Mock<IUpdateNotificationTemplate> _mockUpdateNotificationTemplate;
    private readonly Mock<IEmailSender> _mockEmailSender;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IEmailTemplateRepository> _mockEmailTemplateRepository;
    private readonly Mock<FireForget> _mockFireForget;

    public EmailServiceTests()
    {
        _mockUpdateNotificationTemplate = new Mock<IUpdateNotificationTemplate>();
        _mockEmailSender = new Mock<IEmailSender>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockEmailTemplateRepository = new Mock<IEmailTemplateRepository>();
        _mockFireForget = new Mock<FireForget>(MockBehavior.Loose, null as IServiceScopeFactory);

        _mockUnitOfWork.SetupGet(u => u.EmailTemplateRepository).Returns(_mockEmailTemplateRepository.Object);
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

        var emailService = new EmailService(
            _mockUpdateNotificationTemplate.Object,
            _mockEmailSender.Object,
            _mockUnitOfWork.Object,
            new FireForgetStub((func) => func(_mockUnitOfWork.Object))
        );

        // Act
        await emailService.SendEmailAsync(parameters, eventName, toAddresses, attachments, cancellationToken);

        // Assert
        _mockEmailSender.Verify(
            s => s.SendEmailAsync(It.Is<Message>(m =>
                m.Subject == "UpdatedSubject" &&
                m.Content == "UpdatedContent" &&
                m.Attachments == attachments
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

        var emailService = new EmailService(
            _mockUpdateNotificationTemplate.Object,
            _mockEmailSender.Object,
            _mockUnitOfWork.Object,
            new FireForgetStub((func) => func(_mockUnitOfWork.Object))
        );

        // Act
        await emailService.SendEmailAsync(parameters, eventName, toAddresses, null, cancellationToken);

        // Assert
        _mockEmailSender.Verify(s => s.SendEmailAsync(It.IsAny<Message>()), Times.Never);
    }

    [Fact]
    public async Task SendEmailAsync_ShouldThrow_WhenExceptionOccurs()
    {
        // Arrange
        var parameters = new NotificationContentParameters();
        var eventName = "TestEvent";
        var toAddresses = new[] { "test@example.com" };
        var cancellationToken = CancellationToken.None;

        _mockEmailTemplateRepository
            .Setup(r => r.GetByEventNameAsync(eventName, cancellationToken))
            .ThrowsAsync(new InvalidOperationException("DB error"));

        var emailService = new EmailService(
            _mockUpdateNotificationTemplate.Object,
            _mockEmailSender.Object,
            _mockUnitOfWork.Object,
            new FireForgetStub((func) => func(_mockUnitOfWork.Object))
        );

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            emailService.SendEmailAsync(parameters, eventName, toAddresses, null, cancellationToken)
        );
    }

    // Helper stub to synchronously execute the FireForget delegate for testing
    private class FireForgetStub : FireForget
    {
        private readonly Func<Func<IUnitOfWork, Task>, Task> _executor;

        public FireForgetStub(Func<Func<IUnitOfWork, Task>, Task> executor)
            : base(Mock.Of<IServiceScopeFactory>())
        {
            _executor = executor;
        }

        public override void Execute<TService>(Func<TService, Task> func)
        {
            // Synchronously execute for test
            _executor(func as Func<IUnitOfWork, Task>).GetAwaiter().GetResult();
        }
    }
}