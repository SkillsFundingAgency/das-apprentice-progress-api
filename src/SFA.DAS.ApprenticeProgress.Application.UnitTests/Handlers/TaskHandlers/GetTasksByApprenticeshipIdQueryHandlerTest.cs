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
    public class GetTasksByApprenticeshipIdQueryHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetTasksByApprenticeshipIdQueryHandler_test_status_todo()
        {
            await PopulateDbContext();

            var getTasksByApprenticeshipIdQueryHandler = new GetTasksByApprenticeshipIdQueryHandler(DbContext);

            var command = new GetTasksByApprenticeshipIdQuery
            {
                Status = 0,
                ApprenticeshipId = new Guid("fd0daf58-af19-440d-b52f-7e1d47267d3b"),
                FromDate = new DateTime(2020, 1, 1),
                ToDate = new DateTime(2030, 1, 1)
            };

            var result = await getTasksByApprenticeshipIdQueryHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetTasksByApprenticeshipIdQueryHandler_test_status_done()
        {
            await PopulateDbContext();

            var getTasksByApprenticeshipIdQueryHandler = new GetTasksByApprenticeshipIdQueryHandler(DbContext);

            var command = new GetTasksByApprenticeshipIdQuery
            {
                Status = 1,
                ApprenticeshipId = new Guid("fd0daf58-af19-440d-b52f-7e1d47267d3b"),
                FromDate = new DateTime(2020, 1, 1),
                ToDate = new DateTime(2030, 1, 1)
            };
            var result = await getTasksByApprenticeshipIdQueryHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        private async System.Threading.Tasks.Task PopulateDbContext()
        {
            var tasks = _fixture.CreateMany<Domain.Entities.Task>().ToArray();
            tasks[0].DueDate = new DateTime(2025, 1, 1);
            tasks[0].ApprenticeshipId = new Guid("fd0daf58-af19-440d-b52f-7e1d47267d3b");
            tasks[0].Status = Task.TaskStatus.Todo;

            tasks[1].DueDate = new DateTime(2025, 1, 1);
            tasks[1].ApprenticeshipId = new Guid("fd0daf58-af19-440d-b52f-7e1d47267d3b");
            tasks[1].Status = Task.TaskStatus.Done;

            await DbContext.Task.AddRangeAsync(tasks);
            await DbContext.SaveChangesAsync();
        }
    }
}