using System;
using System.Collections.Generic;
using System.Text;
using Contentful.Core;
using SFA.DAS.ApprenticeProgress.Application.Interfaces;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Infrastructure.Contentful
{
    public class ContentfulService : IContentfulService
    {
        private readonly ContentfulClient _client;

        public ContentfulService(ContentfulClient client)
        {
            _client = client;
        }

        public async Task<ContentfulNotification> GetContentAsync(string notificationId)
        {
            Console.WriteLine($"Requesting Contentful NotificationId: {notificationId}");

            if (EntryIds.TryGetValue(notificationId, out var entryId))
            {
                return await _client.GetEntry<ContentfulNotification>(entryId);
            }

            throw new KeyNotFoundException(
            $"No Contentful entry ID has been configured for notification '{notificationId}'.");
        }

        private static readonly IReadOnlyDictionary<string, string> EntryIds =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["apprenticeship-assessment"] = "3WWkK4mSW3PbkSB6icZiYZ",
            ["off-the-job-training"] = "4I1Oy5doBciTMPfC9gGMPH",
            ["know-your-rights"] = "4b1s30h25lrLTDp0nH6rIu",
            ["connect-network"] = "4R39CYm5nw52E5jsroU7mJ",
            ["student-discount"] = "4SCnIK7GI4ldP1nS2l59U9",
            ["extra-support"] = "4V2a70h25lrLTDp0nH6rIu"
        };        
    }
}
