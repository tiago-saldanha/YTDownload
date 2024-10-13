using Application.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using YTDownload.Commands;

namespace YTDownload.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _service;
        public VideoController(IVideoService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> DownloadAudio([FromBody] DownloadAudioCommand command)
        {
            try
            {
                var filePath = await _service.DownloadAudio(command.Url);
                return Ok($"Audio salvo no Disco em: {filePath}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DownloadAudioMp3([FromBody] DownloadAudioCommand command)
        {
            try
            {
                var filePath = await _service.DownloadAudio(command.Url, true);
                var audioFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(audioFileStream, "audio/mpeg", Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
