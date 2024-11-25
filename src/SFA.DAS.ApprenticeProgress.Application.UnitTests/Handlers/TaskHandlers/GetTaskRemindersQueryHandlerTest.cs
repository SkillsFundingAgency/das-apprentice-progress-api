using System;
using System.Linq;
using System.Threading;
using AutoFixture;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class GetTaskRemindersQueryHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetTaskByApprenticeshipIdAndTaskIdQueryHandler_test()
        {
            await PopulateDbContext();

            var getTaskByApprenticeshipIdAndTaskIdQueryHandler = new GetTaskRemindersQueryHandler(DbContext);

            var command = new GetTaskRemindersQuery
            {
                
            };

            var result = await getTaskByApprenticeshipIdAndTaskIdQueryHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        private async System.Threading.Tasks.Task PopulateDbContext()
        {
            var tasks = _fixture.CreateMany<Domain.Entities.Task>().ToArray();

            tasks[0].DueDate = new DateTime(2025, 1, 1);
            tasks[0].ApprenticeshipId = 1;
            tasks[0].Status = Task.TaskStatus.Todo;
            tasks[0].TaskId = 1;

            await DbContext.Task.AddRangeAsync(tasks);
            await DbContext.SaveChangesAsync();


            var taskReminders = _fixture.CreateMany<Domain.Entities.TaskReminder>().ToArray();
            taskReminders[0].Status = ReminderStatus.NotSent;
            taskReminders[0].TaskId = 1;
            taskReminders[1].Status = ReminderStatus.Sent;

            await DbContext.TaskReminder.AddRangeAsync(taskReminders);
            await DbContext.SaveChangesAsync();

        }
    }
}