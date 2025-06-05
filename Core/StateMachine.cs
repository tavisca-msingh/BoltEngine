using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Comprehend.Model;
using AWS;

namespace Core
{
    public class StateMachine
    {

        private readonly SpeechService speechService = new SpeechService();
        private readonly ComprehendService comprehendService = new ComprehendService(); 
        public StateMachine() 
        {
            


        }

        public async Task<List<Entity>> Process(MemoryStream memoryStream)
        {
            // get the input file
            // process audio and get text
            try
            {
                var data = await speechService.ProcessAudio<TranscribeData>(memoryStream);

                // get the entities from text
                var insights = await comprehendService.GetInsights(data.results.transcripts[0].transcript);
                // update the request 

                return insights;
            }
            catch(Exception ex) { throw ex; }


            // 

        }


        public async Task<List<Entity>> ProcessText(string text)
        {
            // get the input file
            // process audio and get text
            try
            {

                // get the entities from text
                var insights = await comprehendService.GetInsights(text);
                // update the request 

                return insights;
            }
            catch (Exception ex) { throw ex; }


            // 

        }


    }
}
