using Microsoft.Extensions.Logging;
using System;

namespace VippsServicesApp
{
    public static class Logging
    {
        private static ILoggerFactory LoggerFactory { get; set; }
        public static void SetLoggerFactory(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }

        public static void Debug<T>(string message)
        {
            if (LoggerFactory != null)
            {
                LoggerFactory.CreateLogger<T>().LogDebug(message);
            }
        }

        public static void Trace<T>(string message)
        {
            if (LoggerFactory != null)
            {
                LoggerFactory.CreateLogger<T>().LogTrace(message);
            }
        }

        public static void Information<T>(string message)
        {
            if (LoggerFactory != null)
            {
                LoggerFactory.CreateLogger<T>().LogInformation(message);
            }
        }

        public static void Warning<T>(string message)
        {
            if (LoggerFactory != null)
            {
                LoggerFactory.CreateLogger<T>().LogWarning(message);
            }
        }

        public static void Error<T>(Exception error, string message)
        {
            if (LoggerFactory != null)
            {
                LoggerFactory.CreateLogger<T>().LogError(error, message);
            }
        }

        public static void Critical<T>(Exception error, string message)
        {
            if (LoggerFactory != null)
            {
                LoggerFactory.CreateLogger<T>().LogCritical(error, message);
            }
        }
    }
}
