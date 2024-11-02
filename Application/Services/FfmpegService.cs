using System.Diagnostics;

namespace YTDownload.Application.Services
{
    public static class FfmpegService
    {
        public static string ffmpeg = Get();

        private static string Get()
        {
            string ffmpeg = Environment.OSVersion.Platform == PlatformID.Unix ? "ffmpeg" : "ffmpeg.exe";
            string ffmpegPath = Path.Combine(AppContext.BaseDirectory, "lib", ffmpeg);

            if (!File.Exists(ffmpegPath))
            {
                throw new FileNotFoundException("FFmpeg não encontrado. Certifique-se de que o ffmpeg.exe/ffmpeg está na pasta lib.");
            }
            return ffmpegPath;
        }

        public static string AudioToMp3(string filePath)
        {
            string outputFilePath = Path.ChangeExtension(filePath, ".mp3");

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpeg,
                    Arguments = $"-i \"{filePath}\" -preset ultrafast -b:a 192k \"{outputFilePath}\" -y",
                    UseShellExecute = true,
                    CreateNoWindow = false,
                }
            };

            process.Start();
            process.WaitForExit();

            return outputFilePath;
        }

        private static string VideoToMp4(string filePath)
        {
            int threadsToUse = Math.Max(1, Environment.ProcessorCount - 2);
            string outputFilePath = Path.ChangeExtension(filePath, ".mp4");

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpeg,
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