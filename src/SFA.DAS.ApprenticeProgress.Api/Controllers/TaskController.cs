using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SFA.DAS.ApprenticeProgress.Application.Commands;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Domain.Entities;

namespace SFA.DAS.ApprenticeProgress.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("apprenticeships/")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // gets the task categories
        [HttpGet("{apprenticeshipIdentifier}/taskCategories")]
        public async Task<IActionResult> GetTaskCategories(Guid apprenticeshipIdentifier)
        {
            var result = await _mediator.Send(new GetTaskCategoriesQuery());
            if (result == null) return NotFound();
            return Ok(result);
        }

        // adds a new task categories
        [HttpPost("{apprenticeshipIdentifier}/taskCategories")]
        public async Task<IActionResult> AddTaskCategory(Guid apprenticeshipIdentifier, int taskId, int categoryId)
        {
            await _mediator.Send(new AddTaskCategoryCommand
            {
                TaskId = taskId,
                CategoryId = categoryId
            });

            return Ok();
        }

        // gets the task category
        [HttpGet("{apprenticeshipIdentifier}/taskCategories/{taskCategoryId}")]
        public async Task<IActionResult> GetTaskCategory(Guid apprenticeshipIdentifier, int taskId)
        {
            var result = await _mediator.Send(new GetTaskCategoryQuery { TaskId = taskId });
            if (result == null) return NotFound();
            return Ok(result);
        }

        // delete a task category
        [HttpDelete("{apprenticeshipIdentifier}/taskCategories/{taskCategoryId}")]
        public async Task<IActionResult> DeleteTaskCategory(Guid apprenticeshipIdentifier, int? taskCategoryId = 0)
        {
            await _mediator.Send(new DeleteTaskCategoryCommand
            {
                TaskId = taskCategoryId.Value
            });

            return Ok();
        }

        // Gets the tasks
        [HttpGet("{apprenticeshipIdentifier}/tasks")]
        public async Task<IActionResult> GetTasksByApprenticeshipId(Guid apprenticeshipIdentifier, DateTime? fromDate, DateTime? toDate, int status)
        {
            var result = await _mediator.Send(new GetTasksByApprenticeshipIdQuery { ApprenticeshipId = apprenticeshipIdentifier, Status = status, ToDate = toDate, FromDate = fromDate });
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Add a new tasks
        [HttpPost("{apprenticeshipIdentifier}/tasks")]
        public async Task<IActionResult> AddTaskByApprenticeshipId(Guid apprenticeshipIdentifier, DateTime? dueDate, string title, string note, DateTime? completionDateTime, int status, [FromForm] int[] ksbsLinked, int reminderStatus, int reminderValue, int reminderUnit, [FromForm] IFormFile[] files, int? apprenticeshipCategoryId = 0)
        {
            await _mediator.Send(new AddTaskByApprenticeshipIdCommand
            {
                ApprenticeshipId = apprenticeshipIdentifier,
                DueDate = dueDate.Value,
                Title = title,
                ApprenticeshipCategoryId = apprenticeshipCategoryId,
                Note = note,
                CompletionDateTime = completionDateTime.Value,
                Status = status,
                Files = files,
                ReminderUnit = reminderUnit,
                ReminderValue = reminderValue,
                ReminderStatus = reminderStatus,
                KsbsLinked = ksbsLinked,
            });

            return Ok();
        }

        // chnage a task status
        [HttpPost("{apprenticeshipIdentifier}/tasks/{taskId}/changestatus/{status}")]
        public async Task<IActionResult> ChangeTaskStatus(int taskId, int status)
        {
            await _mediator.Send(new UpdateTaskStatusCommand
            {
                TaskId = taskId,
                Status = status
            });

            return Ok();
        }

        // Get a task by Id
        [HttpGet("{apprenticeshipIdentifier}/tasks/{taskId}")]
        public async Task<IActionResult> GetTaskByApprentishipIdAndTaskId(Guid apprenticeshipIdentifier, int taskId)
        {
            var result = await _mediator.Send(new GetTaskByApprentishipIdAndTaskIdQuery { ApprenticeshipId = apprenticeshipIdentifier, TaskId = taskId });
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Update a task by Id
        [HttpPut("{apprenticeshipIdentifier}/tasks/{taskId}")]
        public async Task<IActionResult> UpdateTaskByApprentishipIdAndTaskId(Guid apprenticeshipIdentifier, int taskId, DateTime? dueDate, string title, string note, DateTime? completionDateTime, int status, [FromForm] int[] ksbsLinked, int reminderStatus, int reminderValue, int reminderUnit, [FromForm] IFormFile[] files, int? apprenticeshipCategoryId = 0)
        {
            await _mediator.Send(new UpdateTaskByApprentishipIdAndTaskIdCommand
            {
                TaskId = taskId,
                ApprenticeshipId = apprenticeshipIdentifier,
                DueDate = dueDate.Value,
                Title = title,
                ApprenticeshipCategoryId = apprenticeshipCategoryId,
                Note = note,
                CompletionDateTime = completionDateTime.Value,
                Status = status,
                Files = files,
                ReminderUnit = reminderUnit,
                ReminderValue = reminderValue,
                ReminderStatus = reminderStatus,
                KsbsLinked = ksbsLinked
            });

            return Ok();
        }

        // Remove a task Id
        [HttpDelete("{apprenticeshipIdentifier}/tasks/{taskId}")]
        public async Task<IActionResult> RemoveTaskByApprentishipIdAndTaskId(Guid apprenticeshipIdentifier, int taskId)
        {
            await _mediator.Send(new RemoveTaskByApprentishipIdAndTaskIdCommand
            {
                ApprenticeshipId = apprenticeshipIdentifier,
                TaskId = taskId
            });

            return Ok();
        }
    }
}