namespace YTDownload.Application.Commands
{
    public class DownloadVideoCommand
    {
        public string Url { get; set; }
        public string Resolution { get; set; }
        public bool Mp4 { get; set; }
    }
}
