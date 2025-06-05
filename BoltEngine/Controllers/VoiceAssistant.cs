using AWS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Core;

namespace BoltEngine.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoiceAssistant : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var speechService = new SpeechService();
            //var res = await speechService.Process("s3://processaudiodoc/harvard.wav");

            return Ok(null);

        }

        [HttpPost]
        public async Task<IActionResult> ProcessAudio(List<IFormFile> files)
        {
            Stream stream;
            if (files != null && files.Count > 0)
            {
                stream = files[0].OpenReadStream();

            }
            else
            {
                stream = HttpContext.Request.Form.Files[0].OpenReadStream();

            }
            var mm = new MemoryStream();
            stream.CopyTo(mm);
            //var ss = new SpeechService();
            //var res= await  ss.ProcessAudio(mm);

            var stateMachine = new StateMachine();
            var res = await stateMachine.Process(mm);

            return Ok(res);

        }

        [HttpPost]
        public async Task<IActionResult> ProcessText( string text)
        {

            var stateMachine = new StateMachine();
            var res = await stateMachine.ProcessText(text);

            return Ok(res);

        }
    }
}
