using System.Linq;
using System.Threading;
using AutoFixture;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class GetKsbsByApprenticeshipIdAndGuidListTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task GetKsbsByApprenticeshipIdAndGuidListTest_test()
        {
            await PopulateDbContext();

            var updateKsbCommandHandler = new GetKsbsByApprenticeshipIdAndGuidListQueryHandler(DbContext);

            var command = new GetKsbsByApprenticeshipIdAndGuidListQuery();

            var result = await updateKsbCommandHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        private async System.Threading.Tasks.Task PopulateDbContext()
        {
            var ksbprogress = _fixture.CreateMany<Domain.Entities.KSBProgress>().ToArray();
            await DbContext.KSBProgress.AddRangeAsync(ksbprogress);
            await DbContext.SaveChangesAsync();
        }
    }
}