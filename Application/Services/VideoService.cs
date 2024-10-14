using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Application.Core.Extensions;
using Application.Core.Interfaces;

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

        public async Task<string> DownloadAudio(string url, bool mp3 = false)
        {
            var filePath = string.Empty;
            try
            {
                var video = await _client.Videos.GetAsync(url);
                var streamManifest = await _client.Videos.Streams.GetManifestAsync(video.Id);
                var streamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();

                filePath = Path.Combine(OutputDirectory, $"{video.Title.FormaterName()}.{streamInfo.Container.Name}");

                await _client.Videos.Streams.DownloadAsync(streamInfo, filePath);

                if (mp3)
                    filePath = AudioToMp3(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }

            return filePath;
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
    }
}
