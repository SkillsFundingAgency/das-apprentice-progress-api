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

        public async Task<ContentfulNotification> GetContentAsync(string entryId)
        {
            Console.WriteLine($"Requesting Contentful EntryId: {entryId}");

            return await _client.GetEntry<ContentfulNotification>(entryId);
        }
    }
}
