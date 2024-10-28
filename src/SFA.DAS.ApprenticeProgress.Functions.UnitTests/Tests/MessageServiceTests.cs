using System.Threading.Tasks;
using AutoFixture.NUnit3;
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
            ProcessMessageCommand message = new() { Body = "body", Title = "title" };

            await service.SendMessage(message);

            mockMessageSession.Verify(a => a.Send(message, It.IsAny<SendOptions>()));
        }
    }
}