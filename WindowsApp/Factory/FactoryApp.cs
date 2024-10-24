using Microsoft.Extensions.DependencyInjection;
using WindowsApp;
using Serilog;
using YTDownload.CrossCutting.AppDependencies;

namespace YTDownload.App.Factory
{
    public static class FactoryApp
    {
        public static FormApp Build()
        {
            return new ServiceCollection()
                .Logging()
                .ProvideServices()
                .Start();
        }

        private static IServiceCollection Logging(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\app-log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(configure => configure.AddSerilog(dispose: true));

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
