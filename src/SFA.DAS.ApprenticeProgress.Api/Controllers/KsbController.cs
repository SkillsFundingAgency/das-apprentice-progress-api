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

        [HttpPost("{apprenticeshipIdentifier}/ksbs/")]
        public async Task<IActionResult> AddUpdateKsbProgress(long apprenticeshipIdentifier, [FromBody] ApprenticeKsbProgressData request)
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
            public long ApprenticeshipId { get; set; }
            public int? KSBProgressType { get; set; }
            public Guid? KSBId { get; set; }
            public string KsbKey { get; set; }
            public int? CurrentStatus { get; set; }
        }

        [HttpDelete("{apprenticeshipIdentifier}/ksbs/{ksbProgressId}/taskid/{taskId}")]
        public async Task<IActionResult> RemoveTaskToKsbProgress(long apprenticeshipIdentifier, int ksbProgressId, int taskId)
        {
            await _mediator.Send(new RemoveTaskToKsbProgressCommand
            {
                KsbProgressId = ksbProgressId,
                TaskId = taskId
            });

            return Ok();
        }

        [HttpGet("{apprenticeshipIdentifier}/ksbsguids")]
        public async Task<IActionResult> GetKsbsByApprenticeshipIdAndGuidList(long apprenticeshipIdentifier, [FromQuery] Guid[] guids)
        {
            var result = await _mediator.Send(new GetKsbsByApprenticeshipIdAndGuidListQuery { ApprenticeshipId = apprenticeshipIdentifier, KsbIds = guids});
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{apprenticeshipIdentifier}/ksbs")]
        public async Task<IActionResult> GetKsbsByApprenticeshipId(long apprenticeshipIdentifier)
        {
            var result = await _mediator.Send(new GetKsbsByApprenticeshipIdQuery{ ApprenticeshipId = apprenticeshipIdentifier });
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}