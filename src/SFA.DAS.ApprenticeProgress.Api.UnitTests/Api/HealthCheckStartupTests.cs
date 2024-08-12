namespace SFA.DAS.ApprenticeProgress.Api.UnitTests.AppStart
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using NUnit.Framework;
    using SFA.DAS.ApprenticeProgress.Api.AppStart;

    [TestFixture]
    public static class HealthCheckStartupTests
    {
        [Test]
        public static void CannotCallUseHealthChecksWithNullApp()
        {
            Assert.Throws<ArgumentNullException>(() => default(IApplicationBuilder).UseHealthChecks());
        }
    }
}