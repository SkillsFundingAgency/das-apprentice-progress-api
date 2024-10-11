using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Functions.Api
{
    [ExcludeFromCodeCoverage]
    public class ApprenticeTask
    {
        public int TaskId { get; set; }
        public long ApprenticeshipId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Title { get; set; }
        public int? ApprenticeshipCategoryId { get; set; }
        public string Note { get; set; }
        public DateTime? CompletionDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public TaskStatus? Status { get; set; }

        [Flags]
        public enum TaskStatus
        {
            Todo = 0,
            Done = 1
        }
    }
}