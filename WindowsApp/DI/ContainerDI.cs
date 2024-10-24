using Microsoft.Extensions.DependencyInjection;
using YoutubeExplode;
using WindowsApp;
using YTDownload.Application.Interfaces;
using YTDownload.Application.Services;
using Serilog;

namespace YTDownload.App.DI
{
    public static class ContainerDI
    {
        public static FormApp ConfigureApp()
        {
            return new ServiceCollection()
                .ConfigureLogging()
                .ConfigureServices()
                .Start();
        }

        private static IServiceCollection ConfigureLogging(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\app-log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(configure => configure.AddSerilog(dispose: true));

            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services.ConfigureFFmpeg().ConfigureYoutubeClient();
        }

        private static IServiceCollection ConfigureFFmpeg(this IServiceCollection services)
        {
            var ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", "ffmpeg.exe");

            if (!File.Exists(ffmpegPath))
            {
                throw new FileNotFoundException("FFmpeg não encontrado. Certifique-se de que o ffmpeg.exe está na pasta lib.");
            }

            return services.AddSingleton(ffmpegPath);
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
