using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;
using YTDownload.Application.Extensions;
using YTDownload.Application.Interfaces;
using YTDownload.Application.Commands;
using YTDownload.Application.ViewModel;

namespace YTDownload.Application.Services
{
    public class YoutubeService : IYoutubeService
    {
        private readonly YoutubeClient _client;
        private string OutputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "output");
        private readonly string _ffmpegPath;

        public YoutubeService(YoutubeClient client, string ffmpegPath)
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

                IVideoStreamInfo videoStreamInfo = GetVideoStreamInfo(manifest, command.Resolution, command.Mp4);

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

        public async Task<List<StreamManifestViewModel>> DownloadManifestInfo(string url)
        {
            try
            {
                var video = await GetVideoAsync(url);
                var manifest = await GetManifestAsync(video.Id);
                var streams = manifest.Streams.Select(s => new StreamManifestViewModel(s)).ToList();
                return streams;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

        public async Task<string> Download(DownloadCommand command)
        {
            var filePath = string.Empty;
            try
            {
                var video = await GetVideoAsync(command.Url);
                var manifest = await GetManifestAsync(video.Id);
                IStreamInfo audioStreamInfo;
                if (command.IsAudioOnly)
                {
                    audioStreamInfo = DownloadAudioStream(manifest, s => s.AudioCodec == command.AudioCodec && s.Container.Name == command.ContainerName);
                    filePath = Path.Combine(OutputDirectory, $"{video.Title.FormaterName()}.{audioStreamInfo.Container.Name}");
                    await _client.Videos.Streams.DownloadAsync(audioStreamInfo, filePath);
                    return filePath;
                }
                else
                {
                    audioStreamInfo = DownloadAudioStream(manifest, s => s.Container.Name == command.ContainerName);
                    filePath = Path.Combine(OutputDirectory, $"{video.Title.FormaterName()}.{audioStreamInfo.Container.Name}");
                    IVideoStreamInfo videoStreamInfo = DownloadVideoStream(manifest, s => s.Container.ToString() == command.ContainerName && s.VideoQuality.Label.Contains(command.Resolution));
                    var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
                    await _client.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(filePath).SetFFmpegPath(_ffmpegPath).Build());
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return filePath;
        }

        public async Task Converter(string filePath)
        {
            await Task.Run(() =>
            {
                _ = AudioToMp3(filePath);
            });
        }

        private IVideoStreamInfo DownloadVideoStream(StreamManifest manifest, Func<VideoOnlyStreamInfo, bool> predicate)
        {
            IVideoStreamInfo stream = manifest.GetVideoOnlyStreams().Where(predicate).OrderByDescending(s => s.Size).First();
            return stream;
        }

        private IStreamInfo DownloadAudioStream(StreamManifest manifest, Func<AudioOnlyStreamInfo, bool> predicate)
        {
            IStreamInfo stream = manifest.GetAudioOnlyStreams().Where(predicate).OrderByDescending(s => s.Size).First();
            if (stream == null)
            {
                stream = GetBestAudioStreamInfo(manifest);
            }
            return stream;
        }

        private async Task<Video> GetVideoAsync(string url) => await _client.Videos.GetAsync(url);

        private async Task<StreamManifest> GetManifestAsync(string id) => await _client.Videos.Streams.GetManifestAsync(id);

        private IStreamInfo GetBestAudioStreamInfo(StreamManifest manifest) => manifest.GetAudioStreams().GetWithHighestBitrate();

        private IVideoStreamInfo GetVideoStreamInfo(StreamManifest manifest, string resolution, bool mp4)
        {
            IVideoStreamInfo videoStreamInfo = manifest.GetVideoStreams()
                    .Where(s => s.Container == Container.WebM && s.VideoQuality.Label.Contains(resolution))
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
                    Arguments = $"-i \"{filePath}\" -preset ultrafast -b:a 192k \"{outputFilePath}\" -y",
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
                    Arguments = $"-i \"{filePath}\" -c:v libx264 -preset ultrafast -c:a aac -b:a 128k -threads {threadsToUse} -y \"{outputFilePath}\"",
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
