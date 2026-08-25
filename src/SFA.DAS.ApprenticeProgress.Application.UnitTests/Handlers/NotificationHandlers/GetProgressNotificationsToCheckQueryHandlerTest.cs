using System;
using System.Threading;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetProgressNotificationsToCheck;
using SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture;
using SFA.DAS.ApprenticeProgress.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.Handlers.NotificationHandlers
{
    public class GetProgressNotificationsToCheckQueryHandlerTest
        : ApprenticeProgressDbContextFixture
    {
        [Test]
        public async Task Handle_WhenEnabledNotificationExists_ReturnsNotification()
        {
            // Arrange
            var apprenticeshipProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = Guid.NewGuid(),
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            var progressNotification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgress.AddAsync(apprenticeshipProgress);
            await DbContext.ProgressNotification.AddAsync(progressNotification);
            await DbContext.SaveChangesAsync();

            var notification = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = apprenticeshipProgress.Id,
                NotificationId = progressNotification.Id,
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var query = new GetProgressNotificationsToCheckQuery();

            var handler =
                new GetProgressNotificationsToCheckQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Notifications, Has.Count.EqualTo(1));

            var returnedNotification = result.Notifications[0];

            Assert.Multiple(() =>
            {
                Assert.That(
                    returnedNotification.ApprenticeProgressId,
                    Is.EqualTo(apprenticeshipProgress.Id));

                Assert.That(
                    returnedNotification.NotificationId,
                    Is.EqualTo(progressNotification.Id));

                Assert.That(
                    returnedNotification.IsEnabled,
                    Is.True);
            });
        }

        [Test]
        public async Task Handle_WhenEnabledAndDisabledNotificationsExist_ReturnsOnlyEnabledNotifications()
        {
            // Arrange
            var apprenticeshipProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = Guid.NewGuid(),
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            var enabledProgressNotification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            var disabledProgressNotification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgress.AddAsync(apprenticeshipProgress);

            await DbContext.ProgressNotification.AddRangeAsync(
                enabledProgressNotification,
                disabledProgressNotification);

            await DbContext.SaveChangesAsync();

            var enabledNotification = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = apprenticeshipProgress.Id,
                NotificationId = enabledProgressNotification.Id,
                IsEnabled = true
            };

            var disabledNotification = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = apprenticeshipProgress.Id,
                NotificationId = disabledProgressNotification.Id,
                IsEnabled = false
            };

            await DbContext.ApprenticeshipProgressNotification.AddRangeAsync(
                enabledNotification,
                disabledNotification);

            await DbContext.SaveChangesAsync();

            var query = new GetProgressNotificationsToCheckQuery();

            var handler =
                new GetProgressNotificationsToCheckQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Notifications, Has.Count.EqualTo(1));

            var returnedNotification = result.Notifications[0];

            Assert.Multiple(() =>
            {
                Assert.That(
                    returnedNotification.NotificationId,
                    Is.EqualTo(enabledProgressNotification.Id));

                Assert.That(
                    returnedNotification.IsEnabled,
                    Is.True);
            });
        }

        [Test]
        public async Task Handle_WhenNotificationExists_IncludesApprenticeshipProgress()
        {
            // Arrange
            var apprenticeshipProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = Guid.NewGuid(),
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            var progressNotification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgress.AddAsync(apprenticeshipProgress);
            await DbContext.ProgressNotification.AddAsync(progressNotification);
            await DbContext.SaveChangesAsync();

            var notification = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = apprenticeshipProgress.Id,
                NotificationId = progressNotification.Id,
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var handler =
                new GetProgressNotificationsToCheckQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(
                new GetProgressNotificationsToCheckQuery(),
                CancellationToken.None);

            // Assert
            var returnedNotification = result.Notifications[0];

            Assert.That(
                returnedNotification.ApprenticeshipProgress,
                Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(
                    returnedNotification.ApprenticeshipProgress.Id,
                    Is.EqualTo(apprenticeshipProgress.Id));

                Assert.That(
                    returnedNotification.ApprenticeshipProgress.ApprenticeAccountId,
                    Is.EqualTo(apprenticeshipProgress.ApprenticeAccountId));
            });
        }

        [Test]
        public async Task Handle_WhenNotificationExists_IncludesProgressNotification()
        {
            // Arrange
            var apprenticeshipProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = Guid.NewGuid(),
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            var progressNotification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgress.AddAsync(apprenticeshipProgress);
            await DbContext.ProgressNotification.AddAsync(progressNotification);
            await DbContext.SaveChangesAsync();

            var notification = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = apprenticeshipProgress.Id,
                NotificationId = progressNotification.Id,
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var handler =
                new GetProgressNotificationsToCheckQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(
                new GetProgressNotificationsToCheckQuery(),
                CancellationToken.None);

            // Assert
            var returnedNotification = result.Notifications[0];

            Assert.That(
                returnedNotification.ProgressNotification,
                Is.Not.Null);

            Assert.That(
                returnedNotification.ProgressNotification.Id,
                Is.EqualTo(progressNotification.Id));
        }

        [Test]
        public async Task Handle_WhenNoEnabledNotificationsExist_ReturnsEmptyList()
        {
            // Arrange
            var apprenticeshipProgress = new ApprenticeshipProgress
            {
                ApprenticeAccountId = Guid.NewGuid(),
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true
            };

            var progressNotification = new ProgressNotification
            {
                Id = Guid.NewGuid(),
                IsEnabled = true
            };

            await DbContext.ApprenticeshipProgress.AddAsync(apprenticeshipProgress);
            await DbContext.ProgressNotification.AddAsync(progressNotification);
            await DbContext.SaveChangesAsync();

            var notification = new ApprenticeshipProgressNotification
            {
                ApprenticeProgressId = apprenticeshipProgress.Id,
                NotificationId = progressNotification.Id,
                IsEnabled = false
            };

            await DbContext.ApprenticeshipProgressNotification.AddAsync(notification);
            await DbContext.SaveChangesAsync();

            var handler =
                new GetProgressNotificationsToCheckQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(
                new GetProgressNotificationsToCheckQuery(),
                CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Notifications, Is.Empty);
        }
    }
}
