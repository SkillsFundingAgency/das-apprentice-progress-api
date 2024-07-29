using System;
using Newtonsoft.Json;

namespace SFA.DAS.ApprenticeProgress.Domain.Types
{
    public class ApprenticeshipCategory
    {
        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("ApprenticeshipId")]
        public Guid? ApprenticeshipId { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }
    }
}