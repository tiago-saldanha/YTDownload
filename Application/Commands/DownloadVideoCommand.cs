namespace Application.Commands
{
    public class DownloadVideoCommand
    {
        public string Url { get; set; }
        public string Resolutiuon { get; set; }
        public bool Mp4 { get; set; }
    }

    public enum Resolution
    {
        _144 = 144,
        _240 = 240,
        _360 = 360,
        _480 = 480,
        _720 = 720,
        _1080 = 1080,
        _2160 = 2160
    }
}
