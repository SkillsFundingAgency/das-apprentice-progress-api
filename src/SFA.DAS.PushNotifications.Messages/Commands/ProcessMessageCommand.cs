using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using NServiceBus;

namespace SFA.DAS.PushNotifications.Messages.Commands
{
    [ExcludeFromCodeCoverage]
    public class ProcessMessageCommand : IMessage
    {
        public long? ApprenticeAccountIdentifier { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}
