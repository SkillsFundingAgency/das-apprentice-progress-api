using System;
using System.Linq;
using System.Threading;
using AutoFixture;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Commands;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class UpdateTaskByApprenticeshipIdAndTaskIdCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task UpdateTaskByApprenticeshipIdAndTaskIdCommandHandler_test_with_task()
        {
            await PopulateDbContext();

            var addTaskByApprenticeshipIdCommandHandler = new UpdateTaskByApprenticeshipIdAndTaskIdCommandHandler(DbContext);

            var command = new UpdateTaskByApprenticeshipIdAndTaskIdCommand()
            {
                TaskId = 1,
                ApprenticeshipId = 1,
                DueDate = new DateTime(2019, 05, 09),
                Title = "title",
                ApprenticeshipCategoryId = 0,
                Note = "note",
                CompletionDateTime = new DateTime(2019, 05, 09),
                CreatedDateTime = new DateTime(2019, 05, 09),
                Status = 1,
                ReminderStatus = 1,
                ReminderUnit = 1,
                ReminderValue = 1
            };

            var result = await addTaskByApprenticeshipIdCommandHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task UpdateTaskByApprenticeshipIdAndTaskIdCommandHandler_test_no_task()
        {
            await PopulateDbContext();

            var addTaskByApprenticeshipIdCommandHandler = new UpdateTaskByApprenticeshipIdAndTaskIdCommandHandler(DbContext);

            var command = new UpdateTaskByApprenticeshipIdAndTaskIdCommand()
            {
                TaskId = 0,
            };

            var result = await addTaskByApprenticeshipIdCommandHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        private async System.Threading.Tasks.Task PopulateDbContext()
        {
            var tasks = _fixture.CreateMany<Domain.Entities.Task>().ToArray();
            await DbContext.Task.AddRangeAsync(tasks);
            await DbContext.SaveChangesAsync();
        }
    }
}