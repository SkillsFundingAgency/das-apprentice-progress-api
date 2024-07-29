using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class UpdateTaskByApprentishipIdAndTaskIdCommand : IRequest<Unit>
    {
        public Guid ApprenticeshipId { get; set; }
        public int TaskId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime? CompletionDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? Status { get; set; }
        public int? ApprenticeshipCategoryId { get; set; }

        // files block
        public IFormFile[] Files { get; set; }

        // reminder block
        public int? ReminderUnit { get; set; }
        public int? ReminderValue { get; set; }
        public int? ReminderStatus { get; set; }

        // ksbs linked
        public int[] KsbsLinked { get; set; }
    }
}