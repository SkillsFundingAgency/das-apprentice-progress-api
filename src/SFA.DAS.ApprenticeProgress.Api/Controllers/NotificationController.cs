using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Commands.AddApprenticeshipProgress;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Commands.UpdateProgressNotificationStatus;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetApprenticeshipProgressNotifications;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetProgressNotificationsById;
using SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetProgressNotificationsToCheck;

namespace SFA.DAS.ApprenticeProgress.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("notifications/")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator) => _mediator = mediator;

        [HttpPost("apprenticeship-progress/{apprenticeIdentifier}/")]
        public async Task<IActionResult> CreateApprenticeshipProgress(Guid apprenticeIdentifier)
        {
            var result = await _mediator.Send(new AddApprenticeshipProgressCommand
            {
                ApprenticeIdentifier = apprenticeIdentifier
            });

            return Ok(result);
        }

        [HttpGet("apprenticeship-progress/{apprenticeIdentifier}")]
        public async Task<IActionResult> GetApprenticeProgressByApprenticeId(Guid apprenticeIdentifier)
        {
            var result = await _mediator.Send(new GetApprenticeshipProgressNotificationsByIdQuery
            {
                ApprenticeIdentifier = apprenticeIdentifier
            });

            return Ok(result);
        }

        [HttpGet("progress-notification/{notificationId}")]
        public async Task<IActionResult> GetProgressNotificationById(Guid notificationId)
        {
            var result = await _mediator.Send(new GetProgressNotificationsByIdQuery
            {
                NotificationId = notificationId
            });

            return Ok(result);
        }

        [HttpGet("progress-notification-to-check")]
        public async Task<IActionResult> GetProgressNotificationsToCheck()
        {
            var result = await _mediator.Send(new GetProgressNotificationsToCheckQuery());
            return Ok(result);
        }

        [HttpPatch("updateProgressNotificationStatus/{notificationId}/{apprenticeProgressId}")]
        public async Task<IActionResult> UpdateProgressNotificationStatus(Guid notificationId, long apprenticeProgressId)
        {
            var result = await _mediator.Send(new UpdateProgressNotificationStatusCommand
            {
                NotificationId = notificationId,
                ApprenticeProgressId = apprenticeProgressId            
            });

            return Ok();
        }
    }
}
