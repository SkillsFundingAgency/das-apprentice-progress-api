using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Api.Controllers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Api.UnitTests.Controllers
{
    public class NotificationControllerTests
    {
        [Test, MoqAutoData]
        public async Task CreateApprenticeshipProgress_test(
            [Greedy] NotificationController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeIdentifier = Guid.NewGuid();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.CreateApprenticeshipProgress(apprenticeIdentifier);

            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Test, MoqAutoData]
        public async Task GetApprenticeProgressByApprenticeId_test(
            [Greedy] NotificationController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeIdentifier = Guid.NewGuid();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetApprenticeProgressByApprenticeId(apprenticeIdentifier);

            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Test, MoqAutoData]
        public async Task GetProgressNotificationById_test(
            [Greedy] NotificationController controller)
        {
            var httpContext = new DefaultHttpContext();
            var notificationId = Guid.NewGuid();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetProgressNotificationById(notificationId);

            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Test, MoqAutoData]
        public async Task GetProgressNotificationsToCheck_test(
            [Greedy] NotificationController controller)
        {
            var httpContext = new DefaultHttpContext();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetProgressNotificationsToCheck();

            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Test, MoqAutoData]
        public async Task UpdateProgressNotificationStatus_test(
            [Greedy] NotificationController controller)
        {
            var httpContext = new DefaultHttpContext();
            var notificationId = Guid.NewGuid();
            long apprenticeProgressId = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.UpdateProgressNotificationStatus(
                notificationId,
                apprenticeProgressId);

            result.Should().BeOfType(typeof(OkResult));
        }
    }
}
