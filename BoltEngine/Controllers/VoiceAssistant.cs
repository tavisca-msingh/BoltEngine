using AWS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BoltEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceAssistant : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> Get() 
        {
            var speechService = new SpeechService();
            var res = await speechService.Process("s3://processaudiodoc/harvard.wav");

            return Ok(res);

        }
    }
}
