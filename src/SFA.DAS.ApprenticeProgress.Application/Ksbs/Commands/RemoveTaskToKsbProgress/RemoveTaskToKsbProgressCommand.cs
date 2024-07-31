using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class RemoveTaskToKsbProgressCommand : IRequest<Unit>
    {
        public int KsbProgressId { get; set; }
        public int TaskId { get; set; }
    }
}