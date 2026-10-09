using System;
using System.Threading;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetProgressNotificationsById;
using SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture;
using SFA.DAS.ApprenticeProgress.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.Handlers.NotificationHandlers
{
    public class GetProgressNotificationsByIdQueryHandlerTest
        : ApprenticeProgressDbContextFixture
    {
        [Test]
        public async Task Handle_WhenNotificationExists_ReturnsNotification()
        {
            // Arrange
            var notification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            await DbContext.ProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var query = new GetProgressNotificationsByIdQuery
            {
                NotificationId = notification.Id
            };

            var handler = new GetProgressNotificationsByIdQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ProgressNotification, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(
                    result.ProgressNotification.Id,
                    Is.EqualTo(notification.Id));

                Assert.That(
                    result.ProgressNotification.IsEnabled,
                    Is.EqualTo(notification.IsEnabled));
            });
        }

        [Test]
        public async Task Handle_WhenMultipleNotificationsExist_ReturnsRequestedNotification()
        {
            // Arrange
            var requestedNotification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            var otherNotification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            await DbContext.ProgressNotification.AddRangeAsync(
                requestedNotification,
                otherNotification);

            await DbContext.SaveChangesAsync();

            var query = new GetProgressNotificationsByIdQuery
            {
                NotificationId = requestedNotification.Id
            };

            var handler = new GetProgressNotificationsByIdQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ProgressNotification, Is.Not.Null);

            Assert.That(
                result.ProgressNotification.Id,
                Is.EqualTo(requestedNotification.Id));
        }

        [Test]
        public async Task Handle_WhenNotificationDoesNotExist_ReturnsNullNotification()
        {
            // Arrange
            var notification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            await DbContext.ProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var query = new GetProgressNotificationsByIdQuery
            {
                NotificationId = Guid.NewGuid()
            };

            var handler = new GetProgressNotificationsByIdQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ProgressNotification, Is.Null);
        }

        [Test]
        public async Task Handle_WhenNotificationIsDisabled_StillReturnsNotification()
        {
            // Arrange
            var notification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = false
            };

            await DbContext.ProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var query = new GetProgressNotificationsByIdQuery
            {
                NotificationId = notification.Id
            };

            var handler = new GetProgressNotificationsByIdQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ProgressNotification, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(
                    result.ProgressNotification.Id,
                    Is.EqualTo(notification.Id));

                Assert.That(
                    result.ProgressNotification.IsEnabled,
                    Is.False);
            });
        }
    }
}
