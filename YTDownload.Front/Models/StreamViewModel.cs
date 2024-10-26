namespace YTDownload.Front.Models
{
    public class StreamViewModel
    {
        public StreamViewModel(string containerName, string videoCodec, string resolution, double size, bool isAudioOnly, string audioCodec, string url)
        {
            ContainerName = containerName;
            VideoCodec = videoCodec;
            Resolution = resolution;
            Size = size;
            IsAudioOnly = isAudioOnly;
            AudioCodec = audioCodec;
            Url = url;
        }

        public string ContainerName { get; set; }
        public string VideoCodec { get; set; }
        public string Resolution { get; set; }
        public double Size { get; set; }
        public bool IsAudioOnly { get; set; }
        public string AudioCodec { get; set; }
        public string Url { get; set; }
    }
}
