using System;
using System.Collections.Generic;
using MediatR;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class AddTaskByApprenticeshipIdCommand : IRequest<Unit>
    {
        public Guid ApprenticeshipId { get; set; }
        public int? TaskId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Title { get; set; }
        public int? ApprenticeshipCategoryId { get; set; }
        public string Note { get; set; }
        public DateTime? CompletionDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? CategoryId { get; set; }
        public int? Status { get; set; }

        public List<ApprenticeTaskDataFileRequest>? Files { get; set;}

        public int? ReminderUnit { get; set; }
        public int? ReminderValue { get; set; }
        public int? ReminderStatus { get; set; }

        public string[] KsbsLinked { get; set; }
    }
}