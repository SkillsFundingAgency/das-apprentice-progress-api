using System;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.PushNotifications.Messages.Commands
{
    [ExcludeFromCodeCoverage]
    public class SendPushNotificationCommand
    {
        public Guid ApprenticeAccountIdentifier { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}
