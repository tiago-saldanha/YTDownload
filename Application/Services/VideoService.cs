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
        private string OutputDirectory = Directory.GetCurrentDirectory() + "\\Output";
        private string FFmpegPath { get; set; }
        public VideoService(YoutubeClient client)
        {
            _client = client;
            CreateOutputDirectory();
            FFmpegPath = SetFFmpegPath();
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
        private string SetFFmpegPath()
        {
            var libDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Lib");
            var ffmpegPath = Path.Combine(libDirectory, "ffmpeg.exe");
            if (!File.Exists(ffmpegPath))
            {
                throw new FileNotFoundException("FFmpeg não encontrado. Certifique-se de que o ffmpeg.exe está na pasta Lib.");
            }
            return ffmpegPath;
        }
        private string AudioToMp3(string filePath)
        {
            var mp3FilePath = Path.ChangeExtension(filePath, ".mp3");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = FFmpegPath,
                    Arguments = $"-i \"{filePath}\" \"{mp3FilePath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            return mp3FilePath;
        }
    }
}
