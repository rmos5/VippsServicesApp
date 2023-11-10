using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace VippsServicesApp
{
    public static class Logging
    {
        private class DefaultLoggerFactory : ILoggerFactory
        {
            private class TraceLogger<T> : ILogger
            {
                public string CategoryName { get; }

                public TraceLogger(string categoryName)
                {
                    CategoryName = categoryName;
                }

                public IDisposable BeginScope<TState>(TState state)
                {
                    throw new NotSupportedException();
                }

                public bool IsEnabled(LogLevel logLevel)
                {
                    return true;
                }

                public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
                {
                    System.Diagnostics.Trace.WriteLine($"[{logLevel}] [{CategoryName}] {formatter.Invoke(state, exception)}");
                }
            }

            private Dictionary<string, ILogger> Loggers { get; } = new Dictionary<string, ILogger>();

            public void AddProvider(ILoggerProvider provider)
            {
            }

            public ILogger CreateLogger(string categoryName)
            {
                ILogger result = null;
                if (Loggers.ContainsKey(categoryName))
                    result = Loggers[categoryName];
                else
                {
                    result = new TraceLogger<string>(categoryName);
                    Loggers[categoryName] = result;
                }

                return result;
            }

            public void Dispose()
            {
            }
        }

        private static object _lock = new object();

        private static ILoggerFactory LoggerFactory { get; set; } = new DefaultLoggerFactory();
         
        public static void SetLoggerFactory(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public static void Trace<T>(string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger<T>().LogTrace(message);
            }
        }

        public static void Trace(Type type, string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger(type).LogTrace(message);
            }
        }

        public static void Debug<T>(string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger<T>().LogDebug(message);
            }
        }

        public static void Debug(Type type, string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger(type).LogDebug(message);
            }
        }

        public static void Information<T>(string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger<T>().LogInformation(message);
            }
        }

        public static void Information(Type type, string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger(type).LogInformation(message);
            }
        }

        public static void Warning<T>(string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger<T>().LogWarning(message);
            }
        }

        public static void Warning(Type type, string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger(type).LogWarning(message);
            }
        }

        public static void Error<T>(Exception error, string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger<T>().LogError(error, message);
            }
        }

        public static void Error(Type type, Exception error, string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger(type).LogError(error, message);
            }
        }

        public static void Critical<T>(string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger<T>().LogCritical(message);
            }
        }

        public static void Critical(Type type, string message)
        {
            if (LoggerFactory != null)
            {
                lock (_lock) LoggerFactory.CreateLogger(type).LogCritical(message);
            }
        }
    }
}
