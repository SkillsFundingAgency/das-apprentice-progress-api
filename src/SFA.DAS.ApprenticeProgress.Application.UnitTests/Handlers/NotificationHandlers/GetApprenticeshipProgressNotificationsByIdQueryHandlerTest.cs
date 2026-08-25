using System;
using System.Threading;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetApprenticeshipProgressNotifications;
using SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture;
using SFA.DAS.ApprenticeProgress.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.Handlers.NotificationHandlers
{
    public class GetApprenticeshipProgressNotificationsByIdQueryHandlerTest
        : ApprenticeProgressDbContextFixture
    {
        [Test]
        public async Task Handle_WhenApprenticeshipProgressExists_ReturnsApprenticeshipProgress()
        {
            // Arrange
            var apprenticeIdentifier = Guid.NewGuid();

            var apprenticeshipProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = apprenticeIdentifier,
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgress.AddAsync(apprenticeshipProgress);
            await DbContext.SaveChangesAsync();

            var query = new GetApprenticeshipProgressNotificationsByIdQuery
            {
                ApprenticeIdentifier = apprenticeIdentifier
            };

            var handler =
                new GetApprenticeshipProgressNotificationsByIdQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ApprenticeshipProgress, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(
                    result.ApprenticeshipProgress.Id,
                    Is.EqualTo(apprenticeshipProgress.Id));

                Assert.That(
                    result.ApprenticeshipProgress.ApprenticeAccountId,
                    Is.EqualTo(apprenticeIdentifier));

                Assert.That(
                    result.ApprenticeshipProgress.IsEnabled,
                    Is.True);
            });
        }

        [Test]
        public async Task Handle_WhenNotificationsExist_ReturnsNotificationsForApprenticeshipProgress()
        {
            // Arrange
            var apprenticeIdentifier = Guid.NewGuid();

            var apprenticeshipProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = apprenticeIdentifier,
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgress.AddAsync(apprenticeshipProgress);
            await DbContext.SaveChangesAsync();

            var notification1 = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = apprenticeshipProgress.Id,
                NotificationId = Guid.NewGuid(),
                IsEnabled = true
            };

            var notification2 = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = apprenticeshipProgress.Id,
                NotificationId = Guid.NewGuid(),
                IsEnabled = false
            };

            await DbContext.ApprenticeshipProgressNotification.AddRangeAsync(
                notification1,
                notification2);

            await DbContext.SaveChangesAsync();

            var query = new GetApprenticeshipProgressNotificationsByIdQuery
            {
                ApprenticeIdentifier = apprenticeIdentifier
            };

            var handler =
                new GetApprenticeshipProgressNotificationsByIdQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(
                result.ApprenticeshipProgressNotification,
                Has.Count.EqualTo(2));

            Assert.Multiple(() =>
            {
                Assert.That(
                    result.ApprenticeshipProgressNotification,
                    Has.Some.Matches<ApprenticeshipProgressNotification>(
                        x => x.NotificationId == notification1.NotificationId));

                Assert.That(
                    result.ApprenticeshipProgressNotification,
                    Has.Some.Matches<ApprenticeshipProgressNotification>(
                        x => x.NotificationId == notification2.NotificationId));
            });
        }

        [Test]
        public async Task Handle_WhenOtherApprenticeshipNotificationsExist_ReturnsOnlyNotificationsForRequestedApprentice()
        {
            // Arrange
            var requestedApprenticeIdentifier = Guid.NewGuid();

            var requestedProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = requestedApprenticeIdentifier,
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            var otherProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = Guid.NewGuid(),
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgress.AddRangeAsync(
                requestedProgress,
                otherProgress);

            await DbContext.SaveChangesAsync();

            var requestedNotification = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = requestedProgress.Id,
                NotificationId = Guid.NewGuid(),
                IsEnabled = true
            };

            var otherNotification = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = otherProgress.Id,
                NotificationId = Guid.NewGuid(),
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgressNotification.AddRangeAsync(
                requestedNotification,
                otherNotification);

            await DbContext.SaveChangesAsync();

            var query = new GetApprenticeshipProgressNotificationsByIdQuery
            {
                ApprenticeIdentifier = requestedApprenticeIdentifier
            };

            var handler =
                new GetApprenticeshipProgressNotificationsByIdQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(
                result.ApprenticeshipProgressNotification,
                Has.Count.EqualTo(1));

            var returnedNotification =
                result.ApprenticeshipProgressNotification[0];

            Assert.Multiple(() =>
            {
                Assert.That(
                    returnedNotification.NotificationId,
                    Is.EqualTo(requestedNotification.NotificationId));

                Assert.That(
                    returnedNotification.ApprenticeProgressId,
                    Is.EqualTo(requestedProgress.Id));
            });
        }

        [Test]
        public async Task Handle_WhenNoNotificationsExist_ReturnsEmptyNotificationList()
        {
            // Arrange
            var apprenticeIdentifier = Guid.NewGuid();

            var apprenticeshipProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = apprenticeIdentifier,
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgress.AddAsync(apprenticeshipProgress);
            await DbContext.SaveChangesAsync();

            var query = new GetApprenticeshipProgressNotificationsByIdQuery
            {
                ApprenticeIdentifier = apprenticeIdentifier
            };

            var handler =
                new GetApprenticeshipProgressNotificationsByIdQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ApprenticeshipProgress, Is.Not.Null);
            Assert.That(
                result.ApprenticeshipProgressNotification,
                Is.Empty);
        }
    }
}
