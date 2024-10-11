using System;
using System.Diagnostics.CodeAnalysis;
using NServiceBus;

namespace SFA.DAS.PushNotifications.Messages.Commands
{
    [ExcludeFromCodeCoverage]
    public class ProcessMessageCommand : IMessage
    {
        public string NotificationTitle { get; set; } = null!;
        public string NotificationBody { get; set; } = null!;
    }
}
