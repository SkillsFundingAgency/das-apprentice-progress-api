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
    public class RemoveTaskByApprentishipIdAndTaskIdCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task RemoveTaskByApprentishipIdAndTaskIdCommandHandlerTest_test()
        {
            await PopulateDbContext();

            var removeTaskByApprentishipIdAndTaskIdCommandHandlerTest = new RemoveTaskByApprentishipIdAndTaskIdCommandHandler(DbContext);

            var command = new RemoveTaskByApprentishipIdAndTaskIdCommand();
          
            var result = await removeTaskByApprentishipIdAndTaskIdCommandHandlerTest.Handle(command, CancellationToken.None);
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