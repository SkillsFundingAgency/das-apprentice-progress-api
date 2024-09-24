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
    public class UpdateTaskStatusCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task UpdateTaskStatusCommandHandler_test()
        {
            await PopulateDbContext();

            var addTaskByApprenticeshipIdCommandHandler = new UpdateTaskStatusCommandHandler(DbContext);

            var command = new UpdateTaskStatusCommand();
          
            var result = await addTaskByApprenticeshipIdCommandHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task UpdateTaskStatusCommandHandler_MoveToDone_Test()
        {
            await PopulateDbContext();

            var addTaskByApprenticeshipIdCommandHandler = new UpdateTaskStatusCommandHandler(DbContext);

            int taskId = DbContext.Task.First().TaskId;
            var completionDateTime = DbContext.Task.First().CompletionDateTime;

            DbContext.Task.First().CreatedDateTime = DbContext.Task.First().CompletionDateTime;
            await DbContext.SaveChangesAsync();

            var command = new UpdateTaskStatusCommand() { Status = 1, TaskId = taskId};
            
            var result = await addTaskByApprenticeshipIdCommandHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
            Assert.That(DbContext.Task.First().CompletionDateTime, Is.Not.EqualTo(completionDateTime));
        }

        private async System.Threading.Tasks.Task PopulateDbContext()
        {
            var tasks = _fixture.CreateMany<Domain.Entities.Task>().ToArray();
            await DbContext.Task.AddRangeAsync(tasks);
            await DbContext.SaveChangesAsync();
        }
    }
}
