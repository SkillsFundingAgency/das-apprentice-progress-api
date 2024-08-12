using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Api.Controllers;
using SFA.DAS.Testing.AutoFixture;
using static SFA.DAS.ApprenticeProgress.Api.Controllers.TaskController;

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

        [Test, MoqAutoData]
        public async Task AddTaskByApprenticeshipId_test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();
            int taskId = 1;

            ApprenticeTaskDataRequest data = new ApprenticeTaskDataRequest()
            {
                TaskId = 1,
                ApprenticeshipId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                DueDate = new DateTime(2019, 05, 09),
                Title = "title",
                ApprenticeshipCategoryId = 0,
                Note = "note",
                CompletionDateTime = new DateTime(2019, 05, 09),
                CreatedDateTime = new DateTime(2019, 05, 09),
                CategoryId = 1,
                Status = 1,
                ReminderValue = 1,
                ReminderUnit = 1,
                ReminderStatus = 1
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.AddTaskByApprenticeshipId(apprenticeshipIdentifier, data);
            result.Should().BeOfType(typeof(OkResult));
        }

        [Test, MoqAutoData]
        public async Task UpdateTaskByApprenticeshipIdAndTaskId_test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();

            ApprenticeTaskDataRequest data = new ApprenticeTaskDataRequest()
            {
                TaskId = 1,
                ApprenticeshipId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                DueDate = new DateTime(2019, 05, 09),
                Title = "title",
                ApprenticeshipCategoryId = 0,
                Note = "note",
                CompletionDateTime = new DateTime(2019, 05, 09),
                CreatedDateTime = new DateTime(2019, 05, 09),
                CategoryId = 1,
                Status = 1,
                ReminderValue = 1,
                ReminderUnit = 1,
                ReminderStatus = 1
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.UpdateTaskByApprenticeshipIdAndTaskId(apprenticeshipIdentifier, data);
            result.Should().BeOfType(typeof(OkResult));
        }

        [Test, MoqAutoData]
        public async Task RemoveTaskByApprenticeshipIdAndTaskId_test(
               [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();
            int taskId = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.RemoveTaskByApprenticeshipIdAndTaskId(apprenticeshipIdentifier, taskId);
            result.Should().BeOfType(typeof(OkResult));
        }
    }
}