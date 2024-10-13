namespace Application.Core.Interfaces
{
    public interface IVideoService
    {
        Task<string> DownloadAudio(string url, bool mp3 = false);
    }
}
