using System;
using Newtonsoft.Json;


namespace SFA.DAS.ApprenticeProgress.Domain.Types
{
    public class Task
    {
        [JsonProperty("ApprenticeshipId")]
        public Guid ApprenticeshipId { get; set; }

        [JsonProperty("TaskId")]
        public int?  TaskId { get; set; }

        [JsonProperty("DueDate")]
        public DateTime?  DueDate { get; set; }

        [JsonProperty("ApprenticeshipCategoryId")]
        public int? ApprenticeshipCategoryId { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Note")]
        public string Note { get; set; }

        [JsonProperty("CompletionDateTime")]
        public DateTime?  CompletionDateTime { get; set; }

        [JsonProperty("Status")]
        public int? Status { get; set; }
    }
}