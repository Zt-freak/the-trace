using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EntityFrameworkCore.WeekOpdracht
{
    public class Program
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetLogger("TheLogger");
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
                });
    }
}
