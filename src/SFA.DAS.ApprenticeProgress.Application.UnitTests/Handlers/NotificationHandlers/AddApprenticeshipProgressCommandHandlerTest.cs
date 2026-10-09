using System;
using System.Linq;
using System.Threading;
using AutoFixture;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Commands.AddApprenticeshipProgress;
using SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture;
using SFA.DAS.ApprenticeProgress.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.Handlers.NotificationHandlers
{
    public class AddApprenticeshipProgressCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test]
        public async Task Handle_CreatesApprenticeshipProgress()
        {
            // Arrange
            var apprenticeIdentifier = Guid.NewGuid();

            var command = _fixture.Build<AddApprenticeshipProgressCommand>()
                .With(x => x.ApprenticeIdentifier, apprenticeIdentifier)
                .Create();

            var handler = new AddApprenticeshipProgressCommandHandler(DbContext);

            var beforeHandle = DateTime.Now;

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            var afterHandle = DateTime.Now;

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));

            var savedProgress = await DbContext.ApprenticeshipProgress
                .SingleOrDefaultAsync(x =>
                    x.ApprenticeAccountId == apprenticeIdentifier);

            Assert.That(savedProgress, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(
                    savedProgress.ApprenticeAccountId,
                    Is.EqualTo(apprenticeIdentifier));

                Assert.That(
                    savedProgress.IsEnabled,
                    Is.True);

                Assert.That(
                    savedProgress.FirstLoggedIn,
                    Is.InRange(beforeHandle, afterHandle));
            });
        }

        [Test]
        public async Task Handle_WhenEnabledNotificationsExist_CreatesProgressNotifications()
        {
            // Arrange
            var notification1 = _fixture.Build<ProgressNotification>()
                .With(x => x.Id, (Guid?)null)
                .With(x => x.IsEnabled, true)
                .Create();

            var notification2 = _fixture.Build<ProgressNotification>()
                .With(x => x.Id, (Guid?)null)
                .With(x => x.IsEnabled, true)
                .Create();

            await DbContext.ProgressNotification.AddRangeAsync(
                notification1,
                notification2);

            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<AddApprenticeshipProgressCommand>()
                .With(x => x.ApprenticeIdentifier, Guid.NewGuid())
                .Create();

            var handler = new AddApprenticeshipProgressCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));

            var savedProgress = await DbContext.ApprenticeshipProgress
                .SingleAsync(x =>
                    x.ApprenticeAccountId == command.ApprenticeIdentifier);

            var progressNotifications =
                await DbContext.ApprenticeshipProgressNotification
                    .Where(x => x.ApprenticeProgressId == savedProgress.Id)
                    .ToListAsync();

            Assert.That(progressNotifications, Has.Count.EqualTo(2));

            Assert.Multiple(() =>
            {
                Assert.That(
                    progressNotifications.Any(x =>
                        x.NotificationId == notification1.Id),
                    Is.True);

                Assert.That(
                    progressNotifications.Any(x =>
                        x.NotificationId == notification2.Id),
                    Is.True);

                Assert.That(
                    progressNotifications.All(x => x.IsEnabled),
                    Is.True);
            });
        }

        [Test]
        public async Task Handle_WhenDisabledNotificationExists_DoesNotCreateProgressNotification()
        {
            // Arrange
            var enabledNotification = _fixture.Build<ProgressNotification>()
                .With(x => x.Id, (Guid?)null)
                .With(x => x.IsEnabled, true)
                .Create();

            var disabledNotification = _fixture.Build<ProgressNotification>()
                .With(x => x.Id, (Guid?)null)
                .With(x => x.IsEnabled, false)
                .Create();

            await DbContext.ProgressNotification.AddRangeAsync(
                enabledNotification,
                disabledNotification);

            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<AddApprenticeshipProgressCommand>()
                .With(x => x.ApprenticeIdentifier, Guid.NewGuid())
                .Create();

            var handler = new AddApprenticeshipProgressCommandHandler(DbContext);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var savedProgress = await DbContext.ApprenticeshipProgress
                .SingleAsync(x =>
                    x.ApprenticeAccountId == command.ApprenticeIdentifier);

            var progressNotifications =
                await DbContext.ApprenticeshipProgressNotification
                    .Where(x => x.ApprenticeProgressId == savedProgress.Id)
                    .ToListAsync();

            Assert.That(progressNotifications, Has.Count.EqualTo(1));

            var progressNotification = progressNotifications.Single();

            Assert.Multiple(() =>
            {
                Assert.That(
                    progressNotification.NotificationId,
                    Is.EqualTo(enabledNotification.Id));

                Assert.That(
                    progressNotification.NotificationId,
                    Is.Not.EqualTo(disabledNotification.Id));

                Assert.That(
                    progressNotification.IsEnabled,
                    Is.True);
            });
        }

        [Test]
        public async Task Handle_WhenNoEnabledNotificationsExist_CreatesProgressWithoutNotifications()
        {
            // Arrange
            var disabledNotification = _fixture.Build<ProgressNotification>()
                .With(x => x.Id, (Guid?)null)
                .With(x => x.IsEnabled, false)
                .Create();

            await DbContext.ProgressNotification.AddAsync(disabledNotification);
            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<AddApprenticeshipProgressCommand>()
                .With(x => x.ApprenticeIdentifier, Guid.NewGuid())
                .Create();

            var handler = new AddApprenticeshipProgressCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));

            var savedProgress = await DbContext.ApprenticeshipProgress
                .SingleOrDefaultAsync(x =>
                    x.ApprenticeAccountId == command.ApprenticeIdentifier);

            Assert.That(savedProgress, Is.Not.Null);

            var progressNotifications =
                await DbContext.ApprenticeshipProgressNotification
                    .Where(x => x.ApprenticeProgressId == savedProgress.Id)
                    .ToListAsync();

            Assert.That(progressNotifications, Is.Empty);
        }
    }
}
