using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeProgress.Functions.Api;
using SFA.DAS.ApprenticeProgress.Functions.Services;
using SFA.DAS.PushNotifications.Messages.Commands;

namespace SFA.DAS.ApprenticeProgress.Functions
{
    [ExcludeFromCodeCoverage]
    public class SendTaskReminderFunction
    {
        private readonly IApprenticeProgressApi _api;
        private readonly IMessageService _messageService;

        public SendTaskReminderFunction(
            IApprenticeProgressApi api,
            IMessageService messageService
            )
        {
            _api = api;
            _messageService = messageService;
        }

        [FunctionName("SendTaskReminderEvent")]
        public async Task Run(
            [TimerTrigger("0 */1 * * * *", RunOnStartup = true)] TimerInfo timer,
            ILogger log)
        {
            try
            {
                log.LogInformation("Getting Reminders");

                var taskReminders = await _api.GetTaskReminders();

                if (taskReminders.TaskReminders.Count > 0)
                {
                    foreach (var reminder in taskReminders.TaskReminders)
                    {
                        await _messageService.SendMessage(new ProcessMessageCommand { NotificationBody = "the message body", NotificationTitle = "the notification title" });
                        log.LogInformation("Got reminder and sent to service bus");

                        await _api.UpdateTaskReminders(reminder.TaskId, 1);
                        log.LogInformation("Updated reminder status via API");
                    }
                }
                else
                {
                    log.LogInformation("No reminders found");
                }
            }
            catch (Exception e)
            {
                log.LogError(e, "SendTaskReminderEvent Job has failed");
            }
        }
    }
}