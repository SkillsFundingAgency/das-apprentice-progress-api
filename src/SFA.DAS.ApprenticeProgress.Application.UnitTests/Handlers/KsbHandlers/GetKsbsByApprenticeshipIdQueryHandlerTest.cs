using System;
using System.Linq;
using System.Threading;
using AutoFixture;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class GetKsbsByApprenticeshipIdQueryHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetKsbsByApprenticeshipIdQueryHandler_test()
        {
            await PopulateDbContext();

            var getKsbProgressQuery = new GetKsbsByApprenticeshipIdQueryHandler(DbContext);

            var command = new GetKsbsByApprenticeshipIdQuery();
            command.ApprenticeshipId = 1;

            var result = await getKsbProgressQuery.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        private async System.Threading.Tasks.Task PopulateDbContext()
        {
            var ksbprogress = _fixture.CreateMany<Domain.Entities.KSBProgress>().ToArray();
            await DbContext.KSBProgress.AddRangeAsync(ksbprogress);
            ksbprogress[0].ApprenticeshipId = 1;

            var taskJoins = _fixture.CreateMany<Domain.Entities.TaskKSBs>().ToArray();
            await DbContext.KSBProgress.AddRangeAsync(ksbprogress);
            taskJoins[0].TaskId = 1;
            taskJoins[0].KSBProgressId = 1;

            var tasks = _fixture.CreateMany<Domain.Entities.Task>().ToArray();
            await DbContext.KSBProgress.AddRangeAsync(ksbprogress);
            tasks[0].TaskId = 1;

            await DbContext.SaveChangesAsync();
        }
    }
}