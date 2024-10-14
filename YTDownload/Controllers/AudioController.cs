using Application.Core.Interfaces;
using Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace YTDownload.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class AudioController : ControllerBase
    {
        private readonly IYoutubeService _service;
        public AudioController(IYoutubeService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> DownloadAudioOnDisk([FromBody] DownloadAudioCommand command)
        {
            try
            {
                var filePath = await _service.DownloadAudio(command);
                return Ok($"Audio salvo no Disco em: {filePath}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAudio([FromBody] DownloadAudioCommand command)
        {
            try
            {
                var filePath = await _service.DownloadAudio(command);
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, "audio/mpeg", Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
