using System;
using System.Linq;
using System.Threading;
using AutoFixture;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Tasks.Queries;
using SFA.DAS.ApprenticeProgress.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class GetTaskRemindersByApprenticeshipIdHandlerTests : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetTaskRemindersByApprenticeshipIdQueryHandler_test()
        {
            await PopulateDbContext();

            var getTaskRemindersByApprenticeshipIdQueryHandler = new GetTaskRemindersByApprenticeshipIdHandler(DbContext);

            var request = new GetTaskRemindersByApprenticeshipIdQuery
            {
                ApprenticeshipId = 1
            };

            var result = await getTaskRemindersByApprenticeshipIdQueryHandler.Handle(request, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetTaskRemindersByApprenticeshipIdQueryHandler_Validate_Reminder()
        {
            await PopulateDbContext();

            var getTaskRemindersByApprenticeshipIdQueryHandler = new GetTaskRemindersByApprenticeshipIdHandler(DbContext);

            var request = new GetTaskRemindersByApprenticeshipIdQuery
            {
                ApprenticeshipId = 1
            };

            var result = await getTaskRemindersByApprenticeshipIdQueryHandler.Handle(request, CancellationToken.None);
            Assert.That(result.TaskReminders.FirstOrDefault().TaskId, Is.EqualTo(1));
            Assert.That(result.TaskReminders.FirstOrDefault().ApprenticeshipId, Is.EqualTo(1));
            Assert.That(result.TaskReminders.FirstOrDefault().ApprenticeAccountId, Is.Not.Null);
            Assert.That(result.TaskReminders.FirstOrDefault().DueDate, Is.EqualTo(new DateTime(2025, 1, 1)));
            Assert.That(result.TaskReminders.FirstOrDefault().Title, Is.Not.Null);
            Assert.That(result.TaskReminders.FirstOrDefault().ReminderValue, Is.EqualTo(5));
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
            taskReminders[0].Status = ReminderStatus.Sent;
            taskReminders[0].TaskId = 1;
            taskReminders[0].ReminderValue = 5;
            taskReminders[1].Status = ReminderStatus.NotSent;

            await DbContext.TaskReminder.AddRangeAsync(taskReminders);
            await DbContext.SaveChangesAsync();
        }
    }
}
