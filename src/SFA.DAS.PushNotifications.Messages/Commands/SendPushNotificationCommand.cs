using System.Diagnostics.CodeAnalysis;
using NServiceBus;

namespace SFA.DAS.PushNotifications.Messages.Commands
{
    [ExcludeFromCodeCoverage]
    public class SendPushNotificationCommand : ICommand
    {
        public long? ApprenticeAccountIdentifier { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}