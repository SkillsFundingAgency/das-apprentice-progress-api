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
    public class UpdateTaskRemindersCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task UpdateTaskRemindersCommandHandler_Test()
        {
            await PopulateDbContext();

            var UpdateTaskRemindersCommandHandler = new UpdateTaskRemindersCommandHandler(DbContext);

            var command = new UpdateTaskRemindersCommand();
          
            var result = await UpdateTaskRemindersCommandHandler.Handle(command, CancellationToken.None);
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
