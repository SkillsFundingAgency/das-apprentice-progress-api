using System.Linq;
using System.Threading;
using AutoFixture;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Commands;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class RemoveTaskByApprenticeshipIdAndTaskIdCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task RemoveTaskByApprenticeshipIdAndTaskIdCommandHandlerTest_test()
        {
            await PopulateDbContext();

            var removeTaskByApprenticeshipIdAndTaskIdCommandHandlerTest = new RemoveTaskByApprenticeshipIdAndTaskIdCommandHandler(DbContext);

            var command = new RemoveTaskByApprenticeshipIdAndTaskIdCommand();
          
            var result = await removeTaskByApprenticeshipIdAndTaskIdCommandHandlerTest.Handle(command, CancellationToken.None);
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