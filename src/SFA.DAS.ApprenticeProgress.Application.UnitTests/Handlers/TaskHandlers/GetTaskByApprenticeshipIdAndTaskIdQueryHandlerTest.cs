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
    public class GetTaskByApprenticeshipIdAndTaskIdQueryHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetTaskByApprenticeshipIdAndTaskIdQueryHandler_test()
        {
            await PopulateDbContext();

            var getTaskByApprenticeshipIdAndTaskIdQueryHandler = new GetTaskByApprenticeshipIdAndTaskIdQueryHandler(DbContext);

            var command = new GetTaskByApprenticeshipIdAndTaskIdQuery
            {
                TaskId = 1,
                ApprenticeshipId = new Guid("fd0daf58-af19-440d-b52f-7e1d47267d3b")
            };

            var result = await getTaskByApprenticeshipIdAndTaskIdQueryHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        private async System.Threading.Tasks.Task PopulateDbContext()
        {
            var tasks = _fixture.CreateMany<Domain.Entities.Task>().ToArray();

            tasks[0].DueDate = new DateTime(2025, 1, 1);
            tasks[0].ApprenticeshipId = new Guid("fd0daf58-af19-440d-b52f-7e1d47267d3b");
            tasks[0].Status = Task.TaskStatus.Todo;
            tasks[0].TaskId = 1;

            await DbContext.Task.AddRangeAsync(tasks);
            await DbContext.SaveChangesAsync();
        }
    }
}