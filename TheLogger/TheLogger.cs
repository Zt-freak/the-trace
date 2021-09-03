using EntityFrameworkCore.WeekOpdracht.Business;
using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace TheLogger
{
    public class TheLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<TheLoggerConfiguration> _getCurrentConfig;
        private readonly DataContext _context;

        public TheLogger(string name, Func<TheLoggerConfiguration> getCurrentConfig) => (_name, _getCurrentConfig) = (name, getCurrentConfig);

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) =>
            _getCurrentConfig().LogLevels.ContainsKey(logLevel);

        public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            TheLoggerConfiguration config = _getCurrentConfig();
            if (config.logToConsole)
            {
                if (config.EventId == 0 || config.EventId == eventId.Id)
                {
                    ConsoleColor originalColor = Console.ForegroundColor;

                    Console.ForegroundColor = config.LogLevels[logLevel];
                    Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

                    Console.ForegroundColor = originalColor;
                    Console.WriteLine($"     {_name} - {formatter(state, exception)}");
                }
            }

            if (config.logToDB)
            {
                config.context.Logs.Add(
                    new Log()
                    {
                        Message = formatter(state, exception),
                        Level = logLevel.ToString()
                    }
                );
                config.context.SaveChanges();
            }
        }
    }
}
