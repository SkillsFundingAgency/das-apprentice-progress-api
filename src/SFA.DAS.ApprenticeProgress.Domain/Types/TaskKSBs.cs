using Newtonsoft.Json;


namespace SFA.DAS.ApprenticeProgress.Domain.Types
{
    public class TaskKSBs
    {
        [JsonProperty("TaskId")]
        public int TaskId { get; set; }

        [JsonProperty("KSBProgressId")]
        public int?  KSBProgressId { get; set; }
    }
}