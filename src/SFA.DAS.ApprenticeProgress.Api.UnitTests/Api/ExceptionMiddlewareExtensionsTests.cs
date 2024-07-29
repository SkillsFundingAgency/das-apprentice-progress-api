namespace SFA.DAS.ApprenticeProgress.Api.UnitTests.AppStart
{
    using SFA.DAS.ApprenticeProgress.Api.AppStart;
    using System;
    using NUnit.Framework;
    using NSubstitute;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;

    [TestFixture]
    public static class ExceptionMiddlewareExtensionsTests
    {

        [Test]
        public static void CannotCallConfigureExceptionHandlerWithNullApp()
        {
            Assert.Throws<ArgumentNullException>(() => default(IApplicationBuilder).ConfigureExceptionHandler(Substitute.For<ILogger>()));
        }
    }
}