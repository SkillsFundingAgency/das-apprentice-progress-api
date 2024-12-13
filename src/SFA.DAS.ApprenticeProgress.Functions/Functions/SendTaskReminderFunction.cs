using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
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
                        string dateValue = reminder.DueDate.HasValue ? reminder.DueDate.Value.ToString("f") : "";
                        string msgTitle = "Task due " + dateValue;

                        await _messageService.SendMessage(new SendPushNotificationCommand { ApprenticeAccountIdentifier = reminder.ApprenticeAccountId, Body = reminder.Title, Title = msgTitle });
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
                string errorMsg = "SendTaskReminderEvent Job has failed - " + e.Message;
                _logger.LogError(e, errorMsg);
            }
        }
    }
}
