using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeProgress.Application.Commands;

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





        // gets the ksb statsus
        [HttpGet("ksbStatuses")]
        public async Task<IActionResult> GetKsbStatuses()
        {
            return Ok();
        }

        // gets the ksb types
        [HttpGet("ksbTypes")]
        public async Task<IActionResult> GetKsbTypes()
        {
            return Ok();
        }

        // gets the ksbs
        [HttpGet("{apprenticeshipIdentifier}/ksbs")]
        public async Task<IActionResult> GetKsbsByApprenticeshipIdQuery(Guid apprenticeshipIdentifier)
        {
            return Ok();
        }

        // gets the ksb progress
        [HttpGet("{apprenticeshipIdentifier}/ksbs/{ksbKey}")]
        public async Task<IActionResult> GetKsbProgress(Guid apprenticeshipIdentifier, int ksbKey)
        {
            return Ok();
        }

        // Update a task by Id
        [HttpPut("{apprenticeshipIdentifier}/ksbs/{ksbKey}")]
        public async Task<IActionResult> UpdateKsb(Guid apprenticeshipIdentifier, int? ksbKey)
        {
            return Ok();
        }
    }
}