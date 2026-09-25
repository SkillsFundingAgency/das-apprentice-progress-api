using System;
using System.Threading;
using AutoFixture;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Commands.UpdateProgressNotificationStatus;
using SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture;
using SFA.DAS.ApprenticeProgress.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.Handlers.NotificationHandlers
{
    public class UpdateProgressNotificationStatusCommandHandlerTest
        : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test]
        public async Task Handle_WhenNotificationExists_DisablesNotification()
        {
            // Arrange
            var notification = _fixture.Build<ApprenticeshipProgressNotification>()
                .With(x => x.IsEnabled, true)
                .Create();

            await DbContext.ApprenticeshipProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<UpdateProgressNotificationStatusCommand>()
                .With(x => x.NotificationId, notification.NotificationId)
                .With(x => x.ApprenticeProgressId, notification.ApprenticeProgressId)
                .Create();

            var handler = new UpdateProgressNotificationStatusCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));

            var updatedNotification = await DbContext.ApprenticeshipProgressNotification
                .SingleAsync(x =>
                    x.NotificationId == command.NotificationId &&
                    x.ApprenticeProgressId == command.ApprenticeProgressId);

            Assert.That(updatedNotification.IsEnabled, Is.False);
        }

        [Test]
        public async Task Handle_WhenNotificationDoesNotExist_ReturnsUnitValue()
        {
            // Arrange
            var command = _fixture.Build<UpdateProgressNotificationStatusCommand>()
                .With(x => x.NotificationId, Guid.NewGuid())
                .With(x => x.ApprenticeProgressId, 999999)
                .Create();

            var handler = new UpdateProgressNotificationStatusCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));
        }

        [Test]
        public async Task Handle_WhenNotificationIdDoesNotMatch_DoesNotDisableNotification()
        {
            // Arrange
            var notification = _fixture.Build<ApprenticeshipProgressNotification>()
                .With(x => x.IsEnabled, true)
                .Create();

            await DbContext.ApprenticeshipProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<UpdateProgressNotificationStatusCommand>()
                .With(x => x.NotificationId, Guid.NewGuid())
                .With(x => x.ApprenticeProgressId, notification.ApprenticeProgressId)
                .Create();

            var handler = new UpdateProgressNotificationStatusCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));

            var unchangedNotification = await DbContext.ApprenticeshipProgressNotification
                .SingleAsync(x =>
                    x.NotificationId == notification.NotificationId &&
                    x.ApprenticeProgressId == notification.ApprenticeProgressId);

            Assert.That(unchangedNotification.IsEnabled, Is.True);
        }

        [Test]
        public async Task Handle_WhenApprenticeProgressIdDoesNotMatch_DoesNotDisableNotification()
        {
            // Arrange
            var notification = _fixture.Build<ApprenticeshipProgressNotification>()
                .With(x => x.IsEnabled, true)
                .Create();

            await DbContext.ApprenticeshipProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<UpdateProgressNotificationStatusCommand>()
                .With(x => x.NotificationId, notification.NotificationId)
                .With(x => x.ApprenticeProgressId, notification.ApprenticeProgressId + 1)
                .Create();

            var handler = new UpdateProgressNotificationStatusCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));

            var unchangedNotification = await DbContext.ApprenticeshipProgressNotification
                .SingleAsync(x =>
                    x.NotificationId == notification.NotificationId &&
                    x.ApprenticeProgressId == notification.ApprenticeProgressId);

            Assert.That(unchangedNotification.IsEnabled, Is.True);
        }
    }
}
