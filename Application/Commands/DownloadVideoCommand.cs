namespace YTDownload.Application.Commands
{
    public record DownloadVideoCommand(string Url, string Resolution, bool Mp4);
}
