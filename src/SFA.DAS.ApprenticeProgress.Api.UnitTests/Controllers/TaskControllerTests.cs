using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Api.Controllers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeApp.UnitTests
{
    public class TaskControllerTests
    {
        [Test, MoqAutoData]
        public async Task GetTaskCategories_Test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetTaskCategories(apprenticeshipIdentifier);
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Test, MoqAutoData]
        public async Task AddTaskCategory_test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();
            int taskId = 1;
            int catId = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.AddTaskCategory(apprenticeshipIdentifier, taskId, catId);
            result.Should().BeOfType(typeof(OkResult));
        }

        [Test, MoqAutoData]
        public async Task GetTaskCategory_Test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();
            int taskId = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetTaskCategory(apprenticeshipIdentifier, taskId);
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Test, MoqAutoData]
        public async Task DeleteTaskCategory_Test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();
            int taskId = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.DeleteTaskCategory(apprenticeshipIdentifier, taskId);
            result.Should().BeOfType(typeof(OkResult));
        }

        [Test, MoqAutoData]
        public async Task GetTasksByApprenticeshipId_Test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();
            var fromDate = new DateTime();
            var toDate = new DateTime();
            int status = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetTasksByApprenticeshipId(apprenticeshipIdentifier, fromDate, toDate, status);
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Test, MoqAutoData]
        public async Task ChangeTaskStatus_Test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            int taskId = 1;
            int status = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.ChangeTaskStatus(taskId, status);
            result.Should().BeOfType(typeof(OkResult));
        }


        [Test, MoqAutoData]
        public async Task GetTaskByApprenticeshipIdAndTaskId_test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();
            int taskId = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetTaskByApprenticeshipIdAndTaskId(apprenticeshipIdentifier, taskId);
            result.Should().BeOfType(typeof(NotFoundResult));
        }
    }
}