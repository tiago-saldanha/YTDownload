using Microsoft.AspNetCore.Mvc;
using YTDownload.Application.Commands;
using YTDownload.Application.Interfaces;
using YTDownload.API.Helper;

namespace YTDownload.API.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class MediaController : ControllerBase
    {
        private readonly IYoutubeService _service;

        public MediaController(IYoutubeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Download([FromBody] DownloadCommand command)
        {
            try
            {
                var filePath = await _service.Download(command);
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, MimeType.GetContentType(filePath), Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
