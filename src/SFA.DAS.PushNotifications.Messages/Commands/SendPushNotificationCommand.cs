using System;
using System.Diagnostics.CodeAnalysis;
using NServiceBus;

namespace SFA.DAS.PushNotifications.Messages.Commands
{
    [ExcludeFromCodeCoverage]
    public class SendPushNotificationCommand : IMessage
    {
        public Guid ApprenticeAccountIdentifier { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}
