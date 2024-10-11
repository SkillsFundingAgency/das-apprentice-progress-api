using SFA.DAS.PushNotifications.Messages.Commands;

namespace SFA.DAS.ApprenticeProgress.Functions.Services
{
    public interface IMessageService
    {
        Task SendMessage(ProcessMessageCommand message);
    }
}