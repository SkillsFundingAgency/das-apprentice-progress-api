using Newtonsoft.Json;

namespace SFA.DAS.ApprenticeProgress.Domain.Types
{
    public class TaskFile
    {
        [JsonProperty("TaskId")]
        public int TaskId { get; set; }

        [JsonProperty("TaskFileId")]
        public int? TaskFileId { get; set; }

        [JsonProperty("FileType")]
        public string FileType { get; set; }
    }
}