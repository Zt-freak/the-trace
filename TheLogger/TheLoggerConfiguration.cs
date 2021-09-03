using System;
using System.Collections.Generic;
using EntityFrameworkCore.WeekOpdracht.Business;
using Microsoft.Extensions.Logging;

namespace TheLogger
{
    public class TheLoggerConfiguration
    {
        public int EventId { get; set; }
        public bool logToConsole { get; set; }
        public bool logToDB { get; set; }
        public DataContext context { get; set; }
        public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new()
        {
            [LogLevel.Information] = ConsoleColor.Green,
            [LogLevel.Warning] = ConsoleColor.DarkMagenta,
            [LogLevel.Error] = ConsoleColor.Red
        };
    }
}