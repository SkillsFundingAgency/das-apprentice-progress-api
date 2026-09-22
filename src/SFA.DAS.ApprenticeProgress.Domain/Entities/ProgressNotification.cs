using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class ProgressNotification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string NotificationId { get; set; }
        public string NotificationScope { get; set; }
        public bool IsEnabled { get; set; }
        public ActivationPoint ActivationPoint { get; set; }
        public string Delay { get; set; }
        public DelayUnit DelayUnit { get; set; }
    }

    public enum ActivationPoint 
    {
        FromStart = 1,
        FromEnd = 2,
        Immediate = 3
    }

    public enum DelayUnit
    {
        Day = 1,
        Week = 2,
        Month = 3
    }
}
