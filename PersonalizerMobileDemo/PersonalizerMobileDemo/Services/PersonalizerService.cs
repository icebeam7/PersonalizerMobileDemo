using System;
using System.Collections.Generic;

using PersonalizerMobileDemo.Models;

using Microsoft.Azure.CognitiveServices.Personalizer;
using Microsoft.Azure.CognitiveServices.Personalizer.Models;

namespace PersonalizerMobileDemo.Services
{
    public class PersonalizerService
    {
        private static readonly string apiKey = "";
        private static readonly string serviceEndpoint = "";

        private static PersonalizerClient client = Initialize();
        private static IList<RankableAction> actions = MenuService.GetActions();

        public static RankResponse GetRecommendation(User user)
        {
            var currentContext = new List<object>()
            {
                new { time = user.TimeOfDay },
                new { taste = user.Taste }
            };

            var excludeActions = new List<string> { };
            var eventId = Guid.NewGuid().ToString();

            var request = new RankRequest(actions, currentContext, excludeActions, eventId);
            var response = client.Rank(request);
            return response;
        }

        public static void SendFeedback(RankResponse response, float reward)
        {
            client.Reward(response.EventId, new RewardRequest(reward));
        }

        private static PersonalizerClient Initialize()
        {
            return new PersonalizerClient(new ApiKeyServiceClientCredentials(apiKey))
            {
                Endpoint = serviceEndpoint
            };
        }
    }
}
