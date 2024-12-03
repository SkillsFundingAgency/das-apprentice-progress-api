using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using NServiceBus;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Functions.Services;
using SFA.DAS.PushNotifications.Messages.Commands;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Functions.UnitTests
{
    public class MessageServiceTests
    {
        [Test, MoqAutoData]
        public async Task Send_message(
            [Frozen] Mock<IMessageSession> mockMessageSession,
            MessageService service)
        {
            SendPushNotificationCommand message = new() { Body = "body", Title = "title" };

            await service.SendMessage(message);

            mockMessageSession.Verify(a => a.Send(message, It.IsAny<SendOptions>()));
        }

        [Test, MoqAutoData]
        public async Task Send_message_exception_is_logged(
           [Frozen] Mock<ILogger<MessageService>> mockLogger,
           [Frozen] Mock<IMessageSession> mockMessageSession,
           MessageService service)
        {
            SendPushNotificationCommand message = new() { Body = "body", Title = "title" };

            mockMessageSession.Setup(a => a.Send(message, It.IsAny<SendOptions>()))
                .ThrowsAsync(new System.Exception("Test"));
            await service.SendMessage(message);

            mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to send message: Test")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }
    }
}
