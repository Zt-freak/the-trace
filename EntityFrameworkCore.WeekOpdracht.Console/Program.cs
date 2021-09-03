using EntityFrameworkCore.WeekOpdracht.Business;
using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace EntityFrameworkCore.WeekOpdracht.Console
{
    class Program
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            NLogger.Info("Hello world");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            ConsoleApplication app = serviceProvider.GetService<ConsoleApplication>();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ConsoleApplication>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddLogging(builder => {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddNLog("NLog.config");
            });
        }
    }
}
