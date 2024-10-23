using YTDownload.Application.Commands;
using YTDownload.Application.ViewModel;

namespace YTDownload.Application.Interfaces
{
    public interface IYoutubeService
    {
        Task<string> DownloadAudio(DownloadAudioCommand command);
        Task<string> DownloadVideo(DownloadVideoCommand command);
        Task<List<StreamManifestViewModel>> DownloadManifestInfo(string url);
        Task<string> Download(DownloadCommand command);
    }
}
