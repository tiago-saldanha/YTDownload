using Application.Commands;

namespace Application.Core.Interfaces
{
    public interface IYoutubeService
    {
        Task<string> DownloadAudio(DownloadAudioCommand command);
        Task<string> DownloadVideo(DownloadVideoCommand command);
    }
}
