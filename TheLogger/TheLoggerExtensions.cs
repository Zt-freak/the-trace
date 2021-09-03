using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace TheLogger
{
    public static class TheLoggerExtensions
    {
        public static ILoggingBuilder AddTheLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, TheLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <TheLoggerConfiguration, TheLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddTheLogger(this ILoggingBuilder builder, Action<TheLoggerConfiguration> configure)
        {
            builder.AddTheLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
