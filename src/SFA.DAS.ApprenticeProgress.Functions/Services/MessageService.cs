using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NServiceBus;
using SFA.DAS.PushNotifications.Messages.Commands;

namespace SFA.DAS.ApprenticeProgress.Functions.Services;

public interface IMessageService
{
    Task SendMessage(SendPushNotificationCommand message);
}

public class MessageService : IMessageService
{
    private readonly IMessageSession _messageSession;
    private readonly ILogger<MessageService> _logger;

    public MessageService(
        IMessageSession messageSession,
        ILogger<MessageService> logger
        )
    {
        _messageSession = messageSession;
        _logger = logger;
    }

    public async Task SendMessage(SendPushNotificationCommand message)
    {
        try
        {
            await _messageSession.Send(message);
        }
        catch (Exception ex)
        {
            string errorMessage = "Failed to send message: " + ex.Message;
            _logger.LogError(ex, errorMessage);
        }
    }
}
