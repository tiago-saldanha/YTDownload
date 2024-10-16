using YoutubeExplode;
using YTDownload.Application.Interfaces;
using YTDownload.Application.Services;

namespace YTDownload.API.Dependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            ConfigureFFmpeg(services);
            ConfigureYoutubeClient(services);
            return services;
        }

        private static IServiceCollection ConfigureFFmpeg(this IServiceCollection services)
        {
            var ffmpeg = Environment.OSVersion.Platform == PlatformID.Unix ? "ffmpeg" : "ffmpeg.exe";
            var ffmpegPath = Path.Combine(Directory.GetCurrentDirectory(), "lib", ffmpeg);

            if (!File.Exists(ffmpegPath))
            {
                throw new FileNotFoundException("FFmpeg não encontrado. Certifique-se de que o ffmpeg.exe/ffmpeg está na pasta lib.");
            }
            services.AddSingleton(ffmpegPath);
            return services;
        }

        private static IServiceCollection ConfigureYoutubeClient(this IServiceCollection services)
        {
            services.AddSingleton<YoutubeClient>();
            services.AddScoped<IYoutubeService, YoutubeService>();
            return services;
        }
    }
}
