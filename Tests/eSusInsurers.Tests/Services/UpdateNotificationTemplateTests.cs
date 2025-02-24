using System;
using eSusInsurers.Models.Common;
using eSusInsurers.Services.Implementations;
using Xunit;

namespace eSusInsurers.Tests.Services
{
    public class UpdateNotificationTemplateTests
    {
        private readonly UpdateNotificationTemplate _updateNotificationTemplate;

        public UpdateNotificationTemplateTests()
        {
            _updateNotificationTemplate = new UpdateNotificationTemplate();
        }

        [Fact]
        public void UpdateNotificationContentParametrs_ReplacesPlaceholdersCorrectly()
        {
            // Arrange
            var parameters = new NotificationContentParameters
            {
                Index0 = "Value0",
                Index1 = "Value1",
                Index2 = "Value2",
                Index3 = "Value3",
                URL = "http://example.com",
                UserName = "TestUser",
                AppName = "TestApp",
                EmailId = "test@example.com",
                Password = "TestPassword",
                CompanyName = "TestCompany"
            };

            var notificationContent = "Hello [username], welcome to [AppName]! " +
                                      "Your account details: Email: [emailId], Password: [password]. " +
                                      "Please visit [url] for more information. " +
                                      "Your company [CompanyName] has the following details: " +
                                      "[index0], [index1], [index2], [index3].";

            var eventName = "TestEvent";

            // Act
            var result = _updateNotificationTemplate.UpdateNotificationContentParametrs(
                parameters, notificationContent, eventName);

            // Assert
            Assert.Contains("Hello TestUser, welcome to TestApp!", result);
            Assert.Contains("Your account details: Email: test@example.com, Password: TestPassword.", result);
            Assert.Contains("Please visit http://example.com for more information.", result);
            Assert.Contains("Your company TestCompany has the following details:", result);
            Assert.Contains("Value0, Value1, Value2, Value3.", result);
            Assert.DoesNotContain("[username]", result);
            Assert.DoesNotContain("[AppName]", result);
            Assert.DoesNotContain("[emailId]", result);
            Assert.DoesNotContain("[password]", result);
            Assert.DoesNotContain("[url]", result);
            Assert.DoesNotContain("[CompanyName]", result);
            Assert.DoesNotContain("[index0]", result);
            Assert.DoesNotContain("[index1]", result);
            Assert.DoesNotContain("[index2]", result);
            Assert.DoesNotContain("[index3]", result);
        }

        [Fact]
        public void UpdateNotificationContentParametrs_ReturnsEmptyStringOnException()
        {
            // Arrange
            var parameters = new NotificationContentParameters();
            string notificationContent = null;
            var eventName = "TestEvent";

            // Act
            var result = _updateNotificationTemplate.UpdateNotificationContentParametrs(
                parameters, notificationContent, eventName);

            // Assert
            Assert.Equal(string.Empty, result);
        }
    }
}

