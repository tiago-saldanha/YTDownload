using Microsoft.Extensions.DependencyInjection;
using YoutubeExplode;
using WindowsApp;
using YTDownload.Application.Interfaces;
using YTDownload.Application.Services;

namespace YTDownload.App.DI
{
    public static class ContainerDI
    {
        public static FormApp ConfigureApp()
        {
            var services = new ServiceCollection();
            services.ConfigureServices();
            return Start(services);
        }
        
        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            ConfigureFFmpeg(services);
            ConfigureYoutubeClient(services);
            return services;
        }

        private static IServiceCollection ConfigureFFmpeg(this IServiceCollection services)
        {
            var ffmpeg = Environment.OSVersion.Platform == PlatformID.Unix ? "ffmpeg" : "ffmpeg.exe";
            var ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", ffmpeg);

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

        private static FormApp Start(this IServiceCollection services)
        {
            services.AddScoped<FormApp>();
            var serviceProvider = services.BuildServiceProvider();
            var mainForm = serviceProvider.GetRequiredService<FormApp>();
            return mainForm;
        }
    }
}
