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
        public async Task Get_Categories_Test(
            [Greedy] TaskController controller)
        {
            var httpContext = new DefaultHttpContext();
            var apprenticeshipIdentifier = Guid.NewGuid();
            int taskId = 1;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            //var result = await controller.GetTaskCategories(apprenticeshipIdentifier, taskId);
          //  result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

    }
}

