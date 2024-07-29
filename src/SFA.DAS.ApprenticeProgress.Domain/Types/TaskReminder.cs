using Newtonsoft.Json;


namespace SFA.DAS.ApprenticeProgress.Domain.Types
{
    public class TaskReminder
    {
        [JsonProperty("TaskId")]
        public int TaskId { get; set; }

        [JsonProperty("ReminderId")]
        public int? ReminderId { get; set; }

        [JsonProperty("ReminderValue")]
        public int? ReminderValue { get; set; }

        [JsonProperty("ReminderUnit")]
        public int? ReminderUnit { get; set; }

        [JsonProperty("Status")]
        public int? Status { get; set; }
    }
}