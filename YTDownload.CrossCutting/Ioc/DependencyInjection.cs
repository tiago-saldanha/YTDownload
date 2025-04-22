using Microsoft.Extensions.DependencyInjection;
using YoutubeExplode;
using YTDownload.Application.Interfaces;
using YTDownload.Application.Services;

namespace YTDownload.CrossCutting.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection ProvideServices(this IServiceCollection services) => services.ConfigureYoutubeClient();

        private static IServiceCollection ConfigureYoutubeClient(this IServiceCollection services)
        {
            services.AddSingleton<YoutubeClient>();
            services.AddScoped<IYoutubeService, YoutubeService>();
            return services;
        }
    }
}
