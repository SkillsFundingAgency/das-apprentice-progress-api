using System;
using Newtonsoft.Json;


namespace SFA.DAS.ApprenticeProgress.Domain.Types
{
    public class KSBProgress
    {
        [JsonProperty("ApprenticeshipProgressId")]
        public Guid ApprenticeshipProgressId { get; set; }

        [JsonProperty("KSBProgressId")]
        public int? KSBProgressId { get; set; }

        [JsonProperty("KSBProgressType")]
        public int? KSBProgressType { get; set; }

        [JsonProperty("KSBId")]
        public Guid? KSBId { get; set; }

        [JsonProperty("KSBKey")]
        public string KSBKey { get; set; }

        [JsonProperty("CurrentStatus")]
        public int? CurrentStatus { get; set; }
    }  
}