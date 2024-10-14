using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Application.Core.Extensions;
using Application.Core.Interfaces;
using YoutubeExplode.Videos;
using YoutubeExplode.Converter;
using Application.Commands;

namespace Application.Core.Services
{
    public class VideoService : IVideoService
    {
        private readonly YoutubeClient _client;
        private string OutputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "output");
        private readonly string _ffmpegPath;

        public VideoService(YoutubeClient client, string ffmpegPath)
        {
            _client = client;
            CreateOutputDirectory();
            _ffmpegPath = ffmpegPath;
        }

        public async Task<string> DownloadVideo(DownloadVideoCommand command)
        {
            var filePath = string.Empty;
            try
            {
                var video = await GetVideoAsync(command.Url);
                var manifest = await GetManifestAsync(video.Id);
                var audioStreamInfo = GetBestAudioStreamInfo(manifest);
                filePath = Path.Combine(OutputDirectory, $"{video.Title.FormaterName()}.{Container.WebM}");

                IVideoStreamInfo videoStreamInfo = GetVideoStreamInfo(manifest, command.Resolutiuon, command.Mp4);

                var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
                await _client.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(filePath).SetFFmpegPath(_ffmpegPath).Build());

                if (command.Mp4)
                    filePath = VideoToMp4(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }

            return filePath;
        }

        public async Task<string> DownloadAudio(DownloadAudioCommand command)
        {
            var filePath = string.Empty;
            try
            {
                var video = await GetVideoAsync(command.Url);
                var manifest = await GetManifestAsync(video.Id);
                var info = GetBestAudioStreamInfo(manifest);

                filePath = Path.Combine(OutputDirectory, $"{video.Title.FormaterName()}.{info.Container.Name}");

                await _client.Videos.Streams.DownloadAsync(info, filePath);

                if (command.Mp3)
                    filePath = AudioToMp3(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }

            return filePath;
        }

        private async Task<Video> GetVideoAsync(string url) => await _client.Videos.GetAsync(url);

        private async Task<StreamManifest> GetManifestAsync(string id) => await _client.Videos.Streams.GetManifestAsync(id);

        private IStreamInfo GetBestAudioStreamInfo(StreamManifest manifest) => manifest.GetAudioStreams().GetWithHighestBitrate();

        private IVideoStreamInfo GetVideoStreamInfo(StreamManifest manifest, string resolution, bool mp4)
        {
            IVideoStreamInfo videoStreamInfo = manifest.GetVideoStreams()
                    .Where(s => s.Container == Container.WebM && (s.VideoQuality.Label == resolution))
                    .OrderByDescending(s => s.Size)
                    .First();

            if (videoStreamInfo != null)
            {
                return videoStreamInfo;
            }

            videoStreamInfo = manifest
                .GetVideoStreams()
                .Where(s => s.Container == Container.WebM && (s.VideoQuality.Label == "1080p" || s.VideoQuality.Label == "720p" || s.VideoQuality.Label == "144p"))
                .OrderByDescending(s => s.Size)
                .First();

            return videoStreamInfo;
        }

        private void CreateOutputDirectory()
        {
            if (!Directory.Exists(OutputDirectory)) Directory.CreateDirectory(OutputDirectory);
        }

        private string AudioToMp3(string filePath)
        {
            var outputFilePath = Path.ChangeExtension(filePath, ".mp3");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _ffmpegPath,
                    Arguments = $"-i \"{filePath}\" \"{outputFilePath}\" -y",
                    UseShellExecute = true,
                    CreateNoWindow = false,
                }
            };

            process.Start();
            process.WaitForExit();

            return outputFilePath;
        }

        private string VideoToMp4(string filePath)
        {
            var threadsToUse = Math.Max(1, Environment.ProcessorCount - 2);
            var outputFilePath = Path.ChangeExtension(filePath, ".mp4");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _ffmpegPath,
                    Arguments = $"-i \"{filePath}\" -c:v libx264 -c:a aac -threads {threadsToUse} -loglevel verbose -y \"{outputFilePath}\"",
                    UseShellExecute = true,
                    CreateNoWindow = false,
                }
            };

            process.Start();
            process.WaitForExit();

            return outputFilePath;
        }
    }
}
