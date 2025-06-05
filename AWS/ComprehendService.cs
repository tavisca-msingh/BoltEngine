using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AWS
{
    public class ComprehendService
    {
        public ComprehendService() { }

        public async Task<List<Entity>> GetInsights(string text)
        {

            var client = new AmazonComprehendClient();
            var detectKeyPhrasesRequest = new DetectEntitiesRequest()
            {
                Text = text ,
                LanguageCode = "en",
            };

            var detectEntitiesResponse = await client.DetectEntitiesAsync(detectKeyPhrasesRequest);

            foreach (var e in detectEntitiesResponse.Entities)
            {
                Console.WriteLine($"Text: {e.Text}, Type: {e.Type}, Score: {e.Score}, BeginOffset: {e.BeginOffset}, EndOffset: {e.EndOffset}");
            }

            return detectEntitiesResponse.Entities;


        }
    }
}
