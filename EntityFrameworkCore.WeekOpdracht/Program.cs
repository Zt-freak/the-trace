using EntityFrameworkCore.WeekOpdracht.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using TheLogger;

namespace EntityFrameworkCore.WeekOpdracht
{
    public class Program
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            NLogger.Info("Hello world");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                //.UseNLog();
                .ConfigureLogging(builder =>
                    builder.AddTheLogger(config =>
                    {
                        config.logToConsole = true;
                        config.logToDB = true;
                        config.context = new DataContext();
                    })
        );
    }
}
