using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;
using YTDownload.Application.Extensions;
using YTDownload.Application.Interfaces;
using YTDownload.Application.Commands;
using YTDownload.Application.ViewModel;
using Microsoft.Extensions.Logging;

namespace YTDownload.Application.Services
{
    public class YoutubeService : IYoutubeService
    {
        private readonly YoutubeClient _client;
        private string OutputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "downloads");
        private readonly ILogger<YoutubeService> _logger;

        public YoutubeService(YoutubeClient client, ILogger<YoutubeService> logger)
        {
            _client = client;
            _logger = logger;
            CreateOutputDirectory();
        }

        public async Task<List<StreamManifestViewModel>> DownloadManifestInfo(string url)
        {
            try
            {
                _logger.LogInformation($"Iniciando o Download do manifesto do vídeo [{url}].");
                var video = await _client.Videos.GetAsync(url);

                var manifest = await _client.Videos.Streams.GetManifestAsync(video.Id);
                var streams = manifest.Streams.Select(s => StreamManifestViewModel.Create(s, url)).ToList();

                _logger.LogInformation($"Download dos Streams realizados com sucesso [{url}]");
                return streams;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar baixar o manifesto do video [{url}].");
                throw new Exception(ex.ToString(), ex);
            }
        }

        public async Task<string> Download(DownloadCommand command)
        {
            try
            {
                var video = await _client.Videos.GetAsync(command.Url);
                var title = video.Title.FormaterName();
                _logger.LogInformation($"Iniciando o Download do vídeo [{title}].");

                var manifest = await _client.Videos.Streams.GetManifestAsync(video.Id);
                return command.IsAudioOnly ? await DownloadAudio(manifest, command, title) : await DownloadVideo(manifest, command, title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar baixar o video [{command.Url}].");
                return ex.ToString();
            }
        }

        public async Task Converter(string filePath)
        {
            await Task.Run(() =>
            {
                _ = FfmpegService.ConvertAudioToMp3(filePath);
            });
        }

        private IVideoStreamInfo DownloadVideoStream(StreamManifest manifest, Func<VideoOnlyStreamInfo, bool> predicate) => 
            manifest.GetVideoOnlyStreams().Where(predicate).OrderByDescending(s => s.Size).First();

        private IStreamInfo DownloadAudioStream(StreamManifest manifest, Func<AudioOnlyStreamInfo, bool> predicate) => 
             manifest.GetAudioOnlyStreams().Where(predicate).OrderByDescending(s => s.Size).First() ?? GetBestAudioStreamInfo(manifest);

        private IStreamInfo GetBestAudioStreamInfo(StreamManifest manifest) => manifest.GetAudioStreams().GetWithHighestBitrate();

        private async Task<string> DownloadVideo(StreamManifest manifest, DownloadCommand command, string title)
        {
            var audioStreamInfo = DownloadAudioStream(manifest, s => s.Container.Name == command.ContainerName);
            _logger.LogInformation($"Download do Stream de Audio realizado com sucesso [{audioStreamInfo.Container.Name}].");

            var filePath = Path.Combine(OutputDirectory, $"{title}.{audioStreamInfo.Container.Name}");
            var videoStreamInfo = DownloadVideoStream(manifest, s => s.Container.ToString() == command.ContainerName && s.VideoQuality.Label.Contains(command.Resolution));
            _logger.LogInformation($"Download do Stream de Video realizado com sucesso [{videoStreamInfo.Container.Name}].");

            IStreamInfo[] streamInfos = { audioStreamInfo, videoStreamInfo };
            _logger.LogInformation($"Iniciando Download do Video [{command.Url}].");

            if (File.Exists(filePath)) File.Delete(filePath);

            await _client.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(filePath).SetFFmpegPath(FfmpegService.ffmpeg).Build());
            _logger.LogInformation($"Download do Video realizado com sucesso [{filePath}].");

            return filePath;
        }

        private async Task<string> DownloadAudio(StreamManifest manifest, DownloadCommand command, string title)
        {
            var audioStreamInfo = DownloadAudioStream(manifest, s => s.AudioCodec == command.AudioCodec && s.Container.Name == command.ContainerName);
            _logger.LogInformation($"Download do Stream de Audio realizado com sucesso [{audioStreamInfo.Container.Name}].");

            var filePath = Path.Combine(OutputDirectory, $"{title}.{audioStreamInfo.Container.Name}");
            await _client.Videos.Streams.DownloadAsync(audioStreamInfo, filePath);

            _logger.LogInformation($"Download do Audio realizado com sucesso [{filePath}].");
            return filePath;
        }

        private void CreateOutputDirectory()
        {
            if (!Directory.Exists(OutputDirectory)) Directory.CreateDirectory(OutputDirectory);
        }
    }
}
