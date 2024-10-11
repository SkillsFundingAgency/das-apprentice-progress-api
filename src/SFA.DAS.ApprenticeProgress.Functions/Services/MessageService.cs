using NServiceBus;
using SFA.DAS.PushNotifications.Messages.Commands;

namespace SFA.DAS.ApprenticeProgress.Functions.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageSession _messageSession;

        public MessageService(
            IMessageSession messageSession
            )
        {
            _messageSession = messageSession;
        }

        public async Task SendMessage(ProcessMessageCommand message)
        {
            try
            {
                await _messageSession.Send(message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}