using Microsoft.AspNetCore.Mvc;
using YTDownload.Application.Commands;
using YTDownload.Application.Interfaces;

namespace YTDownload.API.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class VideoController : ControllerBase
    {
        private readonly IYoutubeService _service;
        public VideoController(IYoutubeService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> DownloadVideoOnDisk([FromBody] DownloadVideoCommand command)
        {
            try
            {
                var filePath = await _service.DownloadVideo(command);
                return Ok($"Video salvo no Disco em: {filePath}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetVideo([FromBody] DownloadVideoCommand command)
        {
            try
            {
                var filePath = await _service.DownloadVideo(command);
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
