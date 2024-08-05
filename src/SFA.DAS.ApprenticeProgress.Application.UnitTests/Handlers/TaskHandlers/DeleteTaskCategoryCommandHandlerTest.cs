using System;
using System.Linq;
using System.Threading;
using AutoFixture;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Commands;
using SFA.DAS.ApprenticeProgress.Application.Models;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class DeleteTaskCategoryCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task DeleteTaskCategoryCommandHandler_test()
        {
            await PopulateDbContext();

            var deleteTaskCategoryCommandHandler = new DeleteTaskCategoryCommandHandler(DbContext);

            var command = new DeleteTaskCategoryCommand()
            {
                TaskId = 1
            };
          
            var result = await deleteTaskCategoryCommandHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        private async System.Threading.Tasks.Task PopulateDbContext()
        {
            var ksbprogress = _fixture.CreateMany<Domain.Entities.Task>().ToArray();
            await DbContext.Task.AddRangeAsync(ksbprogress);
            await DbContext.SaveChangesAsync();
        }

    }
}