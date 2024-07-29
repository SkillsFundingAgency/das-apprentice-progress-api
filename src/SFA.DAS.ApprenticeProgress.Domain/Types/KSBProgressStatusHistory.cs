using System;
using Newtonsoft.Json;

namespace SFA.DAS.ApprenticeProgress.Domain.Types
{
    public class KSBProgressStatusHistory
    {
        [JsonProperty("KSBProgressId")]
        public long KSBProgressId { get; set; }

        [JsonProperty("Status")]
        public int? Status { get; set; }

        [JsonProperty("StatusTime")]
        public DateTime? StatusTime { get; set; }
    }  
}