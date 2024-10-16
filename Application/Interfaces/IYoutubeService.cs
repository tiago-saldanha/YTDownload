using YTDownload.Application.Commands;

namespace YTDownload.Application.Interfaces
{
    public interface IYoutubeService
    {
        Task<string> DownloadAudio(DownloadAudioCommand command);
        Task<string> DownloadVideo(DownloadVideoCommand command);
    }
}
