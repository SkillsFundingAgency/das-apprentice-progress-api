using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeProgress.Application.Commands;
using SFA.DAS.ApprenticeProgress.Application.Queries;

namespace SFA.DAS.ApprenticeProgress.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("apprenticeships/")]
    public class KsbController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KsbController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Add or update ksb progress
        [HttpPost("{apprenticeshipIdentifier}/ksbs/")]
        public async Task<IActionResult> AddUpdateKsbProgress(Guid apprenticeshipIdentifier, [FromBody] ApprenticeKsbProgressData request)
        {
            await _mediator.Send(new AddOrUpdateKsbProgressCommand
            {
                ApprenticeshipId = apprenticeshipIdentifier,
                KSBProgressType = request.KSBProgressType.Value,
                KSBId = request.KSBId.Value,
                KsbKey = request.KsbKey,
                CurrentStatus = request.CurrentStatus.Value
            });

            return Ok();
        }

        public class ApprenticeKsbProgressData
        {
            public Guid ApprenticeshipId { get; set; }
            public int? KSBProgressType { get; set; }
            public Guid? KSBId { get; set; }
            public string KsbKey { get; set; }
            public int? CurrentStatus { get; set; }
        }

        // Remove task to ksb join
        [HttpDelete("{apprenticeshipIdentifier}/ksbs/{ksbProgressId}/taskid/{taskId}")]
        public async Task<IActionResult> RemoveTaskToKsbProgress(Guid apprenticeshipIdentifier, int ksbProgressId, int taskId)
        {
            await _mediator.Send(new RemoveTaskToKsbProgressCommand
            {
                KsbProgressId = ksbProgressId,
                TaskId = taskId
            });

            return Ok();
        }

        // gets ksb progress for user based on passed in string list
        [HttpGet("{apprenticeshipIdentifier}/ksbs")]
        public async Task<IActionResult> GetKsbsByApprenticeshipIdAndGuidListQuery(Guid apprenticeshipIdentifier, [FromQuery] Guid[] guids)
        {
            var result = await _mediator.Send(new GetKsbsByApprenticeshipIdAndGuidListQuery { ApprenticeshipId = apprenticeshipIdentifier, KsbIds = guids});
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}