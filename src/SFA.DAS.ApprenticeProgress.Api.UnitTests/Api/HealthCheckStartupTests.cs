namespace SFA.DAS.ApprenticeProgress.Api.UnitTests.AppStart
{
    using SFA.DAS.ApprenticeProgress.Api.AppStart;
    using System;
    using NUnit.Framework;
    using NSubstitute;
    using Microsoft.AspNetCore.Builder;

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