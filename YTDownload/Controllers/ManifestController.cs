using Microsoft.AspNetCore.Mvc;
using YTDownload.Application.Commands;
using YTDownload.Application.Interfaces;

namespace YTDownload.API.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class ManifestController : ControllerBase
    {
        private readonly IYoutubeService _service;

        public ManifestController(IYoutubeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] DownloadManifestCommand command)
        {
            try
            {
                var manifests = await _service.DownloadManifestInfo(command.Url);
                return Ok(manifests);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
