using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeProgress.Application.Commands;
using SFA.DAS.ApprenticeProgress.Application.Models;
using SFA.DAS.ApprenticeProgress.Application.Queries;

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

        [HttpGet("{apprenticeshipIdentifier}/taskCategories")]
        public async Task<IActionResult> GetTaskCategories(long apprenticeshipIdentifier)
        {
            var result = await _mediator.Send(new GetTaskCategoriesQuery());
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("{apprenticeshipIdentifier}/taskCategories")]
        public async Task<IActionResult> AddTaskCategory(long apprenticeshipIdentifier, int taskId, int categoryId)
        {
            await _mediator.Send(new AddTaskCategoryCommand
            {
                TaskId = taskId,
                CategoryId = categoryId
            });

            return Ok();
        }

        [HttpGet("{apprenticeshipIdentifier}/taskCategories/{taskCategoryId}")]
        public async Task<IActionResult> GetTaskCategory(long apprenticeshipIdentifier, int taskId)
        {
            var result = await _mediator.Send(new GetTaskCategoryQuery { TaskId = taskId });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{apprenticeshipIdentifier}/taskCategories/{taskCategoryId}")]
        public async Task<IActionResult> DeleteTaskCategory(long apprenticeshipIdentifier, int? taskCategoryId = 0)
        {
            await _mediator.Send(new DeleteTaskCategoryCommand
            {
                TaskId = taskCategoryId.Value
            });

            return Ok();
        }

        [HttpGet("{apprenticeshipIdentifier}/fromDate/{fromDate}/toDate/{toDate}/status/{status}")]
        public async Task<IActionResult> GetTasksByApprenticeshipId(long apprenticeshipIdentifier, DateTime? fromDate, DateTime? toDate, int status)
        {
            var result = await _mediator.Send(new GetTasksByApprenticeshipIdQuery { ApprenticeshipId = apprenticeshipIdentifier, Status = status, ToDate = toDate, FromDate = fromDate });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("{apprenticeshipIdentifier}/tasks")]
        public async Task<IActionResult> AddTaskByApprenticeshipId(long apprenticeshipIdentifier, [FromBody] ApprenticeTaskDataRequest request)
        {
            await _mediator.Send(new AddTaskByApprenticeshipIdCommand
            {
                ApprenticeshipId = apprenticeshipIdentifier,
                DueDate = request.DueDate.Value,
                Title = request.Title,
                ApprenticeshipCategoryId = request.ApprenticeshipCategoryId,
                Note = request.Note,
                CompletionDateTime = request.CompletionDateTime.Value,
                Status = request.Status,
                Files = request.Files,
                ReminderUnit = request.ReminderUnit,
                ReminderValue = request.ReminderValue,
                ReminderStatus = request.ReminderStatus,
                KsbsLinked = request.KsbsLinked
            });

            return Ok();
        }

        public class ApprenticeTaskDataRequest
        {
            public long ApprenticeshipId { get; set; }
            public int? TaskId { get; set; }
            public DateTime? DueDate { get; set; }
            public string Title { get; set; }
            public int? ApprenticeshipCategoryId { get; set; }
            public string Note { get; set; }
            public DateTime? CompletionDateTime { get; set; }
            public DateTime? CreatedDateTime { get; set; }
            public int? CategoryId { get; set; }
            public int? Status { get; set; }
            public List<ApprenticeTaskDataFileRequest> Files { get; set; } = null!;
            public int? ReminderUnit { get; set; }
            public int? ReminderValue { get; set; }
            public int? ReminderStatus { get; set; }
            public string[] KsbsLinked { get; set; }
        }

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

        [HttpGet("{apprenticeshipIdentifier}/tasks/{taskId}")]
        public async Task<IActionResult> GetTaskByApprenticeshipIdAndTaskId(long apprenticeshipIdentifier, int taskId)
        {
            var result = await _mediator.Send(new GetTaskByApprenticeshipIdAndTaskIdQuery { ApprenticeshipId = apprenticeshipIdentifier, TaskId = taskId });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("{apprenticeshipIdentifier}/tasks/{taskId}")]
        public async Task<IActionResult> UpdateTaskByApprenticeshipIdAndTaskId(long apprenticeshipIdentifier, [FromBody] ApprenticeTaskDataRequest request)
        {
            await _mediator.Send(new UpdateTaskByApprenticeshipIdAndTaskIdCommand
            {
                TaskId = request.TaskId.Value,
                ApprenticeshipId = apprenticeshipIdentifier,
                DueDate = request.DueDate.Value,
                Title = request.Title,
                ApprenticeshipCategoryId = request.ApprenticeshipCategoryId,
                Note = request.Note,
                CompletionDateTime = request.CompletionDateTime.Value,
                Status = request.Status,
                Files = request.Files,
                ReminderUnit = request.ReminderUnit,
                ReminderValue = request.ReminderValue,
                ReminderStatus = request.ReminderStatus,
                KsbsLinked = request.KsbsLinked
            });

            return Ok();
        }

        [HttpDelete("{apprenticeshipIdentifier}/tasks/{taskId}")]
        public async Task<IActionResult> RemoveTaskByApprenticeshipIdAndTaskId(long apprenticeshipIdentifier, int taskId)
        {
            await _mediator.Send(new RemoveTaskByApprenticeshipIdAndTaskIdCommand
            {
                ApprenticeshipId = apprenticeshipIdentifier,
                TaskId = taskId
            });

            return Ok();
        }

        /// <summary>
        /// For use with progress worker to yield reminders that are due this minute
        /// </summary>
        /// <returns></returns>
        [HttpGet("gettaskreminders")]
        public async Task<IActionResult> GetTaskReminders()
        {
            var result = await _mediator.Send(new GetTaskRemindersQuery {  });
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// For use with progress worker to update task reminders with a specified status
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        [HttpPost("updatetaskreminders")]
        public async Task<IActionResult> UpdateTaskReminders(int taskId, int statusId)
        {
            await _mediator.Send(new UpdateTaskRemindersCommand
            {
                TaskId = taskId,
                StatusId = statusId
            });

            return Ok();
        }
    }
}