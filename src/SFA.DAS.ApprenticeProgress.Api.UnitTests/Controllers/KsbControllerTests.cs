using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Api.Controllers;
using SFA.DAS.Testing.AutoFixture;
using static SFA.DAS.ApprenticeProgress.Api.Controllers.KsbController;

namespace SFA.DAS.ApprenticeApp.UnitTests
{
    public class KsbControllerTests
    {
        [Test, MoqAutoData]
        public async Task AddUpdateKsbProgress_test(
            [Greedy] KsbController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = 1;

            var data = new ApprenticeKsbProgressData
            {
                ApprenticeshipId = 1,
                KSBProgressType = 1,
                KSBId = new Guid(),
                KsbKey = "key",
                CurrentStatus = 1
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.AddUpdateKsbProgress(apprenticeshipIdentifier, data);
            result.Should().BeOfType(typeof(OkResult));
        }

        [Test, MoqAutoData]
        public async Task RemoveTaskToKsbProgress_test(
            [Greedy] KsbController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = 1;

            int ksbProgressId = 1;
            int taskId = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.RemoveTaskToKsbProgress(apprenticeshipIdentifier, ksbProgressId, taskId);
            result.Should().BeOfType(typeof(OkResult));
        }

        [Test, MoqAutoData]
        public async Task GetKsbsByApprenticeshipIdAndGuidListQuery_test(
            [Greedy] KsbController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = 1;

            var guidlist = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetKsbsByApprenticeshipIdAndGuidListQuery(apprenticeshipIdentifier, guidlist);
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Test, MoqAutoData]
        public async Task GetKsbsByApprenticeshipIdQuery_test(
            [Greedy] KsbController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = 1;

            var guidlist = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetKsbsByApprenticeshipId(apprenticeshipIdentifier);
            result.Should().BeOfType(typeof(NotFoundResult));
        }
    }
}