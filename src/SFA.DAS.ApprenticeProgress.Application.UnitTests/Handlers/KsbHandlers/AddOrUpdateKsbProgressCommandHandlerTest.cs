using System.Linq;
using System.Threading;
using AutoFixture;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Commands;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class AddOrUpdateKsbProgressCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task AddOrUpdateKsbProgressCommandHandler_Test()
        {
            await PopulateDbContext();

            var addOrUpdateKsbProgressCommandHandler = new AddOrUpdateKsbProgressCommandHandler(DbContext);

            var command = new AddOrUpdateKsbProgressCommand();

            var result = await addOrUpdateKsbProgressCommandHandler.Handle(command, CancellationToken.None);
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