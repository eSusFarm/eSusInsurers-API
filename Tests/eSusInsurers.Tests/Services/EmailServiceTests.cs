using EmailService.Interfaces;
using EmailService.Models;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Helpers;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Common;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace eSusInsurers.Tests.Services
{
   public class EmailServiceTests
   {
       private readonly IEmailSender _emailSender;
       private readonly FireForget _fireForget;
       private readonly IUnitOfWork _unitOfWork;
       private readonly IUpdateNotificationTemplate _updateTemplate;
       private readonly eSusInsurers.Services.Implementations.EmailService _emailService;

       public EmailServiceTests()
       {
           _emailSender = A.Fake<IEmailSender>();
           _fireForget = A.Fake<FireForget>();
           _unitOfWork = A.Fake<IUnitOfWork>();
           _updateTemplate = A.Fake<IUpdateNotificationTemplate>();

           _emailService = new eSusInsurers.Services.Implementations.EmailService(
               _updateTemplate,
               _emailSender,
               _unitOfWork, 
               _fireForget);

           // Setup FireForget to execute the action immediately for testing
           A.CallTo(() => _fireForget.Execute<IUnitOfWork>(A<Func<IUnitOfWork, Task>>.Ignored))
               .Invokes((Func<IUnitOfWork, Task> action) => action(_unitOfWork));
       }


       [Fact]
       public async Task SendEmailAsync_WithAttachments_SendsEmailWithAttachments()
       {
           // Arrange
           var parameters = new NotificationContentParameters();
           var eventName = "test_event";
           var toAddress = new[] { "test@example.com" };
           var attachments = A.Fake<IFormFileCollection>();
           var template = new EmailTemplate 
           { 
               MailSubject = "Test Subject",
               MailContent = "Test Content"
           };

           A.CallTo(() => _unitOfWork.EmailTemplateRepository.GetByEventNameAsync(eventName, A<CancellationToken>._))
               .Returns(template);

           // Act
           await _emailService.SendEmailAsync(parameters, eventName, toAddress, attachments);

           // Assert
           A.CallTo(() => _emailSender.SendEmailAsync(A<Message>.That.Matches(m => 
               m.Attachments == attachments)))
               .MustHaveHappenedOnceExactly();
       }

       [Fact]
       public async Task SendEmailAsync_WithNonExistentTemplate_DoesNotSendEmail()
       {
           // Arrange
           var parameters = new NotificationContentParameters();
           var eventName = "non_existent_event";
           var toAddress = new[] { "test@example.com" };

           A.CallTo(() => _unitOfWork.EmailTemplateRepository.GetByEventNameAsync(eventName, A<CancellationToken>._))
               .Returns((EmailTemplate)null);

           // Act
           await _emailService.SendEmailAsync(parameters, eventName, toAddress);

           // Assert
           A.CallTo(() => _emailSender.SendEmailAsync(A<Message>._))
               .MustNotHaveHappened();
       }
   }
}