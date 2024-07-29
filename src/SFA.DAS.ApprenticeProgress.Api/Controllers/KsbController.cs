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
    [Route("ksb/")]
    public class KsbController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KsbController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // gets the ksb statsus
        [HttpGet("ksbStatuses")]
        public async Task<IActionResult> GetKsbStatuses()
        {
            var result = await _mediator.Send(new GetKsbStatusesQuery {  });
            if (result == null) return NotFound();
            return Ok(result);
        }

        // gets the ksb types
        [HttpGet("ksbTypes")]
        public async Task<IActionResult> GetKsbTypes()
        {
            var result = await _mediator.Send(new GetKsbTypesQuery { });
            if (result == null) return NotFound();
            return Ok(result);
        }

        // gets the ksbs
        [HttpGet("{apprenticeshipIdentifier}/ksbs")]
        public async Task<IActionResult> GetKsbsByApprenticeshipIdQuery(Guid apprenticeshipIdentifier)
        {
            var result = await _mediator.Send(new GetKsbsByApprenticeshipIdQuery { ApprenticeshipId = apprenticeshipIdentifier });
            if (result == null) return NotFound();
            return Ok(result);
        }

        // gets the ksb progress
        [HttpGet("{apprenticeshipIdentifier}/ksbs/{ksbKey}")]
        public async Task<IActionResult> GetKsbProgress(Guid apprenticeshipIdentifier, int ksbKey)
        {
            var result = await _mediator.Send(new GetKsbProgressQuery { });
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Update a task by Id
        [HttpPut("{apprenticeshipIdentifier}/ksbs/{ksbKey}")]
        public async Task<IActionResult> UpdateKsb(Guid apprenticeshipIdentifier, int? ksbKey)
        {
            await _mediator.Send(new UpdateKsbCommand
            {
                ApprenticeId = apprenticeshipIdentifier,
                KsbKey = ksbKey.Value
            });

            return Ok();
        }
    }
}