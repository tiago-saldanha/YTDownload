using Microsoft.Extensions.DependencyInjection;
using YoutubeExplode;
using YTDownload.Application.Interfaces;
using YTDownload.Application.Services;

namespace YTDownload.CrossCutting.AppDependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection ProvideServices(this IServiceCollection services)
        {
            return services.YoutubeClient();
        }

        private static IServiceCollection YoutubeClient(this IServiceCollection services)
        {
            services.AddSingleton<YoutubeClient>();
            services.AddScoped<IYoutubeService, YoutubeService>();
            return services;
        }
    }
}
