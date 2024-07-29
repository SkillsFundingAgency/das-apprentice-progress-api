using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class UpdateTaskStatusCommand : IRequest<Unit>
    {
        public int TaskId { get; set; }
        public int Status { get; set; }
    }
}