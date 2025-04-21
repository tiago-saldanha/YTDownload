using Microsoft.AspNetCore.Mvc;
using YTDownload.Application.Commands;
using YTDownload.Application.Interfaces;
using YTDownload.API.Helper;

namespace YTDownload.API.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class MediaController(IYoutubeService _service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Download([FromBody] DownloadCommand command)
        {
            try
            {
                var path = await _service.Download(command);
                var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                return File(stream, MimeType.GetContentType(path), Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
