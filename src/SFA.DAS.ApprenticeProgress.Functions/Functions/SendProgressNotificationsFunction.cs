using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeProgress.Application.Interfaces;
using SFA.DAS.ApprenticeProgress.Functions.Api.Clients;
using SFA.DAS.ApprenticeProgress.Functions.Services;
using SFA.DAS.PushNotifications.Messages.Commands;

namespace SFA.DAS.ApprenticeProgress.Functions.Functions;

public class SendProgressNotificationsFunction
{
    private readonly ILogger _logger;
    private readonly IApprenticeProgressApiClient _api;
    private readonly IMessageService _messageService;
    private readonly IContentfulService _contentfulService;

    public SendProgressNotificationsFunction(ILoggerFactory loggerFactory, IApprenticeProgressApiClient api, IMessageService messageService, IContentfulService contentfulService)
    {
        _logger = loggerFactory.CreateLogger<SendProgressNotificationsFunction>();
        _api = api;
        _messageService = messageService;
        _contentfulService = contentfulService;
    }

    [Function("SendProgressNotificationsFunction")]
    public async Task Run([TimerTrigger("0 */1 * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        try
        {
            var entryId = "1Xn9hSw5ieLULxM9EPq3FV";
            var content = await _contentfulService.GetContentAsync(entryId);

            _logger.LogInformation(
                "Contentful Title: {Header}",
                content.Header);

            _logger.LogInformation("Getting Notifications");
            var notifications = await _api.GetProgressNotificationsToCheck();

            var today = DateTime.Now.Date;

            if (notifications.Notifications.Count > 0)
            {
                foreach (var notification in notifications.Notifications)
                {
                    var genNoti = new SendNotificationCommand
                    {
                        CorrelationId = Guid.NewGuid(),
                        LearnerAccountId = notification.ApprenticeshipProgress.ApprenticeAccountId,
                        Category = notification.ProgressNotification.NotificationId,
                        Heading = "Notification Header",
                        Body = "Notification Body",
                        LinkUrl = "Notification link",                                                
                    };

                    await _messageService.SendMessage(genNoti);
                    _logger.LogInformation("Got Notifications for apprentice and sent to service bus");

                    await _api.UpdateProgressNotificationStatus(notification.NotificationId, (long)notification.ApprenticeProgressId);
                    _logger.LogInformation("Updated notification status to sent for apprentice");
                }
            }
            else
            {
                _logger.LogInformation("No Notifications Found");
            }
        }
        catch (Exception ex)
        {
            string errorMsg = "SendProgressNotificationsEvent Job has failed - " + ex.Message;
            _logger.LogError(ex, errorMsg);
        }       
    }

}
