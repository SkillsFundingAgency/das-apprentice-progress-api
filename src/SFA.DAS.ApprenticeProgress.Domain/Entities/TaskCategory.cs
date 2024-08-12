using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    public class TaskCategory
    {
        public int TaskId  { get; set; }
        public int? CategoryId { get; set; }

        [NotMapped]
        public virtual ApprenticeshipCategory ApprenticeshipCategory { get; set; }
    }
}