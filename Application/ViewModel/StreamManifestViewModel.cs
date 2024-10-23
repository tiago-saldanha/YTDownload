using YoutubeExplode.Videos.Streams;

namespace YTDownload.Application.ViewModel
{
    public class StreamManifestViewModel
    {
        public string Format { get; private set; }
        public double Size { get; private set; }
        public bool IsAudioOnly { get; private set; }
        public string AudioCodec { get; private set; }
        public string Resolution { get; private set; }
        public string VideoCodec { get; private set; }

        public StreamManifestViewModel(IStreamInfo stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            Format = stream.Container.Name;
            Size = stream.Size.MegaBytes;
            IsAudioOnly = stream.Container.IsAudioOnly;

            AddMedia(stream);
        }

        private void AddMedia(IStreamInfo stream)
        {
            if (stream is IAudioStreamInfo audioStream)
            {
                AudioCodec = audioStream.AudioCodec;
            }
            else if (stream is IVideoStreamInfo videoStream)
            {
                Resolution = videoStream.VideoQuality.Label;
                VideoCodec = videoStream.VideoCodec;
            }
        }
    }
}
