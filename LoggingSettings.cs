namespace VippsServicesApp
{
    internal struct LoggingSettings : ILoggingSettings
    {
        public string LoggingDirectoryPath { get; }

        public LoggingSettings(string loggingDirectoryPath)
        {
           LoggingDirectoryPath = loggingDirectoryPath;
        }
    }
}
