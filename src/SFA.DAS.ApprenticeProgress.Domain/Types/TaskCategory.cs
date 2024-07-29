using Newtonsoft.Json;

namespace SFA.DAS.ApprenticeProgress.Domain.Types
{
    public class TaskCategory
    {
        [JsonProperty("TaskId")]
        public int TaskId  { get; set; }

        [JsonProperty("CategoryId")]
        public int? CategoryId { get; set; }
    }
}