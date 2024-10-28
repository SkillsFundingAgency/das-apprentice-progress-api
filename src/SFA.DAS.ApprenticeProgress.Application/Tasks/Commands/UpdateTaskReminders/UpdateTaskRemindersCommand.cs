using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class UpdateTaskRemindersCommand : IRequest<Unit>
    {
        public int TaskId { get; set; }
        public int StatusId { get; set; }
    }
}