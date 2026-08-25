using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    public class ApprenticeshipProgressNotification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ApprenticeProgressId { get; set; }
        public Guid NotificationId { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ApprenticeshipProgress ApprenticeshipProgress { get; set; }
        public virtual ProgressNotification ProgressNotification { get; set; }
    }
}
