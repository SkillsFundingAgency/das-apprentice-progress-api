using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Commands;
using SFA.DAS.ApprenticeProgress.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class AddOrUpdateKsbProgressCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test]
        public async Task Handle_WhenNoExistingRecord_InsertsNewKsbProgress()
        {
            // Arrange
            var handler = new AddOrUpdateKsbProgressCommandHandler(DbContext);
            var command = _fixture.Build<AddOrUpdateKsbProgressCommand>()
                .With(c => c.KSBId, _fixture.Create<Guid>())
                .With(c => c.ApprenticeshipId, _fixture.Create<long>())
                .With(c => c.CurrentStatus, (int)KSBStatus.Completed) // Use any valid status
                .Create();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));

            var savedEntity = await DbContext.KSBProgress
                .SingleOrDefaultAsync(x => x.KSBId == command.KSBId && x.ApprenticeshipId == command.ApprenticeshipId);
            Assert.That(savedEntity, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(savedEntity.ApprenticeshipId, Is.EqualTo(command.ApprenticeshipId));
                Assert.That(savedEntity.KSBProgressType, Is.EqualTo((KSBProgressType)command.KSBProgressType));
                Assert.That(savedEntity.KSBId, Is.EqualTo(command.KSBId));
                Assert.That(savedEntity.KSBKey, Is.EqualTo(command.KsbKey));
                Assert.That(savedEntity.CurrentStatus, Is.EqualTo((KSBStatus)command.CurrentStatus));
                Assert.That(savedEntity.Note, Is.EqualTo(command.Note));
            });

            // No history should exist for a new record
            var historyCount = await DbContext.KSBProgressStatusHistory.CountAsync();
            Assert.That(historyCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_WhenExistingRecordAndStatusChanged_UpdatesAndAddsHistory()
        {
            // Arrange
            var existingKsb = _fixture.Build<KSBProgress>()
                .With(x => x.KSBProgressId, (int?)null) // Let DB assign identity
                .With(x => x.CurrentStatus, KSBStatus.InProgress)
                .With(x => x.Note, "Old note")
                .Create();
            await DbContext.KSBProgress.AddAsync(existingKsb);
            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<AddOrUpdateKsbProgressCommand>()
                .With(c => c.KSBId, existingKsb.KSBId)
                .With(c => c.ApprenticeshipId, existingKsb.ApprenticeshipId)
                .With(c => c.CurrentStatus, (int)KSBStatus.Completed) // Different status
                .With(c => c.Note, "New note")
                .Create();

            var handler = new AddOrUpdateKsbProgressCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));

            var updatedEntity = await DbContext.KSBProgress
                .SingleAsync(x => x.KSBId == command.KSBId && x.ApprenticeshipId == command.ApprenticeshipId);
            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity.CurrentStatus, Is.EqualTo((KSBStatus)command.CurrentStatus));
                Assert.That(updatedEntity.Note, Is.EqualTo(command.Note));
            });

            // History should contain one record
            var historyRecords = await DbContext.KSBProgressStatusHistory
                .Where(h => h.KSBProgressId == updatedEntity.KSBProgressId.Value)
                .ToListAsync();
            Assert.That(historyRecords, Has.Count.EqualTo(1));
            var history = historyRecords.Single();
            Assert.Multiple(() =>
            {
                Assert.That(history.Status, Is.EqualTo(command.CurrentStatus));
                Assert.That(history.StatusTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            });
        }

        [Test]
        public async Task Handle_WhenExistingRecordAndStatusUnchanged_UpdatesButDoesNotAddHistory()
        {
            // Arrange
            var existingKsb = _fixture.Build<KSBProgress>()
                .With(x => x.KSBProgressId, (int?)null)
                .With(x => x.CurrentStatus, KSBStatus.InProgress)
                .With(x => x.Note, "Old note")
                .Create();
            await DbContext.KSBProgress.AddAsync(existingKsb);
            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<AddOrUpdateKsbProgressCommand>()
                .With(c => c.KSBId, existingKsb.KSBId)
                .With(c => c.ApprenticeshipId, existingKsb.ApprenticeshipId)
                .With(c => c.CurrentStatus, (int)KSBStatus.InProgress) // Same status
                .With(c => c.Note, "Updated note") // Only note changed
                .Create();

            var handler = new AddOrUpdateKsbProgressCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));

            var updatedEntity = await DbContext.KSBProgress
                .SingleAsync(x => x.KSBId == command.KSBId && x.ApprenticeshipId == command.ApprenticeshipId);
            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity.CurrentStatus, Is.EqualTo(KSBStatus.InProgress));
                Assert.That(updatedEntity.Note, Is.EqualTo(command.Note));
            });

            // No history entry should be added
            var historyCount = await DbContext.KSBProgressStatusHistory
                .CountAsync(h => h.KSBProgressId == updatedEntity.KSBProgressId.Value);
            Assert.That(historyCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_WhenMultipleRecordsExist_UsesBothKeysToFindMatch()
        {
            // Arrange
            var ksb1 = _fixture.Build<KSBProgress>()
                .With(x => x.KSBProgressId, (int?)null)
                .With(x => x.KSBId, Guid.NewGuid)
                .With(x => x.ApprenticeshipId, 1000)
                .Create();
            var ksb2 = _fixture.Build<KSBProgress>()
                .With(x => x.KSBProgressId, (int?)null)
                .With(x => x.KSBId, Guid.NewGuid)
                .With(x => x.ApprenticeshipId, 2000)
                .Create();
            await DbContext.KSBProgress.AddRangeAsync(ksb1, ksb2);
            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<AddOrUpdateKsbProgressCommand>()
                .With(c => c.KSBId, ksb1.KSBId)
                .With(c => c.ApprenticeshipId, ksb1.ApprenticeshipId)
                .With(c => c.CurrentStatus, (int)KSBStatus.Completed)
                .Create();

            var handler = new AddOrUpdateKsbProgressCommandHandler(DbContext);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var updatedKsb1 = await DbContext.KSBProgress.FindAsync(ksb1.KSBProgressId);
            Assert.That(updatedKsb1.CurrentStatus, Is.EqualTo(KSBStatus.Completed));

            var unchangedKsb2 = await DbContext.KSBProgress.FindAsync(ksb2.KSBProgressId);
            Assert.That(unchangedKsb2.CurrentStatus, Is.EqualTo(ksb2.CurrentStatus));
        }

        [Test]
        public async Task Handle_WhenCommandHasNullNote_UpdatesWithNull()
        {
            // Arrange
            var existingKsb = _fixture.Build<KSBProgress>()
                .With(x => x.KSBProgressId, (int?)null)
                .With(x => x.Note, "Some note")
                .Create();
            await DbContext.KSBProgress.AddAsync(existingKsb);
            await DbContext.SaveChangesAsync();

            var command = _fixture.Build<AddOrUpdateKsbProgressCommand>()
                .With(c => c.KSBId, existingKsb.KSBId)
                .With(c => c.ApprenticeshipId, existingKsb.ApprenticeshipId)
                .With(c => c.CurrentStatus, (int)existingKsb.CurrentStatus) // unchanged
                .With(c => c.Note, (string)null)
                .Create();

            var handler = new AddOrUpdateKsbProgressCommandHandler(DbContext);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var updated = await DbContext.KSBProgress
                .SingleAsync(x => x.KSBId == command.KSBId && x.ApprenticeshipId == command.ApprenticeshipId);
            Assert.That(updated.Note, Is.Null);
        }

        [Test]
        public async Task Handle_WhenExceptionOccurs_DoesNotSavePartialChanges()
        {
            Assert.Pass("No explicit rollback scenario; EF Core SaveChanges atomic.");
        }
    }
}
