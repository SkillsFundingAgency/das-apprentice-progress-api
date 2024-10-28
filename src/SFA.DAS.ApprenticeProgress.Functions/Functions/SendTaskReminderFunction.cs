using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeProgress.Functions.Api.Clients;
using SFA.DAS.ApprenticeProgress.Functions.Services;
using SFA.DAS.PushNotifications.Messages.Commands;

namespace SFA.DAS.ApprenticeProgress.Functions
{
    [ExcludeFromCodeCoverage]
    public class SendTaskReminderFunction
    {
        private readonly IApprenticeProgressApiClient _api;
        private readonly IMessageService _messageService;
        private readonly ILogger<SendTaskReminderFunction> _logger;

        public SendTaskReminderFunction(
            IApprenticeProgressApiClient api,
            IMessageService messageService,
            ILogger<SendTaskReminderFunction> logger
            )
        {
            _api = api;
            _messageService = messageService;
            _logger = logger;
        }

        [Function("SendTaskReminderEvent")]
        public async Task Run([TimerTrigger("0 */1 * * * *", RunOnStartup = true)] TimerInfo timer, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting Reminders");

                var taskReminders = await _api.GetTaskReminders();

                if (taskReminders.TaskReminders.Count > 0)
                {
                    foreach (var reminder in taskReminders.TaskReminders)
                    {
                        await _messageService.SendMessage(new ProcessMessageCommand { ApprenticeAccountIdentifier = reminder.ApprenticeshipId , Body = "the message body", Title = "the notification title" });
                        _logger.LogInformation("Got reminder and sent to service bus");

                        await _api.UpdateTaskReminders(reminder.TaskId.Value, 1);
                        _logger.LogInformation("Updated reminder status via API");
                    }
                }
                else
                {
                    _logger.LogInformation("No reminders found");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "SendTaskReminderEvent Job has failed");
            }
        }
    }
}