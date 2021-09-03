using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TheLogger
{
    public sealed class TheLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private TheLoggerConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, TheLogger> _loggers = new();

        public TheLoggerProvider(
            IOptionsMonitor<TheLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new TheLogger(name, GetCurrentConfig));

        private TheLoggerConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}