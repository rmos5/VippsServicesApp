using Microsoft.Extensions.Logging;
using System;
using System.Extensions;

namespace VippsServicesApp
{
    public static class Logging
    {
        private static object _lock = new object();

        public static IServiceProvider ServiceProvider { get; set; }

        public static bool IncludeStackTrace { get; set; }

        public static void Trace<T>(string message)
        {
            Trace(typeof(T), message);
        }

        public static void Trace(object source, string message)
        {
            Trace(source.GetType(), message);
        }

        public static void Trace(Type type, string message)
        {
            lock (_lock)
            {
                Type loggerType = typeof(ILogger<>).MakeGenericType(new[] { type });
                ILogger logger = ServiceProvider?.GetService(loggerType) as ILogger;
                logger?.LogTrace(message);
            }
        }

        public static void Debug<T>(string message)
        {
            Debug(typeof(T), message);
        }

        public static void Debug(object source, string message)
        {
            Debug(source.GetType(), message);
        }

        public static void Debug(Type type, string message)
        {
            lock (_lock)
            {
                Type loggerType = typeof(ILogger<>).MakeGenericType(new[] { type });
                ILogger logger = ServiceProvider?.GetService(loggerType) as ILogger;
                logger?.LogDebug(message);
            }
        }

        public static void Information<T>(string message)
        {
            Information(typeof(T), message);
        }

        public static void Information(object source, string message)
        {
            Information(source.GetType(), message);
        }

        public static void Information(Type type, string message)
        {
            lock (_lock)
            {
                Type loggerType = typeof(ILogger<>).MakeGenericType(new[] { type });
                ILogger logger = ServiceProvider?.GetService(loggerType) as ILogger;
                logger?.LogInformation(message);
            }
        }

        public static void Warning<T>(string message)
        {
            Warning(typeof(T), message);
        }

        public static void Warning(object source, string message)
        {
            Warning(source.GetType(), message);
        }

        public static void Warning(Type type, string message)
        {
            lock (_lock)
            {
                Type loggerType = typeof(ILogger<>).MakeGenericType(new[] { type });
                ILogger logger = ServiceProvider?.GetService(loggerType) as ILogger;
                logger?.LogWarning(message);
            }
        }

        private static string GetErrorMessage(string message, Exception error)
        {
            return message + Environment.NewLine + error.GetAllMessages();
        }

        public static void Error<T>(Exception error, string message)
        {
            Error(typeof(T), error, message);
        }

        public static void Error(object source, Exception error, string message)
        {
            Error(source.GetType(), error, message);
        }

        public static void Error(Type type, Exception error, string message)
        {
            lock (_lock)
            {
                Type loggerType = typeof(ILogger<>).MakeGenericType(new[] { type });
                ILogger logger = ServiceProvider?.GetService(loggerType) as ILogger;
                if (IncludeStackTrace)
                    logger?.LogError(error, message);
                else
                    logger?.LogError(GetErrorMessage(message, error));
            }
        }

        public static void Critical<T>(Exception error, string message)
        {
            Critical(typeof(T), error, message);
        }

        public static void Critical(object source, Exception error, string message)
        {
            Critical(source.GetType(), error, message);
        }

        public static void Critical(Type type, Exception error, string message)
        {
            lock (_lock)
            {
                Type loggerType = typeof(ILogger<>).MakeGenericType(new[] { type });
                ILogger logger = ServiceProvider?.GetService(loggerType) as ILogger;
                if (IncludeStackTrace)
                    logger?.LogCritical(error, message);
                else
                    logger?.LogCritical(GetErrorMessage(message, error));
            }
        }
    }
}
