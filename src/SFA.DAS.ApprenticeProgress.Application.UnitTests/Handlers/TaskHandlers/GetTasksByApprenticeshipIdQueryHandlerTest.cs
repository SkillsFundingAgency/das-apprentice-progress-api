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
    public class GetTasksByApprenticeshipIdQueryHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetTasksByApprenticeshipIdQueryHandler_test_status1()
        {
            await PopulateDbContext();

            var getTasksByApprenticeshipIdQueryHandler = new GetTasksByApprenticeshipIdQueryHandler(DbContext);

            var command = new GetTasksByApprenticeshipIdQuery();
            command.Status = 1;

            var result = await getTasksByApprenticeshipIdQueryHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }


        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetTasksByApprenticeshipIdQueryHandler_test_status2()
        {
            await PopulateDbContext();

            var getTasksByApprenticeshipIdQueryHandler = new GetTasksByApprenticeshipIdQueryHandler(DbContext);

            var command = new GetTasksByApprenticeshipIdQuery();
            command.Status = 2;

            var result = await getTasksByApprenticeshipIdQueryHandler.Handle(command, CancellationToken.None);
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