using EmailService.Interfaces;
using EmailService.Models;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models.Common;
using eSusInsurers.Services.Interfaces;

namespace eSusInsurers.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IUpdateNotificationTemplate _updateNotificationTemplate;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IUpdateNotificationTemplate updateNotificationTemplate
                          , IEmailSender emailSender
                          , IUnitOfWork unitOfWork)
        {
            _updateNotificationTemplate = updateNotificationTemplate;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }

        public async Task SendEmailAsync(NotificationContentParameters parameters
                                                     , string eventName
                                                     , string[] ToAddress
                                                     , CancellationToken cancellationToken)
        {
            try
            {
                var templateDetails = await _unitOfWork.EmailTemplateRepository.GetByEventNameAsync(eventName, cancellationToken);

                if (templateDetails != null)
                {
                    var subject = _updateNotificationTemplate.UpdateNotificationContentParametrs(parameters, templateDetails.MailSubject, eventName);
                    var content = _updateNotificationTemplate.UpdateNotificationContentParametrs(parameters, templateDetails.MailContent, eventName);
                    var message = new Message(ToAddress, subject, content, null);
                    await _emailSender.SendEmailAsync(message);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}