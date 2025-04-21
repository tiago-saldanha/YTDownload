using Microsoft.AspNetCore.Mvc;
using YTDownload.Application.Commands;
using YTDownload.Application.Interfaces;

namespace YTDownload.API.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class ManifestController(IYoutubeService _service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] DownloadManifestCommand command)
        {
            try
            {
                var manifests = await _service.DownloadManifest(command.Url);
                return Ok(manifests);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
