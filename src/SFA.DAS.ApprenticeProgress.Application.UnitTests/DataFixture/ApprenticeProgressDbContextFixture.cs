using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class ApprenticeProgressDbContextFixture
    {
        [SetUp]
        public void BaseSetup()
        {
            var options = new DbContextOptionsBuilder<ApprenticeProgressDataContext>()
                .UseInMemoryDatabase($"SFA.DAS.ApprenticeProgress.Database_{DateTime.UtcNow.ToFileTimeUtc()}")
                .Options;

            DbContext = new ApprenticeProgressDataContext(options);
        }

        public ApprenticeProgressDataContext DbContext { get; private set; }

     //   [TearDown]
     //   public void TearDown() => DbContext?.Dispose();
    }
}
